using Camunda.Worker;

namespace CamundaTests.Models.SimpleEventGateway;

[HandlerTopics("porder", LockDuration = 10_000)]
public class ProcessOrderHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["res"] = Variable.String("res")
            }
        };
    }
}