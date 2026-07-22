using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Umbraco.Automate.Core.Actions;
using Umbraco.Community.Automate.ClickUp.Models;

namespace Umbraco.Community.Automate.ClickUp;

/// <summary>
/// Maps a non-success ClickUp API response into a failed <see cref="ActionResult"/> — shared
/// across every action that calls the ClickUp API, so the status-code-to-category mapping and
/// error-body parsing only need to be written once.
/// </summary>
internal static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Maps <paramref name="response"/> to a failed <see cref="ActionResult"/>: categorizes by
    /// HTTP status code, and pulls the message out of ClickUp's <c>{ "err": "..." }</c> error
    /// body when present (falling back to the response's reason phrase otherwise). Only call
    /// this when <see cref="HttpResponseMessage.IsSuccessStatusCode"/> is <see langword="false"/>.
    /// </summary>
    public static async Task<ActionResult> ToClickUpFailureAsync(
        this HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var category = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => StepRunErrorCategory.Authentication,
            (HttpStatusCode)429 => StepRunErrorCategory.RateLimiting,
            HttpStatusCode.BadRequest or HttpStatusCode.NotFound => StepRunErrorCategory.Validation,
            _ when (int)response.StatusCode >= 500 => StepRunErrorCategory.ServiceUnavailable,
            _ => StepRunErrorCategory.InvalidResponse,
        };

        string? errorMessage = null;
        try
        {
            var errorBody = await response.Content.ReadFromJsonAsync<ClickUpErrorResponse>(cancellationToken);
            errorMessage = errorBody?.Err;
        }
        catch (JsonException)
        {
            // Response body wasn't JSON (or didn't match) — fall back to the reason phrase below.
        }

        return ActionResult.Failed(
            new InvalidOperationException(
                $"ClickUp API error ({(int)response.StatusCode}): {errorMessage ?? response.ReasonPhrase ?? "unknown error"}"),
            category);
    }
}
