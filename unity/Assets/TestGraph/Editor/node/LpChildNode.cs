using UnityEditor.Experimental.GraphView;
 
[LpTitle("测试/节点1")]
public class LpChildNode : LpNode
{
    [LpControl("测试名字")]
    public string testName;
    public LpChildNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "LpChildNode";
    }
}
