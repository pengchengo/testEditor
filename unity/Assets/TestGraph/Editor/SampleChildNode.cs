using UnityEditor.Experimental.GraphView;
 
public class SampleChildNode : SampleNode
{
    [SampleControl("测试名字")]
    public string testName;
    public SampleChildNode(SampleGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "SampleChildNode";
    }
}
