
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace BuffSystem
{
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

        public virtual void OnAdd() //由 target object 在添加此buff时候调用
        {
            if (m_buffType == buffType.Addition)
            {
                foreach (KeyValuePair<string, float> kvp in target_effects)
                {
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
            timer = 0;
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
            target_object._buffs.Remove(this);
        }
    }
}

// TODO：需要写一个buff管理器，根据给定的buffID动态地实例化buffBase对象
// 同时管理器也需要实现从Json读取buff配置的功能
// 这里需要考虑是否把全部buff的配置都读取到内存中，还是只读取当前需要的buff配置

// 后面我们还需要思考 buff如何与数值系统的内部运算（比如开局给定一个基础粉丝增速3%）结合：是先于数值系统的内部运算，还是后于数值系统的内部运算？
// 此外，多个乘法buff之间如何叠加？
// 乘法buff和加法buff之间怎么叠加？
// 乘法buff和加法buff之间的运算顺序？