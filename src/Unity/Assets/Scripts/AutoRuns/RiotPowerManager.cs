using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public struct RiotPower
{
    public float riotPower;
    // public Buff riotPowerBuff;
}
public class RiotPowerManager : AutoRunObjectBase
{
    private RiotPower _riotPower;

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

    public float RiotPower
    {
        get { return _riotPower.riotPower; }
        set { _riotPower.riotPower = Math.Clamp(value, 0f, 100f); } // ������0��100֮��
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
