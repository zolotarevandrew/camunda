using Camunda.Worker;

namespace CamundaTests.Models.TimerEvents;


[HandlerTopics("e_book_table", LockDuration = 10_000)]
public class TimerTaskHandler : IExternalTaskHandler
{

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(50000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}