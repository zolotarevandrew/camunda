using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallel;

[HandlerTopics("completetask", LockDuration = 10_000)]
public class CompleteTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        var variables = externalTask.Variables.Keys;
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}