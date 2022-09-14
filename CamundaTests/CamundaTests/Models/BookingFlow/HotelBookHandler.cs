using Camunda.Api.Client;
using Camunda.Worker;

namespace CamundaTests.Models.BookingFlow;


[HandlerTopics("ee_hotel_booking", LockDuration = 10_000)]
public class HotelBookHandler : IExternalTaskHandler
{
    private readonly CamundaClient _client;

    public HotelBookHandler(CamundaClient client)
    {
        _client = client;
    }
    
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            await _client.DeliverMessage(new ProcessInstanceDeliverMessage
            {
                InstanceId = new string[]
                {
                    externalTask.ProcessInstanceId
                },
                MessageName = "invalid_booking"
                    //"employee_checked"
            });
        });

        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                
            }
        };
    }
}