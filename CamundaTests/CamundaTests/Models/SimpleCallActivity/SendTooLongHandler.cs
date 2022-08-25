using Camunda.Worker;

namespace CamundaTests.Models.SimpleCallActivity;


[HandlerTopics("send_too_long", LockDuration = 10_000)]
public class SendTooLongHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"order_result", new Variable("processed", VariableType.String)}
            }
        };
    }
}