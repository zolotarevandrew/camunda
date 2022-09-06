using Camunda.Api.Client;
using Camunda.Worker;
using VariableType = Camunda.Worker.VariableType;

namespace CamundaTests.Models.ComplexPizzaOrder.Delivery;


[HandlerTopics("t_deliver_pizza", LockDuration = 10_000)]
public class DeliverPizzaHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;
    public DeliverPizzaHandler(CamundaClient client)
    {
        _client = client;
    }
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        await _client.DeliverMessage(new ProcessInstanceDeliverMessage
        {
            MessageName = "pizza_received",
            InstanceId = new string[]
            {
                externalTask.ProcessInstanceId
            }
        });
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"delivery_minutes", new Variable(20, VariableType.Integer)}
            }
        };
    }
}