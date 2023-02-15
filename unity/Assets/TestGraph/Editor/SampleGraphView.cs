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