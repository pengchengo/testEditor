using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpStartNodeRT : LpRunNode
{
    // Start is called before the first frame update
    public override void Enter(LPRunEdge edge = null)
    {
        this.FlowFirst();
    }
}
