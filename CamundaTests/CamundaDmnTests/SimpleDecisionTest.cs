using System.Diagnostics;
using net.adamec.lib.common.dmn.engine.engine.execution.context;
using net.adamec.lib.common.dmn.engine.parser;

namespace CamundaDmnTests;

public class SimpleDecisionTest
{
    private static DmnExecutionContext Context;
    static SimpleDecisionTest()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "dmn", "test.dmn");
        var fileContent = File.ReadAllText(filePath);
        var def = DmnParser.ParseString(fileContent, DmnParser.DmnVersionEnum.V1_3);
        Context = DmnExecutionContextFactory.CreateExecutionContext(def);
    }
    
    [Fact]
    public async Task Test()
    {
        var watch = Stopwatch.StartNew();
        
        Context.WithInputParameter("str", "test");
        Context.WithInputParameter("tes", false);
        var result = Context.ExecuteDecision("TestDecision");

        var a = result.First.Variables[0].Value as string;
        Assert.Equal("notok", a);
        watch.Stop();
        var b = watch.Elapsed;
    }
}