using System;

public class LpTitleAttribute : Attribute
{
    public string name;
    public LpTitleAttribute(string n){
        name = n;
    }
}
