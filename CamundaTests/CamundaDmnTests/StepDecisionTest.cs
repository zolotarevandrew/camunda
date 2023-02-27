using net.adamec.lib.common.dmn.engine.engine.execution.context;
using net.adamec.lib.common.dmn.engine.parser;

namespace CamundaDmnTests;

public class StepDecisionTest
{
    private static DmnExecutionContext Context;
    static StepDecisionTest()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "dmn", "stepv1.dmn");
        var fileContent = File.ReadAllText(filePath);
        var def = DmnParser.ParseString(fileContent, DmnParser.DmnVersionEnum.V1_3ext);
        Context = DmnExecutionContextFactory.CreateExecutionContext(def);
    }
    
    [Fact]
    public void Test()
    {
        Context.WithInputParameter("step", "RegNumber");
        var result = Context.ExecuteDecision("StepDecision");

        var calcDecision = result.First.Variables[0].Value as string;
        Assert.Equal("RegNumberNext", calcDecision);

        Context.Reset();
        
                    
        Context.WithInputParameter("metadata", new 
        {
            ownerDenied = false,
            uploadRegDocsMissing = false,
            bizDetailsMissing = false,
            bizAddressMissing = false
        });
        
        var stepRes = Context.ExecuteDecision(calcDecision);
        var next = stepRes.First.Variables[0].Value;
    }
}