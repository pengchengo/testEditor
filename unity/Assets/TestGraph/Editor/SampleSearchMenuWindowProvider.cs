using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SampleSearchMenuWindowProvider : ScriptableObject, ISearchWindowProvider
{
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>();
        //添加一级菜单
        entries.Add(new SearchTreeGroupEntry(new GUIContent("pc创建新节点")));
        var triggers = GetClassList(typeof(SampleNode));

        entries.Add(new SearchTreeGroupEntry(new GUIContent("pc节点根")) { level = 1 });
        foreach (var item in triggers)
        {
            string str = item.Name;
            Debug.Log(str);
            entries.Add(new SearchTreeEntry(new GUIContent(str)) { level = 2, userData = item }); 
        }
        return entries;
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
