using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Delivery;


[HandlerTopics("t_deliver_pizza", LockDuration = 10_000)]
public class DeliverPizzaHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"delivery_minutes", new Variable(20, VariableType.Integer)}
            }
        };
    }
}