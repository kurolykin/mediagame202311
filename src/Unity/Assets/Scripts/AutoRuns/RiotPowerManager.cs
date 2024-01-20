using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public class RiotPowerManager : AutoRunObjectBase
{
    // Start is called before the first frame update
    void Awake()
    {
        this._data = new Dictionary<string, float>();
        this._buffs = new List<BuffBase>();
        this._data.Add("riotpower", 0.0f);
    }
    // 根据检查力度设置buff
    // 未完成
    public void RiotPwoerBuff()
    {
        BuffBase riotPowerBuff = new BuffBase(BuffBase.buffType.Addition,3,"RiotPowerBuff", 5.0f, this, new Dictionary<string,float>(){
            {"riotpower", 10f}
        });
        gameObject.GetComponent<BuffManager>().RegisterBuff(riotPowerBuff);
        this._buffs.Add(riotPowerBuff);
    }
    // 增加监管力度数值
    public void IncreaseRiotPower(float amount)
    {
        this._data["riotpower"] = Math.Min(100, this._data["riotpower"] + amount);
        Debug.Log($"riotpower increased by {amount}. Current riotpower: {this._data["riotpower"]}");
    }
    // 减少监管力度数值
    public void DecreaseRiotPower(float amount)
    {
        this._data["riotpower"] = Math.Max(0, this._data["riotpower"] - amount);
        Debug.Log($"riotpower decreased by {amount}. Current riotpower: {this._data["riotpower"]}");
    }
    // 打印监管力度数值
    public void PrintRiotPower()
    {
        Debug.Log($"Current riotpower: {this._data["riotpower"]}");
    }
}
