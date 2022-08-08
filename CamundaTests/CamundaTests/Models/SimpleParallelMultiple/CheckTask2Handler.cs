using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallelMultiple;

[HandlerTopics("pchecktask2", LockDuration = 10_000)]
public class ParallelCheckTask2Handler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["task2_result"] = Variable.String("task2_res")
            }
        };
    }
}