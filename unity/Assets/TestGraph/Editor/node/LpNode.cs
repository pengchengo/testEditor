using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class LpNode : Node
{
    public int id;
    public int portId;
    public bool testBool{ get; set; } = true;
    
    public LpGraphView graphView;

    public List<LpPort> portList = new List<LpPort>();
    public LpNode(LpGraphView _graphView, int nodeId)
    {
        graphView = _graphView;
        id = nodeId;
        title = "Lp";
        portId = 1;
        testBool = true;

        var contents = this.Q("contents");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/TestGraph/Editor/Resources/styles/LpView.uss");
        //Debug.Log("styleSheet=");
        //Debug.Log(styleSheet);
        this.styleSheets.Add(styleSheet);
        //contents.AddStyleSheetPath("styles/LpView");

        //inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
    }

    public LpPort getPortById(int id){
        foreach(var port in portList){
            if(port.id == id){
                return port;
            }
        }
        return null;
    }

    public Property getValueFromNodeData(NodeData nodeData, string name){
        foreach(var propertyInfo in nodeData.variables){
            if(propertyInfo.name == name){
                return propertyInfo;
            }
        }
        return null;
    }

    public void initFromData(NodeData nodeData){
        var contents = this.Q("contents");
        var propertyList = this.GetType().GetProperties();
        //Debug.Log("property=");
        foreach(var propertyInfo in propertyList){
            foreach(LpControlAttribute attribute in propertyInfo.GetCustomAttributes(typeof(LpBaseControlAttribute), false)){
                var viewCont = new VisualElement();
                Property data = this.getValueFromNodeData(nodeData, propertyInfo.Name);
                if(data != null){
                    propertyInfo.SetValue(this, ObjectUtils.GetObjValue(data));
                }
                viewCont.AddToClassList("ControlField");
                viewCont.Add(new Label(attribute.name) {name = attribute.name});
                var propertyType = propertyInfo.PropertyType;
                if (propertyType == typeof(bool))
                    viewCont.Add(AddControl(this, new Toggle() {name = propertyInfo.Name}, propertyInfo));
                else if (propertyType == typeof(string))
                    viewCont.Add(AddControl(this, new TextField() {name = propertyInfo.Name}, propertyInfo));
                contents.Add(viewCont);
                //Debug.Log(propertyInfo.Name);
            }
        }
        //Debug.Log("property end");
        foreach (var fieldInfo in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
            foreach(LpControlAttribute attribute in fieldInfo.GetCustomAttributes(typeof(LpBaseControlAttribute), false)){
                var viewCont = new VisualElement();
                viewCont.AddToClassList("ControlField");
                Property data = this.getValueFromNodeData(nodeData, fieldInfo.Name);
                if(data != null){
                    fieldInfo.SetValue(this, ObjectUtils.GetObjValue(data));
                }
                viewCont.Add(new Label(attribute.name) {name = attribute.name});

                var propertyType = fieldInfo.FieldType;
                if (propertyType == typeof(bool))
                    viewCont.Add(AddControl(this, new Toggle() {name = fieldInfo.Name}, fieldInfo));
                else if (propertyType == typeof(string))
                    viewCont.Add(AddControl(this, new TextField() {name = fieldInfo.Name}, fieldInfo));
                contents.Add(viewCont);
                //Debug.Log(fieldInfo.Name);
            }
        }
        //Debug.Log("field end");
    }

    private BaseField<T> AddControl<T>(LpNode node, BaseField<T> field, PropertyInfo property)
    {
        field.value = (T) property.GetValue(node);
        field.RegisterValueChangedCallback(e =>
        {
            Debug.Log("AddControl RegisterValueChangedCallback 1");
            //node.owner.owner.RegisterCompleteObjectUndo(typeof(T).Name + " Change");
            property.SetValue(node, e.newValue);
            //node.Dirty(ModificationScope.Node);
        });
        return field;
    }

    private BaseField<T> AddControl<T>(LpNode node, BaseField<T> field, FieldInfo property)
    {
        field.value = (T) property.GetValue(node);
        field.RegisterValueChangedCallback(e =>
        {
            Debug.Log("AddControl RegisterValueChangedCallback 2");
            //node.owner.owner.RegisterCompleteObjectUndo(typeof(T).Name + " Change");
            property.SetValue(node, e.newValue);
            //node.Dirty(ModificationScope.Node);
            graphView.testToJson();
        });
        return field;
    }
}
