using System;

public class LpControlAttribute : LpBaseControlAttribute
{
    public string name = "";
    public LpControlAttribute(string n){
        name = n;
    }
}
