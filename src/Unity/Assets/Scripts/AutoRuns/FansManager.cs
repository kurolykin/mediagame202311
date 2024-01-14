using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 真爱粉结构体
public struct RealFans
{
    public float realFansAttitude;
    public float realFansCount;
    public Buff realFansBuff; // 当前状态下真爱粉buff
}
// 黑粉结构体定义
public struct Haters
{
    public float hatersAttitude;
    public float hatersCount;
    public Buff hatersBuff; // 当前状态下黑粉buff
}
public class FansManager : AutoRunObjectBase
{
    private bool _status;
    private RealFans _realFans;
    private Haters _haters;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Fans Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Refresh is called to update data and buff
    public override void Refresh()
    {
        Debug.Log("Fans Update");
        // Here we implement our own logic
        
        _data["amount"] *= _buffs["amount_bonus"];
        _data["hashtag"] *= _buffs["hashtag_bonus"];
        _data["like"] *= _buffs["like_bonus"];
    }

    // reveal whole data and buff, debug only
    public override void Reveal()
    {
        string dataStr = "";
        foreach (KeyValuePair<string, float> kvp in _data)
        {
            dataStr += kvp.Key + ":" + kvp.Value + " ";
        }
        string buffStr = "";
        foreach (KeyValuePair<string, float> kvp in _buffs)
        {
            buffStr += kvp.Key + ":" + kvp.Value + " ";
        }
        Debug.Log("Reveal Fans ! \n" + "Fans Data: " + dataStr + " Fans Buff: " + buffStr);
    }

    // buff管理器
    public override void BuffManager()
    {
        AutoRunObjectBase autoRunObjectBase = new AutoRunObjectBase();

        Buff _realFansBuff = new Buff(
            _realFans.realFansBuff.element,
            _realFans.realFansBuff.value,
            _realFans.realFansBuff.durationTime,
            _realFans.realFansBuff.description,
            _realFans.realFansBuff.operationType
        );

        autoRunObjectBase.SetBuff(buffGlobalIndex, _realFansBuff);

        Buff _hatersBuff = new Buff(
            _hatersFans.hatersBuff.element,
            _hatersFans.hatersBuff.value,
            _hatersFans.hatersBuff.durationTime,
            _hatersFans.hatersBuff.description,
            _hatersFans.hatersBuff.operationType
        );

        autoRunObjectBase.SetBuff(buffGlobalIndex, _hatersBuff);
    } 
    public float RealFansAttitude
    {
        get {return _realFans.realFansAttitude;}
        set {_realFans.realFansAttitude = Math.Clamp(value, 0f, 50f);}
    }
    public float RealFansCount
    {
        get {return _realFans.realFansCount;}
        set {_realFans.realFansCount = Math.Clamp(value, 0f, 1000f);}
    }
    public float HatersAttitude
    {
        get {return _haters.hatersAttitude;}
        set {_haters.hatersAttitude = Math.Clamp(value, 0f, 50f);}
    }
    public float HatersCount
    {
        get {return _haters.hatersCount;}
        set {_haters.hatersCount = Math.Clamp(value, 0f, 100f);}
    }
    // 增加真爱粉态度数值的方法
    public void IncreaseRealFansAttitude(float amount)
    {
        RealFansAttitude += amount;
        Console.WriteLine($"RealFansAttitude increased by {amount}. Current RealFansAttitude: {RealFansAttitude}");
    }

    // 减少真爱粉态度数值的方法
    public void DecreaseRealFansAttitude(float amount)
    {
        RealFansAttitude -= amount;
        Console.WriteLine($"RealFansAttitude decreased by {amount}. Current RealFansAttitude: {RealFansAttitude}");
    }

    // 打印当前真爱粉态度数值的方法
    public void PrintRealFansAttitude()
    {
        Console.WriteLine($"Current RealFansAttitude: {RealFansAttitude}");
    }

    // 增加真爱粉数目数值的方法
    public void IncreaseRealFansCount(float amount)
    {
        RealFansCount += amount;
        Console.WriteLine($"RealFansCount increased by {amount}. Current RealFansCount: {RealFansCount}");
    }

    // 减少真爱粉数目数值的方法
    public void DecreaseRealFansCount(float amount)
    {
        RealFansCount -= amount;
        Console.WriteLine($"RealFansCount decreased by {amount}. Current RealFansCount: {RealFansCount}");
    }

    // 打印当前真爱粉数目数值的方法
    public void PrintRealFansCount()
    {
        Console.WriteLine($"Current RealFansCount: {RealFansCount}");
    }

    // 增加黑粉态度数值的方法
    public void IncreaseHatersAttitude(float amount)
    {
        HatersAttitude += amount;
        Console.WriteLine($"HatersAttitude increased by {amount}. Current HatersAttitude: {HatersAttitude}");
    }

    // 减少黑粉态度数值的方法
    public void DecreaseHatersAttitude(float amount)
    {
        HatersAttitude -= amount;
        Console.WriteLine($"HatersAttitude decreased by {amount}. Current HatersAttitude: {HatersAttitude}");
    }

    // 打印当前黑粉态度数值的方法
    public void PrintHatersAttitude()
    {
        Console.WriteLine($"Current HatersAttitude: {HatersAttitude}");
    }

    // 增加黑粉数目数值的方法
    public void IncreaseHatersCount(float amount)
    {
        HatersCount += amount;
        Console.WriteLine($"HatersCount increased by {amount}. Current HatersCount: {HatersCount}");
    }

    // 减少黑粉数目数值的方法
    public void DecreaseHatersCount(float amount)
    {
        HatersCount -= amount;
        Console.WriteLine($"HatersCount decreased by {amount}. Current HatersCount: {HatersCount}");
    }

    // 打印当前黑粉数目数值的方法
    public void PrintHatersCount()
    {
        Console.WriteLine($"Current HatersCount: {HatersCount}");
    }
}
