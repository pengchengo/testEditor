using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LpRunRoot : MonoBehaviour
{
    LpRunNode startNode = null;
    Dictionary<int, LpRunNode> initNodeMap = new Dictionary<int, LpRunNode>();
    List<LPRunEdge> edgeList = new List<LPRunEdge>();
    // Start is called before the first frame update
    void Start()
    {
        initFromData();
        run();
    }

    public void initFromData(){
        string filePath = "Assets/TestGraph/sample.graph";
        string json = File.ReadAllText(filePath);
        GraphData graphData = JsonUtility.FromJson<GraphData>(json);
        
        initNodeMap.Clear();
        foreach(var nodeInfo in graphData.nodes){
            Type type = Type.GetType(nodeInfo.type+"RT");
            LpRunNode obj = Activator.CreateInstance(type) as LpRunNode;
            obj.init(nodeInfo);
            obj.afterInit();
            var nodeId = int.Parse(nodeInfo.id);
            //obj.initFromData(nodeInfo);
            initNodeMap.Add(nodeId, obj);
            if(nodeInfo.type == "LpStartNode"){
                this.startNode = obj;
            }
        }
        foreach(var edgeInfo in graphData.edges){
            LpRunNode sourceNode = null;
            LpRunPort sourcePort = null;
            if(edgeInfo.targetNodeId != ""){
                sourceNode = initNodeMap[int.Parse(edgeInfo.targetNodeId)];
                sourcePort = sourceNode.getPortById(int.Parse(edgeInfo.target));
            }
            LpRunNode targetNode = null;
            LpRunPort targetPort = null;
            if(edgeInfo.sourceNodeId != ""){
                targetNode = initNodeMap[int.Parse(edgeInfo.sourceNodeId)];
                targetPort = targetNode.getPortById(int.Parse(edgeInfo.source));
            }
            var runEdge = new LPRunEdge();
            runEdge.sourceNode = sourceNode;
            runEdge.sourcePort = sourcePort;
            runEdge.targetNode = targetNode;
            runEdge.targetPort = targetPort;
            if(sourcePort != null){
                sourcePort.outputEdge = runEdge;
            }
            if(targetPort != null){
                targetPort.inputEdge = runEdge;
            }
            this.edgeList.Add(runEdge);
        }
    }
    
    public void run(){
        this.startNode.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
