using Camunda.Worker;

namespace CamundaTests.Models.SimpleTask;

[HandlerTopics("GetCity", LockDuration = 10_000)]
[HandlerVariables("city")]
public class GetCityTaskHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var city = externalTask.Variables["city"].Value.ToString();

        await Task.Delay(1000);

        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["MESSAGE"] = Variable.String("Hello, Guest!")
            }
        };
    }
}