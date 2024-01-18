using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;
public class Fans : AutoRunObjectBase
{
    void Awake()
    {
        this._data = new Dictionary<string, float>
        {
            {"僵尸粉", 1500.0f},
            {"路人粉", 500.0f},
            {"真爱粉", 50.0f},
            {"黑粉", 100.0f}
            { "粉丝", 0.0f}
        };
        this._buffs = new List<BuffBase>();
        Debug.Log("Fans Start");
    }

    // Refresh is called to update data and buff
    public override void Refresh()
    {
        if (this._buffs.Count > 0)
        {
            foreach (BuffBase buff in this._buffs)
            {
                buff.OnUpdate();
            }
            this._buffs.RemoveAll(buff => buff.timer >= buff.duration);
        }
    }

    // reveal whole data and buff, debug only
    public override void Reveal()
    {
        string dataStr = "";
        if (this._data != null && this._data.Count > 0)
        {
            foreach (KeyValuePair<string, float> kvp in this._data)
            {
                dataStr += kvp.Key + ":" + kvp.Value + " ";
            }
        }
        string buffStr = "";
        if (this._buffs != null && this._buffs.Count > 0)
        {
            foreach (BuffBase buff in this._buffs)
            {
                buffStr += buff.buffName + ":" + buff.timer + " ";
            }
        }
        Debug.Log("Reveal Fans ! \n" + "Fans Data: " + dataStr + " Fans Buff: " + buffStr);
    }

    public override string RevealBuff()
    {
        string buffStr = "";
        if (this._buffs != null && this._buffs.Count > 0)
        {
            buffStr = "Fans Buff: ";
            foreach (BuffBase buff in this._buffs)
            {
                buffStr += buff.buffName + ":";
                foreach (KeyValuePair<string, float> kvp in buff.target_effects)
                {
                    buffStr += kvp.Key;
                    if (buff.m_buffType == BuffBase.buffType.Addition)
                    {
                        if (kvp.Value > 0)
                        {
                            buffStr += "每回合增加" + kvp.Value + " ";
                        }
                        else if (kvp.Value < 0)
                        {
                            buffStr += "每回合减少" + (-kvp.Value) + " ";
                        }
                    }
                    else if (buff.m_buffType == BuffBase.buffType.Multiplication)
                    {
                        if (kvp.Value > 1)
                        {
                            buffStr += "每回合增加" + (kvp.Value - 1) * 100 + "% ";
                        }
                        else if (kvp.Value < 1)
                        {
                            buffStr += "每回合减少" + (1 - kvp.Value) * 100 + "% ";
                        }
                    }
                }
                buffStr += "剩余时间:" + (buff.duration - buff.timer) + "/" + buff.duration + " ";
            }
        }
        return buffStr;
    }
}
