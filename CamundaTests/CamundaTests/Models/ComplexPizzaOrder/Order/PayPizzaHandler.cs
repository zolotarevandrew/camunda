using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Order;


[HandlerTopics("t_pay_pizza", LockDuration = 10_000)]
public class PayPizzaHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        
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