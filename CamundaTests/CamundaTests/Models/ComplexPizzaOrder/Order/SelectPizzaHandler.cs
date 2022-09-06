using Camunda.Worker;

namespace CamundaTests.Models.ComplexPizzaOrder.Order;


[HandlerTopics("t_select_pizza", LockDuration = 10_000)]
public class SelectPizzaHandler : IExternalTaskHandler
{
    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                {"name", new Variable("Custom pizza", VariableType.String)},
                {"type", new Variable("custom", VariableType.String)},
                {"price", new Variable(200, VariableType.Integer)}
            }
        };
    }
}