using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpDebugNodeRT : LpRunNode
{
    public override void Enter(LPRunEdge edge = null)
    {
        base.Enter(edge);
        string content = this.getPropertyValue<string>("content");
        Debug.Log("LpDebugNodeRT print="+content);
    }
}
