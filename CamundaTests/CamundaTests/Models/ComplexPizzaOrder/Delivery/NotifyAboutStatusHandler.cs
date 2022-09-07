using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Delivery;


[HandlerTopics("t_notify_pizza_status", LockDuration = 10_000)]
public class NotifyAboutStatusHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;
    public NotifyAboutStatusHandler(CamundaClient client)
    {
        _client = client;
    }
    
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);

        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}