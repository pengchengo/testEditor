using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpCompareNodeRT : LpRunNode
{
    LpRunPort input1;
    LpRunPort input2;
    public override void afterInit(){
        input1 = this.getPortByName("参数1");
        input2 = this.getPortByName("参数2");
    }
    public override void Enter(LPRunEdge edge = null){
        float f1 = (float)this.getInputValue(input1);
        float f2 = (float)this.getInputValue(input2);
        if(f1 > f2){
            this.getPortByName("大于").Flow();
        }else if(f1 == f2){
            this.getPortByName("等于").Flow();
        }else{
            this.getPortByName("小于").Flow();
        }
        //input1.getValue += 
    }
}
