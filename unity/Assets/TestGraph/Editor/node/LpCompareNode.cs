using UnityEditor.Experimental.GraphView;

[LpTitle("逻辑/比大小")]
public class LpCompareNode : LpNode
{
    [LpControl("是否包含等于")]
    public bool isEqual = false;
    public LpCompareNode(LpGraphView _graphView, int nodeId):base(_graphView, nodeId)
    {
        title = "比大小";

        var inputPort = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port), "入口");
        inputContainer.Add(inputPort);

        var input1Port = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port), "参数1");
        inputContainer.Add(input1Port);

        var input2Port = new LpPort(this, Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port), "参数2");
        inputContainer.Add(input2Port);
 
        //outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        var output1Port = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "大于");
        outputContainer.Add(output1Port);

        var output2Port = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "等于");
        outputContainer.Add(output2Port);

        var output3Port = new LpPort(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port), "小于");
        outputContainer.Add(output3Port);
    }
}
