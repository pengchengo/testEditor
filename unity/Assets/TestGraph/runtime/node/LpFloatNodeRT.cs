using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpFloatNodeRT : LpRunNode
{
    public override void afterInit(){
        LpRunPort output = this.getPortByName("出口");
        output.getter = getFloat;
    }

    public object getFloat(){
        float value = this.getPropertyValue<float>("value");
        return (object)value;
    }
}
