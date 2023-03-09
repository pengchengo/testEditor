using UnityEditor.Experimental.GraphView;

[LpTitle("逻辑/比大小")]
public class LpCompareNode : LpNode
{
    [LpControl("比大小")]
    public string testName;
    public LpCompareNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "LpCompareNode";
    }
}
