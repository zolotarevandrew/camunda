using Camunda.Worker;

namespace CamundaTests.Models.SimpleParallel;

[HandlerTopics("gettask", LockDuration = 10_000)]
//[HandlerVariables("task")]
public class GetDataTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var data = externalTask.Variables["task"].Value.ToString();
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                //["condition"] = Variable.String(data)
            }
        };
    }
}