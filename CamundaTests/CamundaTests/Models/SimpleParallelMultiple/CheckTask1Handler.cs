using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallelMultiple;

[HandlerTopics("pchecktask1", LockDuration = 10_000)]
public class ParallelCheckTask1Handler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["task1_result"] = Variable.String("task1_res")
            }
        };
    }
}