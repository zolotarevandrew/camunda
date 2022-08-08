using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallelMultiple;

[HandlerTopics("pgettask", LockDuration = 10_000)]
//[HandlerVariables("task")]
public class ParallelGetDataTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var data = externalTask.Variables["task"].Value.ToString();
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["task1Collection"] = Variable.String(data)
            }
        };
    }
}