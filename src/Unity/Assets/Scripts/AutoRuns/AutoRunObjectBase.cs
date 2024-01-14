using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 操作类型枚举类型
// 新增一个枚举类型，表示不同的操作符
public enum OperationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
public struct Buff
{
    public string _element;
    public float _value;
    public float _durationTime;
    public string _describe;
    public OperationType _operationType;

    public Buff(string element, float value, float durationTime, string description, OperationType operationType)
    {
        _element = element;
        _value = value;
        _durationTime = durationTime;
        _describe = description;
        _operationType = operationType;
    }
}

public abstract class AutoRunObjectBase : MonoBehaviour
{

    // 数据字典，用于存储对象的数据
    protected Dictionary<string, float> _data;
    // 缓冲区字典，用于存储对象的buff
    protected Dictionary<int, Buff> _buffs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AutoRunObjectBase Start");
    }

    // 初始化方法，用于设置数据和buff

    /* 
    public InitBuff()
    {
        Debug.Log("Initial Buffs Start");

        _buffs = new Dictionary<int, Buff>
        {
            {0, new Buff("fameAmount",0.0f,0.0f,"作用于知名度的数量buff")},
            {1, new Buff("controversyAmount",0.0f,0.0f,"作用于争议度的数量buff")},
            {2, new Buff("famePercentage",0.0f,0.0f,"作用于知名度的百分比buff")},
            {3, new Buff("controversyPercentage",0.0f,0.0f,"作用于争议度的百分比buff")},
            {4, new Buff("realFansAttitude",0.0f,0.0f,"作用于真爱粉态度的百分比buff")},
            {5, new Buff("realFansCount",0.0f,0.0f,"作用于真爱粉数目的百分比buff")},
            {6, new Buff("hatersAttitude",0.0f,0.0f,"作用于黑粉态度的百分比buff")},
            {7, new Buff("hatersCount",0.0f,,0.0f,"作用于黑粉数量的百分比buff")}
        };
    } 
    */
    
    public virtual void Init(Dictionary<string, float> data, Dictionary<string, float> buffs)
    {
        this._data = data;
        this._buffs = buffs;
    }
    
    // Refresh is called to update data and buff
    // 抽象方法，用于在派生类中实现具体的刷新逻辑
    public abstract void Refresh();

    // reveal whole data and buff, debug only
    // 输出对象的全部数据和buff内容，用于调试
    public virtual void Reveal()
    {
        string dataStr = "";
        foreach (KeyValuePair<string, float> kvp in _data)
        {
            dataStr += kvp.Key + ":" + kvp.Value + " ";
        }
        string buffStr = "";
        foreach (KeyValuePair<int, Buff> kvp in _buffs)
        {
            buffStr += kvp.Value._element + ": " + kvp.Value._describe + " "; // 输出 buff 名称和 amount 属性
            buffStr += "_value: " + kvp.Value._value + " "; // 输出 buff 的 _value 属性
            buffStr += "_durationTime: " + kvp.Value._durationTime + " "; // 输出 buff 的 _durationTime 属性
        }

        Debug.Log("Reveal AutoRunObjectBase ! \n" + "AutoRunObjectBase Data: " + dataStr + " AutoRunObjectBase Buff: " + buffStr);
    }

    // buff管理器
    public abstract void BuffManager();
    // 遍历buff字典，改动data数值
    public void BuffToData()
    {
        foreach(keyValuePair<int, Buff> kvp in _buff)
        {
            // 获取当前迭代的 Buff 对象
            Buff currentBuff = kvp.Value;

            // 根据 Buff 中的 _element 字段获取对应的键
            string dataKey = currentBuff._element;

            // 检查 _data 字典中是否包含该键
            if (_data.ContainsKey(dataKey))
            {
                // 根据操作符执行相应的运算
                switch (currentBuff._operation)
                {
                    case OperationType.Addition:
                        _data[dataKey] += currentBuff._value;
                        break;
                    case OperationType.Subtraction:
                        _data[dataKey] -= currentBuff._value;
                        break;
                    case OperationType.Multiplication:
                        _data[dataKey] *= currentBuff._value;
                        break;
                    case OperationType.Division:
                        // 避免除零错误
                        if (currentBuff._value != 0)
                        {
                            _data[dataKey] /= currentBuff._value;
                        }
                        else
                        {
                            Debug.LogWarning($"Division by zero for key {dataKey}.");
                        }
                        break;
                    default:
                        Debug.LogWarning($"Unsupported operation for key {dataKey}.");
                        break;
                }
            }
            else
            {
                // 如果 _data 字典中不包含该键，你可以根据需要进行处理
                Debug.LogWarning($"Key {dataKey} not found in _data dictionary.");
            }
        }
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

    // 获取指定键的buff值
    public Buff GetBuff(int key)
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

    // 设置指定键的buff值
    public void SetBuff(int key, Buff buff)
    {
        if (_buffs.ContainsKey(key))
        {
            Debug.Log("ERROR!There is already extists a buff!")
        }
        else
        {
            // 如果键不存在，添加新的 Buff 对象
            _buffs.Add(key, buff);
        }
    }
}
