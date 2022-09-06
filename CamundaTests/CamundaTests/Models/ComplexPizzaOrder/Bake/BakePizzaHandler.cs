using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Bake;


[HandlerTopics("t_bake_pizza", LockDuration = 10_000)]
public class BakePizzaHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"minutes", new Variable(20, VariableType.Integer)}
            }
        };
    }
}