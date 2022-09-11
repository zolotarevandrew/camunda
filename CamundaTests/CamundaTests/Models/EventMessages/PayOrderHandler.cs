using Camunda.Worker;

namespace CamundaTests.Models.EventMessages;


[HandlerTopics("t_pay_order", LockDuration = 10_000)]
public class PayOrderHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}