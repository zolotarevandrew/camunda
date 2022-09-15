using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.BoundaryEvents;


[HandlerTopics("e_attached2", LockDuration = 10_000)]
public class BoundaryTask2Handler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {

        await Task.Delay(10000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}