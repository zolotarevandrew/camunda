using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Delivery;


[HandlerTopics("t_payment_received", LockDuration = 10_000)]
public class PaymentReceivedHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(50000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"merchant", new Variable("new_merchant", VariableType.String)}
            }
        };
    }
}