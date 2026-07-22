using System.Net.Http.Json;
using Umbraco.Automate.Core.Actions;
using Umbraco.Automate.OpenIddict.Credentials;
using Umbraco.Community.Automate.ClickUp.Models;
using Umbraco.Community.Automate.ClickUp.Connection;

namespace Umbraco.Community.Automate.ClickUp.Actions;

/// <summary>
/// Creates a task in a ClickUp List via the ClickUp Create Task API.
/// Requires a ClickUp connection.
/// </summary>
[Action("clickup.createTask", "Create Task",
    Description = "Creates a task in a ClickUp List.",
    Group = Constants.ActionGroup,
    Icon = "icon-checkbox",
    ConnectionTypeAlias = ClickUpConnectionType.ConnectionAlias)]
public sealed class CreateTaskAction : ActionBase<CreateTaskSettings, CreateTaskOutput>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOAuthCredentialsService _credentialsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskAction"/> class.
    /// </summary>
    public CreateTaskAction(
        ActionInfrastructure infrastructure,
        IHttpClientFactory httpClientFactory,
        IOAuthCredentialsService credentialsService)
        : base(infrastructure)
    {
        _httpClientFactory = httpClientFactory;
        _credentialsService = credentialsService;
    }

    /// <inheritdoc />
    public override async Task<ActionResult> ExecuteAsync(ActionContext context, CancellationToken cancellationToken)
    {
        var settings = context.GetSettings<CreateTaskSettings>();

        if (string.IsNullOrWhiteSpace(settings.ListId))
        {
            return ActionResult.Failed(new ArgumentException("List ID is required."), StepRunErrorCategory.Validation);
        }

        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            return ActionResult.Failed(new ArgumentException("Task Name is required."), StepRunErrorCategory.Validation);
        }

        if (!settings.DueDate.TryParseDate(out var dueDate))
        {
            return ActionResult.Failed(
                new FormatException($"Due Date '{settings.DueDate}' is not a valid date."),
                StepRunErrorCategory.Validation);
        }

        if (!settings.StartDate.TryParseDate(out var startDate))
        {
            return ActionResult.Failed(
                new FormatException($"Start Date '{settings.StartDate}' is not a valid date."),
                StepRunErrorCategory.Validation);
        }

        var request = new ClickUpCreateTaskRequest
        {
            Name = settings.Name,
            MarkdownContent = string.IsNullOrWhiteSpace(settings.Description) ? null : settings.Description,
            Assignees = settings.Assignees.SplitCsvInts(),
            Tags = settings.Tags.SplitCsv(),
            Status = string.IsNullOrWhiteSpace(settings.Status) ? null : settings.Status,
            Priority = settings.Priority,
            DueDate = dueDate?.ToUnixTimeMilliseconds(),
            DueDateTime = dueDate.HasValue ? dueDate.Value.TimeOfDay != TimeSpan.Zero : null,
            StartDate = startDate?.ToUnixTimeMilliseconds(),
            StartDateTime = startDate.HasValue ? startDate.Value.TimeOfDay != TimeSpan.Zero : null,
            TimeEstimate = settings.TimeEstimateMinutes.HasValue ? settings.TimeEstimateMinutes.Value * 60_000L : null,
            Parent = string.IsNullOrWhiteSpace(settings.ParentTaskId) ? null : settings.ParentTaskId,
            LinksTo = string.IsNullOrWhiteSpace(settings.LinksToTaskId) ? null : settings.LinksToTaskId,
            NotifyAll = settings.NotifyAll,
        };

        try
        {
            using var client = await _httpClientFactory.CreateClickUpClientAsync(
                context.Connection?.GetSettings<ClickUpConnectionSettings>(), _credentialsService, cancellationToken);

            using var httpResponse = await client.PostAsJsonAsync(
                $"list/{Uri.EscapeDataString(settings.ListId)}/task",
                request,
                cancellationToken);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.ToClickUpFailureAsync(cancellationToken);
            }

            var task = await httpResponse.Content.ReadFromJsonAsync<ClickUpTaskResponse>(cancellationToken);
            if (task is null)
            {
                return ActionResult.Failed(
                    new InvalidOperationException("ClickUp returned an empty response."),
                    StepRunErrorCategory.InvalidResponse);
            }

            return Success(new CreateTaskOutput
            {
                TaskId = task.Id,
                Name = task.Name,
                Url = task.Url,
                StatusName = task.Status?.Status,
                StatusType = task.Status?.Type,
                Priority = task.Priority?.Priority,
                CustomId = task.CustomId,
                ParentTaskId = task.Parent,
                TopLevelParentTaskId = task.TopLevelParent,
                DateCreated = task.DateCreated.ParseUnixMilliseconds(),
                CreatorId = task.Creator?.Id,
                CreatorUsername = task.Creator?.Username,
                ListName = task.List?.Name,
                SpaceId = task.Space?.Id,
                FolderId = task.Folder?.Id,
                ProjectId = task.Project?.Id,
            });
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (InvalidOperationException ex)
        {
            return ActionResult.Failed(ex, StepRunErrorCategory.Authentication);
        }
        catch (HttpRequestException ex)
        {
            return ActionResult.Failed(ex, StepRunErrorCategory.ServiceUnavailable);
        }
    }
}
