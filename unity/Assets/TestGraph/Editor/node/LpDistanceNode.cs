using UnityEditor.Experimental.GraphView;

[LpTitle("逻辑/坐标距离")]
public class LpDistanceNode : LpNode
{
    public LpDistanceNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "坐标距离";

        var input1Port = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port), "坐标1");
        inputContainer.Add(input1Port);

        var input2Port = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port), "坐标2");
        inputContainer.Add(input2Port);

        var output1Port = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "出口");
        outputContainer.Add(output1Port);
    }
}
