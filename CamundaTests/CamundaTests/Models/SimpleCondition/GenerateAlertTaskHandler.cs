using Camunda.Worker;

namespace CamundaTests.Models.SimpleCondition;


[HandlerTopics("generatealert", LockDuration = 10_000)]
public class GenerateAlertTaskHandler : IExternalTaskHandler
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