using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using BuffSystem;

public class RiotPowerManager : AutoRunObjectBase
{
    // Start is called before the first frame update
    void Start()
    {
        this._data = new Dictionary<string, float>
        {
            {"RiotPower", 0.0f},
        };
    }
    // 根据检查力度设置buff
    // 未完成
    public void RiotPwoerBuff()
    {
        BuffBase riotPowerBuff = new BuffBase(BuffBase.buffType.Addition,3,"RiotPowerBuff", 5.0f, this, new Dictionary<string,float>(){
            {"riotPower", 10f}
        });
        gameObject.GetComponent<BuffManager>().RegisterBuff(riotPowerBuff);
        this._buffs.Add(riotPowerBuff);
    }
    // 增加监管力度数值
    public void IncreaseRiotPower(float amount)
    {
        this._data["RiotPower"] = Math.Min(100, this._data["RiotPower"] + amount);
        Debug.Log($"RiotPower increased by {amount}. Current RiotPower: {this._data["RiotPower"]}");
    }
    // 减少监管力度数值
    public void DecreaseRiotPower(float amount)
    {
        this._data["RiotPower"] = Math.Max(0, this._data["RiotPower"] - amount);
        Debug.Log($"RiotPower decreased by {amount}. Current RiotPower: {this._data["RiotPower"]}");
    }
    // 打印监管力度数值
    public void PrintRiotPower()
    {
        Debug.Log($"Current RiotPower: {this._data["RiotPower"]}");
    }
}
