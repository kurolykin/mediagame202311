using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AutoRunObjectBase : MonoBehaviour
{

    // 数据字典，用于存储对象的数据
    // Warning： 继承AutoRunObjectBase设计自己的类时，
    // 请将所有数值存放进_data字典中，不要额外定义其他的变量
    public Dictionary<string, float> _data;
    // 缓冲区字典，用于存储对象的buff
    public List<BuffBase> _buffs;
    public List<int> _buffsToRemove = new List<int>();
    

    // Start is called before the first frame update
    void Awake()
    {
        _data = new Dictionary<string, float>();
        _buffs = new List<BuffBase>();
        Debug.Log("AutoRunObjectBase Start");
    }

    // 初始化方法，用于设置数据和buff
    public virtual void Init(Dictionary<string, float> data, List<BuffBase> buffs)
    {
        this._data = data;
        this._buffs = buffs;
        if (this._data == null || this._data.Count == 0)
        {
            throw new System.Exception("AutoRunObjectBase Init Data is null or empty");
        }

    }
    
    // 抽象方法，用于在派生类中实现具体的刷新逻辑
    public virtual void Refresh()
    {
        foreach (BuffBase buff in _buffs)
        {
            buff.OnUpdate();
        }
        foreach (int index in _buffsToRemove)
        {
            _buffs.RemoveAt(index);
        }
    }

    public virtual void Reveal()
    {
        string dataStr = "";
        foreach (KeyValuePair<string, float> kvp in _data)
        {
            dataStr += kvp.Key + ":" + kvp.Value + " ";
        }
        string buffStr = "";
        foreach (BuffBase buff in _buffs)
        {
            buffStr += buff.buffName + ":" + buff.timer + " ";
        }
        Debug.Log("Reveal AutoRunObjectBase ! \n" + "AutoRunObjectBase Data: " + dataStr + " AutoRunObjectBase Buff: " + buffStr);
    }

    public virtual string RevealBuff()
    {
        string buffStr = "";
        foreach (BuffBase buff in _buffs)
        {
            buffStr += buff.buffName + ":" + buff.timer + " ";
        }
        return buffStr;
    }

    // 获取指定键的数据值
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

    // 设置指定键的数据值
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

}
