using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RiotPowerManager
{
    private float riotPower;

    public float RiotPower
    {
        get { return riotPower; }
        set { riotPower = Math.Clamp(value, 0f, 100f); } // 限制在0到100之间
    }

    public RiotPowerManager(float initialRiotPower = 50f)
    {
        RiotPower = initialRiotPower;
    }

    public void IncreaseRiotPower(float amount)
    {
        RiotPower += amount;
        Console.WriteLine($"RiotPower increased by {amount}. Current RiotPower: {RiotPower}");
    }

    public void DecreaseRiotPower(float amount)
    {
        RiotPower -= amount;
        Console.WriteLine($"RiotPower decreased by {amount}. Current RiotPower: {RiotPower}");
    }

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
