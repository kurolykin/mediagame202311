using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using BuffSystem;

public class RiotPowerManager : AutoRunObjectBase
{
    private string riotPowerStage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Refresh()
    {

    }

    public override void Reveal()
    {

    }

    // buff管理器
    public void BuffManage()
    {
        // AutoRunObjectBase autoRunObjectBase = new AutoRunObjectBase();

        // Buff _riotPowerBuff = new Buff(
        //     _riotPowerFans.riotPowerBuff.element,
        //     _riotPowerFans.riotPowerBuff.value,
        //     _riotPowerFans.riotPowerBuff.durationTime,
        //     _riotPowerFans.riotPowerBuff.description,
        //     _riotPowerFans.riotPowerBuff.operationType
        // );

        // autoRunObjectBase.SetBuff(buffGlobalIndex, _riotPowerBuff);

    }
    // 设置检查力度阶段
    public void RiotPowerStageSet()
    {
        switch (_data["riotPower"] / 25)
        {
            case 0:
                riotPowerStage = "Low";
                break;
            case 1:
                riotPowerStage = "Normal";
                break;
            case 2:
                riotPowerStage = "Medium";
                break;
            case 3:
                riotPowerStage = "High";
                break;
        }

    }

    // 根据检查力度设置buff
    // 未完成
    public void RiotPwoerBuff()
    {
        
    }

    public float RiotPower
    {
        get { return _riotPower.riotPower; }
        set { _riotPower.riotPower = Math.Clamp(value, 0f, 100f); } // 
    }
    // 初始化监管力度数值
    public RiotPowerManager(float initialRiotPower = 50f)
    {
        RiotPower = initialRiotPower;
    }
    // 增加监管力度数值
    public void IncreaseRiotPower(float amount)
    {
        RiotPower += amount;
        Debug.Log($"RiotPower increased by {amount}. Current RiotPower: {RiotPower}");
    }
    // 减少监管力度数值
    public void DecreaseRiotPower(float amount)
    {
        RiotPower -= amount;
        Debug.Log($"RiotPower decreased by {amount}. Current RiotPower: {RiotPower}");
    }
    // 打印监管力度数值
    public void PrintRiotPower()
    {
        Debug.Log($"Current RiotPower: {RiotPower}");
    }
}
