using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.BoundaryEvents;


[HandlerTopics("e_attached", LockDuration = 10_000)]
public class BoundaryTaskHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;
    public BoundaryTaskHandler(CamundaClient client)
    {
        _client = client;
    }
    
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await _client.DeliverMessage(new ProcessInstanceDeliverMessage
        {
            InstanceId = new string[]
            {
                externalTask.ProcessInstanceId
            },
            MessageName = "cancelled"
        });
        
        await Task.Delay(10000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}