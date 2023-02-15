using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectUtils
{
    public static byte GetObjType(object obj)
    {
        if (obj == null) return ObjectType.Null;
        if (obj is Enum) return ObjectType.Int;
        if (obj is int) return ObjectType.Int;
        if (obj is float) return ObjectType.Float;
        if (obj is double) return ObjectType.Double;
        if (obj is Vector2) return ObjectType.Vec2;
        if (obj is Vector3) return ObjectType.Vec3;
        if (obj is Vector4) return ObjectType.Vec4;
        if (obj is bool) return ObjectType.Bool;
        if (obj is Color) return ObjectType.Color;
        if (obj is string) return ObjectType.String;
        return ObjectType.Null;
    }

    /*public static byte[] ToByte(object obj)
    {
        var buffer = new ByteBuffer();
        var type = GetObjType(obj);
        buffer.WriteByte(type);
        switch (type)
        {
            case ObjectType.Int:
                buffer.WriteInt((int) obj);
                break;
            case ObjectType.String:
                buffer.WriteString((string) obj);
                break;
            case ObjectType.Float:
                buffer.WriteFloat((float) obj);
                break;
            case ObjectType.Bool:
                buffer.WriteBool((bool) obj);
                break;
            case ObjectType.Color:
                buffer.WriteColor((Color) obj);
                break;
            case ObjectType.Double:
                buffer.WriteDouble((double) obj);
                break;
            case ObjectType.Vec2:
                buffer.WriteVec2((Vector2) obj);
                break;
            case ObjectType.Vec3:
                buffer.WriteVec3((Vector3) obj);
                break;
            case ObjectType.Vec4:
                buffer.WriteVec4((Vector4) obj);
                break;
        }

        var bs = buffer.ToBytes();
        buffer.Close();
        return bs;
    }
    public static object ToObject(byte[] bs)
    {
        var buff = new ByteBuffer(bs);
        var type = buff.ReadByte();
        object obj = null;
        switch (type)
        {
            case ObjectType.Int:
                obj = buff.ReadInt();
                break;
            case ObjectType.String:
                obj = buff.ReadString();
                break;
            case ObjectType.Float:
                obj = buff.ReadFloat();
                break;
            case ObjectType.Bool:
                obj = buff.ReadBool();
                break;
            case ObjectType.Color:
                obj = buff.ReadColor();
                break;
            case ObjectType.Double:
                obj = buff.ReadDouble();
                break;
            case ObjectType.Vec2:
                obj = buff.ReadVec2();
                break;
            case ObjectType.Vec3:
                obj = buff.ReadVec3();
                break;
            case ObjectType.Vec4:
                obj = buff.ReadVec4();
                break;
        }

        buff.Close();
        return obj;
    }*/
}

public class ObjectType
{
    public const byte Null = 0;
    public const byte Int = 1;
    public const byte Float = 2;
    public const byte Double = 3;
    public const byte Vec2 = 4;
    public const byte Vec3 = 5;
    public const byte Vec4 = 6;
    public const byte Color = 7;
    public const byte Bool = 8;
    public const byte String = 9;
}

[Serializable]
public class Property
{
    public string name;
    public object value = null;
}

[Serializable]
public class SlotData
{
    public string name;
    public object value;
}

[Serializable]
public class NodeData
{
    public string type;
    public List<Property> variables = new List<Property>();
    public List<SlotData> slots = new List<SlotData>();
    public string id;
}

[Serializable]
public class EdgeData
{
    public string source;
    public string sourceNodeId;

    public string target;
    public string targetNodeId;
}

[Serializable]
public class GraphData
{
    public List<Property> properties = new List<Property>();
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgeData> edges = new List<EdgeData>();
}