using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SampleGraphView : GraphView
{
    SampleGraphEditorWindow window;
    public SampleGraphView(SampleGraphEditorWindow _window) : base()
    {
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

        var node1 = new SampleChildNode();
        var node2 = new SampleChild2Node();
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


        testGraphData();
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
        propertyData.value = 1;
        graphData.properties.Add(propertyData);
        var result = JsonUtility.ToJson(graphData);
        Debug.Log("testGraphData=");

        var testData = JsonUtility.FromJson<GraphData>(result);
        Debug.Log(testData.nodes.Count);
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
        SampleChildNode nodeView = new SampleChildNode();
        nodeView.SetPosition(new Rect(pos, nodeView.GetPosition().size));
        this.AddElement(nodeView);
    }
}