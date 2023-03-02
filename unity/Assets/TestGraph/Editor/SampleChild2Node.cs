
public class SampleChild2Node : SampleNode
{
    [SampleControl("全程朝目标")]
    public bool toTarget;
    
    public SampleChild2Node(SampleGraphView _graphView):base(_graphView)
    {
        title = "SampleChild2Node";
    }
}
