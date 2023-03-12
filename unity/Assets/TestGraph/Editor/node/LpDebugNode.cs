using UnityEditor.Experimental.GraphView;

[LpTitle("行为/输出日志")]
public class LpDebugNode : LpNode
{
    [LpControl("内容")]
    public string content = "";
    public LpDebugNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "输出日志";

        var inputPort = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port), "入口");
        inputContainer.Add(inputPort);
    }
}
