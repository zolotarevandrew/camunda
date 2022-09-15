using Camunda.Worker;

namespace CamundaTests.Models.TimerEvents;


[HandlerTopics("e_notify_not_booked", LockDuration = 10_000)]
public class TimerTaskNotifyHandler : IExternalTaskHandler
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