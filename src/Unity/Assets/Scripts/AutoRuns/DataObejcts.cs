using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;
public class DataObject : MonoBehaviour
{

    // 数据字典，用于存储对象的数据
    // Warning： 继承AutoRunObjectBase设计自己的类时，
    // 请将所有数值存放进_data字典中，不要额外定义其他的变量
    public Dictionary<string, float> _data;
    // 缓冲区字典，用于存储对象的buff
    public Dictionary<int, BuffBase> _buffs;
    public List<int> _buffsToRemove = new List<int>();


    // Start is called before the first frame update
    void Awake()
    {
        _data = new Dictionary<string, float>();
        _buffs = new Dictionary<int, BuffBase>();
        this._data.Add("热度", 1000);
        this._data.Add("心理压力", 10);
        this._data.Add("取证进度", 0);
        this._data.Add("热度增速", 0);
        this._data.Add("热度等级", 0);
        this._data.Add("压力增速", 0);
        this._data.Add("精力", 10);
    }

    // 初始化方法，用于设置数据和buff
    public virtual void Init(Dictionary<string, float> data, Dictionary<int, BuffBase> buffs)
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
        if (_buffs.Count > 0)
        {
            foreach (KeyValuePair<int, BuffBase> kvp in _buffs)
            {
                kvp.Value.OnUpdate();
                if (kvp.Value.timer > kvp.Value.duration)
                {
                    _buffsToRemove.Add(kvp.Key);
                }
            }
            foreach (int buffID in _buffsToRemove)
            {
                _buffs.Remove(buffID);
            }
            _buffsToRemove.Clear();
        }
        this._data["热度"] *= 1+this._data["热度增速"];
        this._data["心理压力"] *= 1+this._data["压力增速"];
        this._data["精力"] = Mathf.Clamp(this._data["精力"] + this._data["压力增速"], 0, 100);
    }

    public virtual void Reveal()
    {
        string dataStr = "";
        foreach (KeyValuePair<string, float> kvp in _data)
        {
            dataStr += kvp.Key + ":" + kvp.Value + " ";
        }
        string buffStr = "";
        foreach (KeyValuePair<int, BuffBase> kvp in _buffs)
        {
            buffStr += kvp.Value.buffName + ":" + kvp.Value.timer + " ";
        }   
        Debug.Log("Reveal AutoRunObjectBase ! \n" + "AutoRunObjectBase Data: " + dataStr + " AutoRunObjectBase Buff: " + buffStr);
    }

    public virtual string RevealBuff()
    {
        string buffStr = "";
        foreach (KeyValuePair<int, BuffBase> kvp in _buffs)
        {
            buffStr += kvp.Value.buffName + ":" + kvp.Value.timer + " ";
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
