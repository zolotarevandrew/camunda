using Camunda.Worker;
using Newtonsoft.Json;

namespace CamundaTests;

public class CollectionVariable : Variable
{
    public Dictionary<string, object> ValueInfo { get; private set; }
    
    [JsonConstructor]
    public CollectionVariable(object? value) : base(JsonConvert.SerializeObject(value, Formatting.None), VariableType.Object)
    {
        ValueInfo = new Dictionary<string, object>()
        {
            ["serializationDataFormat"] = (object) "application/json",
            ["objectTypeName"] = (object) "java.lang.Object"
        };
    }
}