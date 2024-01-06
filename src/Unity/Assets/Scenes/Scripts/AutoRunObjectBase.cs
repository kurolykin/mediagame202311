using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoRunObjectBase : MonoBehaviour
{

    protected Dictionary<string, float> _data;
    protected Dictionary<string, float> _buffs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AutoRunObjectBase Start");
    }

    public virtual void Init(Dictionary<string, float> data, Dictionary<string, float> buffs)
    {
        this._data = data;
        this._buffs = buffs;
    }
    
    // Refresh is called to update data and buff
    public abstract void Refresh();

    // reveal whole data and buff, debug only
    public virtual void Reveal(){
        string dataStr = "";
        foreach (KeyValuePair<string, float> kvp in _data)
        {
            dataStr += kvp.Key + ":" + kvp.Value + " ";
        }
        string buffStr = "";
        foreach (KeyValuePair<string, float> kvp in _buffs)
        {
            buffStr += kvp.Key + ":" + kvp.Value + " ";
        }
        Debug.Log("Reveal AutoRunObjectBase ! \n" + "AutoRunObjectBase Data: " + dataStr + " AutoRunObjectBase Buff: " + buffStr);
    }

    // get data
    public float GetData(string key)
    {
        if (_data.ContainsKey(key))
        {
            return _data[key];
        }
        else
        {
            return 0;
        }
    }

    // set data
    public void SetData(string key, float value)
    {
        if (_data.ContainsKey(key))
        {
            _data[key] = value;
        }
        else
        {
            _data.Add(key, value);
        }
    }

    // get buff
    public float GetBuff(string key)
    {
        if (_buffs.ContainsKey(key))
        {
            return _buffs[key];
        }
        else
        {
            return 0;
        }
    }

    // set buff
    public void SetBuff(string key, float value)
    {
        if (_buffs.ContainsKey(key))
        {
            _buffs[key] = value;
        }
        else
        {
            _buffs.Add(key, value);
        }
    }
}
