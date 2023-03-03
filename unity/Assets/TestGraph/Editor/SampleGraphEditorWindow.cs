using UnityEditor;
using UnityEngine;
 using System.Collections.Generic;

public class SampleGraphEditorWindow : EditorWindow
{
    static Dictionary<string, SampleGraphView> grapMap = new Dictionary<string, SampleGraphView>();

    static string curPath = "";

    SampleGraphView curGraphView = null;
    [MenuItem("Window/Open SampleGraphView")]
    public static void Open(string path)
    {
        curPath = path;
        GetWindow<SampleGraphEditorWindow>("SampleGraphView");
    }

    void OnEnable()
    {
        curGraphView = new SampleGraphView(this, curPath){
          style  = { flexGrow = 1}
        };
        rootVisualElement.Add(curGraphView
        );

    }

    void OnDisable(){
        Debug.Log("SampleGraphEditorWindow OnDisable");
        curGraphView.Save();
    }
}