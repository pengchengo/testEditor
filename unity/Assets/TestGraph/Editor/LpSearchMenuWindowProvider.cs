using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LpSeachTree{
    public string name;
    public List<LpSeachTree> childs = new List<LpSeachTree>();

    public List<Type> nodeList = new List<Type>();

    public LpSeachTree(string n){
        name = n;
    }
}
public class LpSearchMenuWindowProvider : ScriptableObject, ISearchWindowProvider
{
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        //添加一级菜单
        //entries.Add(new SearchTreeGroupEntry(new GUIContent("新节点")));
        var lpNodeList = GetClassList(typeof(LpNode));
        var rootTree = new LpSeachTree("新节点");
        //entries.Add(new SearchTreeGroupEntry(new GUIContent("pc节点根")) { level = 1 });
        foreach (var item in lpNodeList)
        {
            var attributes = item.GetCustomAttributes(typeof(LpTitleAttribute), true);
            if(attributes.Length > 0){
                string path = (attributes[0] as LpTitleAttribute).name;
                string[] sArray= path.Split(new char[1] {'/'});
                //int curLevel = 1;
                var curTree = rootTree;
                for(int i = 0; i < sArray.Length-1; i++){
                    name = sArray[i];
                    Debug.Log("name="+name);
                    LpSeachTree childTree = null;
                    foreach(var child in curTree.childs){
                        if(child.name == name){
                            childTree = child;
                            break;
                        }
                    }
                    if(childTree == null){
                        childTree = new LpSeachTree(name);
                        curTree.childs.Add(childTree);
                    }
                    curTree = childTree;
                    //entries.Add(new SearchTreeGroupEntry(new GUIContent("name")) { level = curLevel++ });
                }
                curTree.nodeList.Add(item);
                //string str = item.Name;
                //Debug.Log(str);
                //entries.Add(new SearchTreeEntry(new GUIContent(str)) { level = curLevel++, userData = item }); 
            }
        }
        this.buildTree(entries, rootTree, 0);
        return entries;
    }

    public void buildTree(List<SearchTreeEntry> entries, LpSeachTree tree, int l){
        Debug.Log("treeName="+tree.name);
        entries.Add(new SearchTreeGroupEntry(new GUIContent(tree.name)){ level = l });
        foreach(var item in tree.nodeList){
            Debug.Log("itemName="+item.Name);
            var attributes = item.GetCustomAttributes(typeof(LpTitleAttribute), true);
            string path = (attributes[0] as LpTitleAttribute).name;
            string[] sArray = path.Split(new char[1] {'/'});
            entries.Add(new SearchTreeEntry(new GUIContent(sArray[sArray.Length-1])) { level = l+1, userData = item }); 
        }
        foreach(var child in tree.childs){
            this.buildTree(entries, child, l+1);
        }
    }

    public delegate bool SearchMenuWindowOnSelectEntryDelegate(SearchTreeEntry searchTreeEntry, SearchWindowContext context);            //声明一个delegate类

    public SearchMenuWindowOnSelectEntryDelegate OnSelectEntryHandler;                              //delegate回调方法

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        if (OnSelectEntryHandler == null)
        {
            return false;
        }
        return OnSelectEntryHandler(searchTreeEntry, context);
    }

    private List<Type> GetClassList(Type type)
    {
        var q = type.Assembly.GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => !x.IsGenericTypeDefinition)
            .Where(x => type.IsAssignableFrom(x));
        return q.ToList();
    }
}
