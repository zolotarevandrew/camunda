using Camunda.Worker;

namespace CamundaTests.Models.BookingFlowV2;


[HandlerTopics("e_charge_money", LockDuration = 10_000)]
public class ChargeMoneyHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return new BpmnErrorResult("12", "2313", new Dictionary<string, Variable>());
    }
}