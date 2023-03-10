using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.Port;

public class LpPort:Port
{
    static int staticId = 1;
    public int id;

    public LpPort(LpNode node, Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type, string name): base(portOrientation, portDirection, portCapacity, type)
    {
        id = node.portId;
        node.portId = node.portId + 1;
        var connectorListener = new LpEdgeConnectorListener();
        m_EdgeConnector = new EdgeConnector<Edge>(connectorListener);
        this.AddManipulator(this.m_EdgeConnector);
        node.portList.Add(this);
        portName = name;
    }
}
