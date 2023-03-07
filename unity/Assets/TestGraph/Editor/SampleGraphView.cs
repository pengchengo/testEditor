using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SampleGraphView : GraphView
{
    static int nodeId = 1;
    SampleGraphEditorWindow window;
    string filePath = "";
    public SampleGraphView(SampleGraphEditorWindow _window, string path) : base()
    {
        Debug.Log("SampleGraphView path="+path);
        filePath = path;
        window = _window;
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new SelectionDragger());

        /*nodeCreationRequest += context =>
        {
            AddElement(new SampleNode());
        };*/

        var menuWindowProvider = ScriptableObject.CreateInstance<SampleSearchMenuWindowProvider>();
        menuWindowProvider.OnSelectEntryHandler = OnMenuSelectEntry;

        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), menuWindowProvider);
        };

        
        this.initFromData();

        //testGraphData();
    }

    Dictionary<int, SampleNode> initNodeMap = new Dictionary<int, SampleNode>();

    public void initFromData(){
        string json = File.ReadAllText(filePath);
        GraphData graphData = JsonUtility.FromJson<GraphData>(json);
        
        initNodeMap.Clear();

        foreach(var nodeInfo in graphData.nodes){
            Type type = Type.GetType(nodeInfo.type);
            SampleNode obj = Activator.CreateInstance(type, this, int.Parse(nodeInfo.id)) as SampleNode;
            SampleGraphView.nodeId = int.Parse(nodeInfo.id)+1;
            var posList = nodeInfo.pos.Split (',');
            var postion = obj.GetPosition();
            obj.SetPosition(new Rect(int.Parse(posList[0]), int.Parse(posList[1]), postion.width, postion.height));
            obj.initFromData(nodeInfo);
            initNodeMap.Add(obj.id, obj);
            AddElement(obj);
        }

        foreach(var edgeInfo in graphData.edges){
            SampleNode sourceNode = null;
            SamplePort sourcePort = null;
            if(edgeInfo.sourceNodeId != ""){
                sourceNode = initNodeMap[int.Parse(edgeInfo.sourceNodeId)];
                sourcePort = sourceNode.getPortById(int.Parse(edgeInfo.source));
            }
            SampleNode targetNode = null;
            SamplePort targetPort = null;
            if(edgeInfo.targetNodeId != ""){
                targetNode = initNodeMap[int.Parse(edgeInfo.targetNodeId)];
                targetPort = targetNode.getPortById(int.Parse(edgeInfo.target));
            }
            if(sourcePort != null && targetPort != null){
                var edgeView = new Edge
                {
                    input = sourcePort,
                    //userData = edge,
                    output = targetPort
                };
                edgeView.output.Connect(edgeView);
                edgeView.input.Connect(edgeView);
                AddElement(edgeView);
            }
            
        }
    }

    public void testCreate(){
        var node1 = new SampleChildNode(this, SampleGraphView.nodeId++);
        var node2 = new SampleChild2Node(this, SampleGraphView.nodeId++);
        var propertyList = node1.GetType().GetProperties();
        foreach(var propertyInfo in propertyList){
            foreach(var attribute in propertyInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                //Debug.Log("node1 propertyInfo.Name="+propertyInfo.Name);
                //Debug.Log("node1 propertyInfo.GetValue(node)="+propertyInfo.GetValue(node1));
            }
        }
        foreach (var fieldInfo in node1.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
            foreach(var attribute in fieldInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                var propertyType = fieldInfo.FieldType;
                if(propertyType == typeof(string)){
                    fieldInfo.SetValue(node1, "peter");
                }
                //Debug.Log("node1 fieldInfo.Name="+fieldInfo.Name);
                //Debug.Log("node1 fieldInfo.GetValue(node)="+fieldInfo.GetValue(node1));
            }
        }
        node1.initFromData(new NodeData());
        node2.initFromData(new NodeData());
        var position = new Vector2(node2.GetPosition().x + 200, node2.GetPosition().y);
        node2.SetPosition(new Rect(position, node2.GetPosition().size));
        AddElement(node1);
        AddElement(node2);

        var node1OutPut = node1.outputPort;
        var node2InPut = node2.inputPort;
        var edgeView = new Edge
        {
            //userData = edge,
            output = node2InPut,
            input = node1OutPut
        };
        edgeView.output.Connect(edgeView);
		edgeView.input.Connect(edgeView);
        AddElement(edgeView);
    }

    public void testGraphData(){
        var graphData = new GraphData();
        var nodeData = new NodeData();
        nodeData.type = "nodeTestType";
        graphData.nodes.Add(nodeData);
        var edgeData = new EdgeData();
        edgeData.source = "testSource";
        edgeData.sourceNodeId = "testsourceNodeId";
        edgeData.target = "testtarget";
        edgeData.targetNodeId = "testtargetNodeId";
        graphData.edges.Add(edgeData);
        var propertyData = new Property();
        propertyData.name = "testName";
        propertyData.value = "1";
        graphData.properties.Add(propertyData);
        var result = JsonUtility.ToJson(graphData);
        Debug.Log("testGraphData=");

        var testData = JsonUtility.FromJson<GraphData>(result);
        Debug.Log(testData.nodes.Count);
    }

    public void Save(){
        Debug.Log("SampleGraphView Save");
        var graphData = new GraphData();
        int num = 0;
        foreach(var node in this.nodes){
            num++;
            var nodeData = new NodeData();
            nodeData.id = (node as SampleNode).id.ToString();
            nodeData.type = node.GetType().ToString();
            nodeData.pos = node.GetPosition().x.ToString()+","+node.GetPosition().y.ToString();
            var propertyList = node.GetType().GetProperties();
            foreach(var propertyInfo in propertyList){
                foreach(var attribute in propertyInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                    //Debug.Log("propertyInfo.Name="+propertyInfo.Name);
                    //Debug.Log("propertyInfo.GetValue(node)="+propertyInfo.GetValue(node));
                    var propertyData = new Property();
                    propertyData.name = propertyInfo.Name;
                    propertyData.type = ObjectUtils.GetObjType(propertyInfo.GetValue(node));
                    propertyData.value = propertyInfo.GetValue(node).ToString();
                    nodeData.variables.Add(propertyData);
                }
            }
            foreach (var fieldInfo in node.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
                foreach(var attribute in fieldInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                    var propertyType = fieldInfo.FieldType;
                    var propertyData = new Property();
                    propertyData.name = fieldInfo.Name;
                    propertyData.type = ObjectUtils.GetObjType(fieldInfo.GetValue(node));
                    propertyData.value = fieldInfo.GetValue(node).ToString();
                    nodeData.variables.Add(propertyData);
                    //Debug.Log("fieldInfo.Name="+fieldInfo.Name);
                    //Debug.Log("fieldInfo.GetValue(node)="+fieldInfo.GetValue(node));
                }
            }
            graphData.nodes.Add(nodeData);
        }

        foreach(var edge in this.edges){
            var edgeData = new EdgeData();
            var inputPort = edge.input as SamplePort;
            var inputNode = inputPort.node as SampleNode;
            Debug.Log("inputPort.id ="+ inputPort.id);
            if(inputNode != null){
                //Debug.Log("sampleNode.inputNode id= "+inputNode.id);
                edgeData.source = inputPort.id.ToString();
                edgeData.sourceNodeId = inputNode.id.ToString();
            }else{
                //Debug.Log("sampleNode.inputNode = null");
                edgeData.source = "0";
                edgeData.sourceNodeId = "0";
            }
            var outputPort = edge.output as SamplePort;
            var outputNode = outputPort.node as SampleNode;
            Debug.Log("outputPort.id ="+ outputPort.id);
            if(outputNode != null){
                Debug.Log("sampleNode.outputNode id= "+outputNode.id);
                edgeData.target = outputPort.id.ToString();
                edgeData.targetNodeId = outputNode.id.ToString();
            }else{
                Debug.Log("sampleNode.outputNode = null");
                edgeData.target = "0";
                edgeData.targetNodeId = "0";
            }
            graphData.edges.Add(edgeData);
        }
        string result = JsonUtility.ToJson(graphData);
        
        //string FileUrl = Application.dataPath + "/TestGraph/sampleData.txt";
        Debug.Log("SampleGraphView Save filePath="+filePath);
        File.WriteAllText(filePath, result);
    }

    public void testToJson(){
        Debug.Log("testToJson start");
        int num = 0;
        foreach(var node in this.nodes){
            num++;
            var nodeData = new NodeData();
            var propertyList = node.GetType().GetProperties();
            foreach(var propertyInfo in propertyList){
                foreach(var attribute in propertyInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                    //Debug.Log("propertyInfo.Name="+propertyInfo.Name);
                    //Debug.Log("propertyInfo.GetValue(node)="+propertyInfo.GetValue(node));
                }
            }
            foreach (var fieldInfo in node.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)){
                foreach(var attribute in fieldInfo.GetCustomAttributes(typeof(SampleBaseControlAttribute), false)){
                    var propertyType = fieldInfo.FieldType;
                    //Debug.Log("fieldInfo.Name="+fieldInfo.Name);
                    //Debug.Log("fieldInfo.GetValue(node)="+fieldInfo.GetValue(node));
                }
            }
        }
        Debug.Log("testToJson end num="+num);
    }

    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
    {
        return ports.ToList();
    }

    private bool OnMenuSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as Type;
        var windowRoot = window.rootVisualElement;
        var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, context.screenMousePosition - window.position.position);
        var graphMousePosition = contentViewContainer.WorldToLocal(windowMousePosition);
        CreateNode(type, graphMousePosition);

        return true;
    }

    private void CreateNode(Type type, Vector2 pos = default)
    {
        SampleChildNode nodeView = new SampleChildNode(this, SampleGraphView.nodeId++);
        nodeView.SetPosition(new Rect(pos, nodeView.GetPosition().size));
        this.AddElement(nodeView);
    }
}