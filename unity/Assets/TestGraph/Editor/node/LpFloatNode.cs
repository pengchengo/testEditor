using UnityEditor.Experimental.GraphView;

[LpTitle("数据/浮点")]
public class LpFloatNode : LpNode
{
    [LpControl("数值")]
    public float value = 0.0f;
    public LpFloatNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "浮点";

        var outPort = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "出口");
        outputContainer.Add(outPort);
    }
}
