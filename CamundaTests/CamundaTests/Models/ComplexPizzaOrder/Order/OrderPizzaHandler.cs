using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Order;


[HandlerTopics("t_order_pizza", LockDuration = 10_000)]
public class OrderPizzaHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"address", new Variable("Test address", VariableType.String)}
            }
        };
    }
}