using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpVector3NodeRT : LpRunNode
{
    public override void afterInit(){
        LpRunPort output = this.getPortByName("出口");
        output.getter = getV3;
    }

    public object getV3(){
        Vector3 value = this.getPropertyValue<Vector3>("value");
        return (object)value;
    }
}
