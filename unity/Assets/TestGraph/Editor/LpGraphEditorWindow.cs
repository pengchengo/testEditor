using UnityEditor;
using UnityEngine;
 using System.Collections.Generic;

public class LpGraphEditorWindow : EditorWindow
{
    static Dictionary<string, LpGraphView> grapMap = new Dictionary<string, LpGraphView>();

    static string curPath = "";

    LpGraphView curGraphView = null;
    [MenuItem("Window/Open LpGraphView")]
    public static void Open(string path)
    {
        curPath = path;
        GetWindow<LpGraphEditorWindow>("LpGraphView");
    }

    void OnEnable()
    {
        curGraphView = new LpGraphView(this, LpGraphEditorWindow.curPath){
          style  = { flexGrow = 1}
        };
        rootVisualElement.Add(curGraphView
        );

    }

    void OnDestroy(){
        Debug.Log("LpGraphEditorWindow OnDestroy ");
        if(curGraphView != null){
            curGraphView.Save();
            curGraphView = null;
        }
        
    }
}