using UnityEditor.Experimental.GraphView;
 
public abstract class SampleNode : Node
{
    public Port inputPort;
    public Port outputPort;
    public SampleNode()
    {
        title = "Sample";
 
        inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);
 
        outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputContainer.Add(outputPort);
    }
}
