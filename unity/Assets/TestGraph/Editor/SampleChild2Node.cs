
public class SampleChild2Node : SampleNode
{
    [SampleControl("全程朝目标")]
    public bool toTarget;
    
    public SampleChild2Node(SampleGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "SampleChild2Node";
    }
}
