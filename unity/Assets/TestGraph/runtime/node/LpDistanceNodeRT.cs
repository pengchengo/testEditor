using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpDistanceNodeRT : LpRunNode
{
    LpRunPort input1;
    LpRunPort input2;
    public override void afterInit(){
        input1 = this.getPortByName("坐标1");
        input2 = this.getPortByName("坐标2");
        this.getPortByName("出口").getter = getDistance;
    }
    
    object getDistance(){
        Vector3 pos1 = (Vector3)this.getInputValue(input1);
        Vector3 pos2 = (Vector3)this.getInputValue(input2);
        float distance = Vector3.Distance(pos1, pos2);
        Debug.Log("Distance = "+distance.ToString());
        return distance;
    }
}
