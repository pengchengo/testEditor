[LpTitle("测试/节点2")]
public class LpChild2Node : LpNode
{
    [LpControl("全程朝目标")]
    public bool toTarget;
    
    public LpChild2Node(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "LpChild2Node";
    }
}
