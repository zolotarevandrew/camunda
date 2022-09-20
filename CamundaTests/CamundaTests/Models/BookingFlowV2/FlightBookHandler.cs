using Camunda.Worker;

namespace CamundaTests.Models.BookingFlowV2;


[HandlerTopics("e_flight_booking", LockDuration = 10_000)]
public class V2FlightBookHandler : IExternalTaskHandler
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