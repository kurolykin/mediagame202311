using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;
public class Fans : AutoRunObjectBase
{
    Dictionary<string, string> FansAttitudeStage;

    void Start()
    {
        // this._data = new Dictionary<string, float>
        // {
        //     {"Zombie Fans", 0.0f},
        //     {"Real Fans", 0.0f},
        //     {"Haters", 0.0f},
        // };
        FansAttitudeStage.Add("Zombie Fans", "Low");
        FansAttitudeStage.Add("Normal Fans", "Low");
        FansAttitudeStage.Add("Real Fans", "Low");
        FansAttitudeStage.Add("Haters", "Low");
        
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
                buffStr += "剩余时间:" + (buff.duration-buff.timer) + "/" + buff.duration + " ";
            }
        }
        return buffStr;
    }

    public void FansAttitudeStageSet()
    {
        switch (_data["ZombieFansAttitude"] / 25)
        {
            case 0:
                FansAttitudeStage["Zombie Fans"] = "Low";
                break;
            case 1:
                FansAttitudeStage["Zombie Fans"] = "Normal";
                break;
            case 2:
                FansAttitudeStage["Zombie Fans"] = "Medium";
                break;
            case 3:
                FansAttitudeStage["Zombie Fans"] = "High";
                break;
            default:
                throw new System.Exception("FansAttitudeStageSet Error: ZombieFansAttitude out of range");
                break;
        }
        switch (_data["NormalFansAttitude"] / 25)
        {
            case 0:
                FansAttitudeStage["Normal Fans"] = "Low";
                break;
            case 1:
                FansAttitudeStage["Normal Fans"] = "Normal";
                break;
            case 2:
                FansAttitudeStage["Normal Fans"] = "Medium";
                break;
            case 3:
                FansAttitudeStage["Normal Fans"] = "High";
                break;
            default:
                throw new System.Exception("FansAttitudeStageSet Error: NormalFansAttitude out of range");
                break;

        }
        switch (_data["RealFansAttitude"] / 25)
        {
            case 0:
                FansAttitudeStage["Real Fans"] = "Low";
                break;
            case 1:
                FansAttitudeStage["Real Fans"] = "Normal";
                break;
            case 2:
                FansAttitudeStage["Real Fans"] = "Medium";
                break;
            case 3:
                FansAttitudeStage["Real Fans"] = "High";
                break;
            default:
                throw new System.Exception("FansAttitudeStageSet Error: RealFansAttitude out of range");
                break;
        }
        switch (_data["HatersAttitude"] / 25)
        {
            case 0:
                FansAttitudeStage["Haters"] = "Low";
                break;
            case 1:
                FansAttitudeStage["Haters"] = "Normal";
                break;
            case 2:
                FansAttitudeStage["Haters"] = "Medium";
                break;
            case 3:
                FansAttitudeStage["Haters"] = "High";
                break;
            default:
                throw new System.Exception("FansAttitudeStageSet Error: HatersAttitude out of range");
                break;
        }
    }
}
