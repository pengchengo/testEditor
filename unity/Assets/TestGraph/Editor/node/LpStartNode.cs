using UnityEditor.Experimental.GraphView;
 
[LpTitle("逻辑/开始节点")]
public class LpStartNode : LpNode
{
    public LpStartNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "开始节点";

        var outputPort = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "出口");
        outputContainer.Add(outputPort);
    }
}
