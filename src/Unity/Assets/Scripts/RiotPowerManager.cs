using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RiotPowerManager
{
    // 监管力度数值
    private float riotPower;

    public float RiotPower
    {
        get { return riotPower; }
        set { riotPower = Math.Clamp(value, 0f, 100f); } // ������0��100֮��
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
        Console.WriteLine($"RiotPower increased by {amount}. Current RiotPower: {RiotPower}");
    }
    // 减少监管力度数值
    public void DecreaseRiotPower(float amount)
    {
        RiotPower -= amount;
        Console.WriteLine($"RiotPower decreased by {amount}. Current RiotPower: {RiotPower}");
    }
    // 打印监管力度数值
    public void PrintRiotPower()
    {
        Console.WriteLine($"Current RiotPower: {RiotPower}");
    }
}

class Program
{
    static void Main()
    {
        RiotPowerManager riotPowerManager = new RiotPowerManager();

        riotPowerManager.PrintRiotPower();

        riotPowerManager.IncreaseRiotPower(20f);
        riotPowerManager.DecreaseRiotPower(10f);

        riotPowerManager.PrintRiotPower();
    }
}
    }
}
