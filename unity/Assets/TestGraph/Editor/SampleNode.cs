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

        var propertyList = this.GetType().GetProperties();
        Debug.Log("property=");
        foreach(var propertyInfo in propertyList){
            foreach(var attribute in propertyInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                var viewCont = new VisualElement();
                viewCont.AddToClassList("ControlField");
                viewCont.Add(new Label(propertyInfo.Name) {name = propertyInfo.Name});
                viewCont.Add(AddControl(this, new Toggle() {name = "testFiend"}, propertyInfo));
                contents.Add(viewCont);
                Debug.Log(propertyInfo.Name);
            }
        }
        Debug.Log("property end");
        foreach (var fieldInfo in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
            foreach(var attribute in fieldInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                var viewCont = new VisualElement();
                viewCont.AddToClassList("ControlField");
                viewCont.Add(new Label(fieldInfo.Name) {name = fieldInfo.Name});
                viewCont.Add(AddControl(this, new Toggle() {name = "testFiend"}, fieldInfo));
                contents.Add(viewCont);
                Debug.Log(fieldInfo.Name);
            }
        }
        Debug.Log("field end");
    }

    private BaseField<T> AddControl<T>(SampleNode node, BaseField<T> field, PropertyInfo property)
    {
        field.value = (T) property.GetValue(node);
        field.RegisterValueChangedCallback(e =>
        {
            //node.owner.owner.RegisterCompleteObjectUndo(typeof(T).Name + " Change");
            property.SetValue(node, e.newValue);
            //node.Dirty(ModificationScope.Node);
        });
        return field;
    }

    private BaseField<T> AddControl<T>(SampleNode node, BaseField<T> field, FieldInfo property)
    {
        field.value = (T) property.GetValue(node);
        field.RegisterValueChangedCallback(e =>
        {
            //node.owner.owner.RegisterCompleteObjectUndo(typeof(T).Name + " Change");
            property.SetValue(node, e.newValue);
            //node.Dirty(ModificationScope.Node);
        });
        return field;
    }
}
