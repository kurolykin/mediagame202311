using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AROManager : MonoBehaviour
{
    public int turn = 0;
    Dictionary<string, AutoRunObjectBase> _autoRunObjectBases = new Dictionary<string, AutoRunObjectBase>();

    public void Register(string name, AutoRunObjectBase autoRunObjectBase)
    {
        _autoRunObjectBases.Add(name, autoRunObjectBase);
    }

    public void Unregister(string name)
    {
        _autoRunObjectBases.Remove(name);
    }

    public void Refresh()
    {
        turn++;
        foreach (KeyValuePair<string, AutoRunObjectBase> kvp in _autoRunObjectBases)
        {
            kvp.Value.Refresh();
        }
    }

    public void Reveal()
    {
        foreach (KeyValuePair<string, AutoRunObjectBase> kvp in _autoRunObjectBases)
        {
            kvp.Value.Reveal();
        }
    }

    public float GetData(string name, string key)
    {
        return _autoRunObjectBases[name].GetData(key);
    }
    public void SetData(string name, string key, float value)
    {
        _autoRunObjectBases[name].SetData(key, value);
    }
    public AutoRunObjectBase GetInstance(string name)
    {
        return _autoRunObjectBases[name];
    }
}
