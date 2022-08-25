using Camunda.Worker;

namespace CamundaTests.Models.SimpleCallActivity;


[HandlerTopics("receive_order", LockDuration = 10_000)]
public class ReceiveOrderHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(50000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"order_result", new Variable("processed", VariableType.String)}
            }
        };
    }
}