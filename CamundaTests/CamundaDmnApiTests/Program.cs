using net.adamec.lib.common.dmn.engine.engine.execution.context;
using net.adamec.lib.common.dmn.engine.parser;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var filePath = @"C:\Projects\github\camunda\CamundaTests\CamundaDmnApiTests\bin\Debug\net7.0\dmn\stepv1.dmn";
var fileContent = File.ReadAllText(filePath);
var def = DmnParser.ParseString(fileContent, DmnParser.DmnVersionEnum.V1_3);
DmnExecutionContext context = DmnExecutionContextFactory.CreateExecutionContext(def);

app.MapGet("/test", () =>
{
    context.WithInputParameter("step", "RegNumber");
    context.WithInputParameter("tes", new
    {
        
    });
    var result = context.ExecuteDecision("TestDecision");

    return result.First.Variables[0].Value as string;
});

app.Run();