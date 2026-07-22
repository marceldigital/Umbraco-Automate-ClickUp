using System.Text.Json.Serialization;

namespace Umbraco.Community.Automate.ClickUp.Models;

/// <summary>
/// ClickUp's error response body, returned on non-success status codes
/// (e.g. <c>{ "err": "List not found", "ECODE": "..." }</c>).
/// </summary>
internal sealed class ClickUpErrorResponse
{
    [JsonPropertyName("err")]
    public string? Err { get; set; }
}
