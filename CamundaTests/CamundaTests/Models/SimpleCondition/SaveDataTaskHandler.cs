using Camunda.Worker;

namespace CamundaTests.Models.SimpleCondition;


[HandlerTopics("savedata", LockDuration = 10_000)]
public class SaveDataTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>()
        };
    }
}