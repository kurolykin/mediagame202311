using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class BuffBase
{
    public enum buffType //区分加法和乘法
    {
        Addition,
        Multiplication,
    }
    public buffType m_buffType;
    public int buffID;
    public string buffName;
    public float timer;
    public float duration;
    public AutoRunObjectBase target_object;
    public Dictionary<string, float> target_effects; //目标数值的名称和对应的buff值

    public BuffBase(buffType buffType, int buffID, string buffName, float duration, AutoRunObjectBase target_object, Dictionary<string, float> target_effects)
    {
        this.m_buffType = buffType;
        this.buffID = buffID;
        this.buffName = buffName;
        this.duration = duration;
        this.target_object = target_object;
        this.target_effects = target_effects;
    }

    public BuffBase()
    {

    }

    public virtual void OnAdd() //由 target object 在添加此buff时候调用
    {
        if (m_buffType == buffType.Addition)
        {
            Debug.Log("target_object._data.Count = " + target_object._data.Count);
            foreach (KeyValuePair<string, float> kvp in target_effects)
            {
                Debug.Log("target_object._data[kvp.Key] = " + target_object._data[kvp.Key] + " kvp.Value = " + kvp.Value);
                target_object._data[kvp.Key] += kvp.Value;
            }
        }
        else if (m_buffType == buffType.Multiplication)
        {
            foreach (KeyValuePair<string, float> kvp in target_effects)
            {
                target_object._data[kvp.Key] *= kvp.Value;
            }
        }
        timer = 1;
    }
    public virtual void OnUpdate() //在 target object 的 Refresh() 中调用
    {
        timer += 1;
        if (timer > duration)
        {
            OnRemove(); //超时，移除buff
        }
        else //刷新目标的数据
        {
            if (buffType.Addition == m_buffType)
            {
                foreach (KeyValuePair<string, float> kvp in target_effects)
                {
                    target_object._data[kvp.Key] += kvp.Value;
                }
            }
            else if (buffType.Multiplication == m_buffType)
            {
                foreach (KeyValuePair<string, float> kvp in target_effects)
                {
                    target_object._data[kvp.Key] *= kvp.Value;
                }
            }
        }
    }
    public virtual void OnRemove() //由 target object 移除此buff时使用
    {
        //告知目标移除buff
        target_object._buffsToRemove.Add(buffID);
    }
    public virtual void OnReveal() //在 target object 的 Reveal() 中调用
    {
        string dataStr = "";
        dataStr += "BuffID: " + buffID + "\n";
        dataStr += "BuffName: " + buffName + "\n";
        dataStr += "BuffType: " + m_buffType + "\n";
        dataStr += "Duration: " + duration + "\n";
        dataStr += "TargetObject: " + target_object + "\n";
        dataStr += "TargetEffects: " + "\n";
        foreach (KeyValuePair<string, float> kvp in target_effects)
        {
            dataStr += kvp.Key + ":" + kvp.Value + ", ";
        }
        Debug.Log(dataStr);
    }
}
class BuffManager : MonoBehaviour
{
    public static Dictionary<int, BuffBase> allBuffs = new Dictionary<int, BuffBase>();
    [SerializeField]
    public AROManager aroManager;
    void Start()
    {
        // TODO load all buffs from json
    }
    public void PrintBuffs()
    {
        foreach (KeyValuePair<int, BuffBase> kvp in allBuffs)
        {
            kvp.Value.OnReveal();
        }
    }
    public void ActivateBuff(int buffID, int duration = -1)
    {
        //从AllBuffs中实例化一个buff对象
        BuffBase buff = allBuffs[buffID];
        Debug.Log("Attempting to activate buff " + buff.buffID + " on " + buff.target_object);
        buff.target_object._buffs.Add(buff); //将buff添加到目标的buff列表中
                                             //调用buff的OnAdd()函数
        if (duration != -1)
        {
            buff.duration = duration;
        }
        buff.OnAdd();
    }
    public void RegisterBuff(BuffBase buff)
    {
        allBuffs.Add(buff.buffID, buff);
    }
    //不需要RemoveBuff，因为buff的移除是由buff自己的OnRemove()函数完成的
    public void ReadBuffsFromJson(string jsonpath)
    {
        string json_str = System.IO.File.ReadAllText(jsonpath);
        JsonTextReader reader = new JsonTextReader(new StringReader(json_str));
        BuffBase curBuff = null;
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                string property = reader.Value.ToString();
                if (property == "BuffID")
                {
                    curBuff = new BuffBase();
                    curBuff.buffID = reader.ReadAsInt32().Value;
                    Debug.Log("Start reading buff " + curBuff.buffID);
                    reader.Read(); // 跳过property name
                    curBuff.buffName = reader.ReadAsString();
                    reader.Read(); // 跳过property name
                    string buffType = reader.ReadAsString();
                    //convert buffType to enum
                    BuffBase.buffType m_buffType = (BuffBase.buffType)System.Enum.Parse(typeof(BuffBase.buffType), buffType);
                    curBuff.m_buffType = m_buffType;
                    reader.Read(); // 跳过property name
                    string target_object = reader.ReadAsString();
                    curBuff.target_object = aroManager.GetInstance(target_object);
                    reader.Read(); // 跳过property name
                    curBuff.duration = (float)reader.ReadAsDouble().Value;
                    reader.Read(); // 跳过property name
                    Dictionary<string, float> target_effects = new Dictionary<string, float>();
                    while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                    {
                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            string target_effect = reader.Value.ToString();
                            Debug.Log("Reading target effect " + target_effect);
                            float target_effect_value = (float)reader.ReadAsDouble().Value;
                            target_effects.Add(target_effect, target_effect_value);
                        }
                        else if (reader.TokenType == JsonToken.EndObject)
                        {
                            break;
                        }
                    }
                    curBuff.target_effects = target_effects;
                    Debug.Log("Attempting to add buff " + curBuff.buffID);
                    allBuffs.Add(curBuff.buffID, curBuff);
                }
               
            }
        }
    }
}