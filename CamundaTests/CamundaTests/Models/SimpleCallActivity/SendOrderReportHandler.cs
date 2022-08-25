using Camunda.Worker;

namespace CamundaTests.Models.SimpleCallActivity;


[HandlerTopics("send_report", LockDuration = 10_000)]
public class SendOrderReportHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}