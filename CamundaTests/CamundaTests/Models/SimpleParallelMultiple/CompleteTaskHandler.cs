using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallelMultiple;

[HandlerTopics("pcompletetask", LockDuration = 10_000)]
public class ParallelCompleteTaskHandler : IExternalTaskHandler
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