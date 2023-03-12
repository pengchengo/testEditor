using UnityEditor.Experimental.GraphView;
using UnityEngine;

[LpTitle("数据/坐标")]
public class LpVector3Node : LpNode
{
    [LpControl("数值")]
    public Vector3 value;
    public LpVector3Node(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "坐标";

        var outPort = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "出口");
        outputContainer.Add(outPort);
    }
}
