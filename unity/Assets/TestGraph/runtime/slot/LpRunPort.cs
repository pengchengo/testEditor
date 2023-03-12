using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpRunPort
{
    public LpRunNode node;
    public SlotData data;
    public int id;
    public List<LPRunEdge> inputEdgeList = new List<LPRunEdge>();
    public LPRunEdge outputEdge = null;
    public string name = "";
    public delegate T ValueHandler<T>();
    public ValueHandler<object> getter;
    //public Input
    public LpRunPort(LpRunNode n, SlotData d, int i){
        node = n;
        data = d;
        id = i;
        name = data.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flow(){
        this.outputEdge.targetNode.Enter(this.outputEdge);
    }
}
