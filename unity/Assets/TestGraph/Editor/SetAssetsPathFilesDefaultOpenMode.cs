using System.IO;
using UnityEngine;
 
//--监听后缀名是"anything"的自定义文件
/*[UnityEditor.AssetImporters.ScriptedImporter(1, "graph")]
public class SampleGraphImpoter : UnityEditor.AssetImporters.ScriptedImporter
{
    public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
    {
        var pos = JsonUtility.FromJson<Vector3>(File.ReadAllText(ctx.assetPath));
        SampleGraphEditorWindow.Open();
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SetAssetsPathFilesDefaultOpenMode
{
    [UnityEditor.Callbacks.OnOpenAssetAttribute(1)]
    public static bool SingleSelect(int instanceID, int line)
    {
        return false;
    }

    [UnityEditor.Callbacks.OnOpenAssetAttribute(2)]
    public static bool DoubleSelect(int instanceID, int line)
    {
        //给双击文件事件实例化一个ID并返回所选文件路径
        string path = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(instanceID));
        //文件路径
        string name = Application.dataPath + "/" + path.Replace("Assets/", "");
        //指定打开文件类型
        if (name.EndsWith(".graph"))
        {
            SampleGraphEditorWindow.Open();
            return true;
        }
        return false;
    }
}