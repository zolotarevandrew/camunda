using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.EventMessages;


[HandlerTopics("t_payed_order", LockDuration = 10_000)]
public class OrderPayedHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;

    public OrderPayedHandler(CamundaClient client)
    {
        _client = client;
    }
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        await _client.DeliverMessage(new ProcessInstanceDeliverMessage
        {
            InstanceId = new string [] {externalTask.ProcessInstanceId},
            MessageName = "order_payed"
        });
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}