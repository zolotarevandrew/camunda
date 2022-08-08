using Camunda.Worker;

namespace CamundaTests.Models.SimpleCondition;


[HandlerTopics("getdata", LockDuration = 10_000)]
[HandlerVariables("data")]
public class GetDataTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        var data = externalTask.Variables["data"].Value.ToString();
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["condition"] = Variable.String(data)
            }
        };
    }
}