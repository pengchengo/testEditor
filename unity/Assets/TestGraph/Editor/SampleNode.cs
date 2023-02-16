using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SampleNode : Node
{
    public bool testBool{ get; set; } = true;
    public Port inputPort;
    public Port outputPort;
    public SampleNode()
    {
        title = "Sample";

        testBool = true;

        var contents = this.Q("contents");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/TestGraph/Editor/Resources/styles/SampleView.uss");
        Debug.Log("styleSheet=");
        Debug.Log(styleSheet);
        this.styleSheets.Add(styleSheet);
        //contents.AddStyleSheetPath("styles/SampleView");

        inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);
 
        outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        outputContainer.Add(outputPort);

        var viewCont = new VisualElement();
        viewCont.AddToClassList("ControlField");
        viewCont.Add(new Label("LableName1") {name = "LableName2"});

        var propertyList = this.GetType().GetProperties();
        Debug.Log("property=");
        var property = this.GetType().GetProperty("testBool");
        foreach(var PropertyInfo in propertyList){
            //Debug.Log(PropertyInfo.Name);
        }
        
        viewCont.Add(AddControl(this, new Toggle() {name = "testFiend"}, property));
        contents.Add(viewCont);
    }

    private BaseField<T> AddControl<T>(SampleNode node, BaseField<T> field, PropertyInfo property)
    {
        field.value = (T) property.GetValue(node);
        /*field.OnValueChanged(e =>
        {
            node.owner.owner.RegisterCompleteObjectUndo(typeof(T).Name + " Change");
            property.SetValue(node, e.newValue);
            node.Dirty(ModificationScope.Node);
        });*/
        return field;
    }
}
