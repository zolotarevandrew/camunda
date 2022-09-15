using Camunda.Worker;

namespace CamundaTests.Models.BoundaryEvents;


[HandlerTopics("e_cancel_task", LockDuration = 10_000)]
public class CancelBoundaryTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}