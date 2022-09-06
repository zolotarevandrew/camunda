using Camunda.Api.Client;
using Camunda.Worker;
using VariableType = Camunda.Worker.VariableType;

namespace CamundaTests.Models.ComplexPizzaOrder.Order;


[HandlerTopics("t_pay_pizza", LockDuration = 10_000)]
public class PayPizzaHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;
    public PayPizzaHandler(CamundaClient client)
    {
        _client = client;
    }
    
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        await _client.DeliverMessage(new ProcessInstanceDeliverMessage
        {
            MessageName = "payment_received",
            InstanceId = new string[]
            {
                externalTask.ProcessInstanceId
            }
        });
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"payed", new Variable(25, VariableType.Integer)},
                {"type", new Variable("type", VariableType.String)}
            }
        };
    }
}