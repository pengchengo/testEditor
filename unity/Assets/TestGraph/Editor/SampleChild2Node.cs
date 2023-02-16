using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class SampleChild2Node : SampleNode
{
    //[DefaultControl(Label = "全程朝目标")]
    public bool toTarget = true;
    public SampleChild2Node()
    {
        title = "SampleChild2Node";
    }
}
