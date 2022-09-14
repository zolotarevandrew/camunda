using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.BookingFlow;


[HandlerTopics("ee_cancel_booking", LockDuration = 10_000)]
public class CancelBookingHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;

    public CancelBookingHandler(CamundaClient client)
    {
        _client = client;
    }
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        await _client.DeliverMessage(new ProcessInstanceDeliverMessage
        {
            InstanceId = new string[]
            {
                externalTask.ProcessInstanceId
            },
            MessageName = "employee_checked"
            //"employee_checked"
        });
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}