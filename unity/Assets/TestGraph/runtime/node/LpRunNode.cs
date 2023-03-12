using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpRunNode
{
    public NodeData data = null;
    public int id = 0;
    public List<LpRunPort> portList = new List<LpRunPort>();
    public LPRunEdge enterEdge;
    public LpRunNode(){
        
    }
    public void init(NodeData v){
        data = v;
        id = int.Parse(data.id);
        int portId = 1;
        foreach (var slot in data.slots)
        {
            portList.Add(new LpRunPort(this, slot, portId++));
        }
    }
    public T getPropertyValue<T>(string name){
        foreach (var property in data.variables)
        {   
            if(property.name == name){
                T value = (T)ObjectUtils.GetObjValue(property);
                return value;
            }
        }
        Debug.LogError(this.GetType().Name+" 没找到属性:"+name);
        object error = "";
        return (T)error;
    }

    public LpRunPort getPortByName(string name){
        foreach(var port in portList){
            if(port.name == name){
                return port;
            }
        }
        return null;
    }

    public T getPortValue<T>(string name){
        LpRunPort port = this.getPortByName(name);
        //port.inputEdge
        object error = "";
        return (T)error; 
    }

    public LpRunPort getPortById(int id){
        foreach(var port in portList){
            if(port.id == id){
                return port;
            }
        }
        return null;
    }

    public virtual void afterInit(){

    }

    public virtual void Enter(LPRunEdge edge = null){
        this.enterEdge = edge;
    }

    public void FlowFirst(){
        foreach(var port in portList){
            if(port.outputEdge != null){
                port.outputEdge.targetNode.Enter(port.outputEdge);
                return;
            }
        }
    }

    public object getInputValue(LpRunPort port){
        if(port.inputEdgeList.Count <= 1 || this.enterEdge == null){
            return port.inputEdgeList[0].sourcePort.getter();
        }
        LPRunEdge selectEdge = port.inputEdgeList[0];
        foreach(var inputEdge in port.inputEdgeList){
            if(inputEdge == this.enterEdge){
                selectEdge = this.enterEdge;
                break;
            }
        }
        return selectEdge.sourcePort.getter();
    }
}
