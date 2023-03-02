using UnityEditor.Experimental.GraphView;
 
public class SampleChildNode : SampleNode
{
    [SampleControl("测试名字")]
    public string testName;
    public SampleChildNode(SampleGraphView _graphView):base(_graphView)
    {
        title = "SampleChildNode";
    }
}
