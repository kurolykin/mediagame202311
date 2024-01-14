using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;


public class RiotPowerManager
{
    private float riotPower;

    public float RiotPower
    {
        get { return riotPower; }
        set { riotPower = Math.Clamp(value, 0f, 100f); } // ������0��100֮��
    }

    public RiotPowerManager(float initialRiotPower = 50f)
    {
        RiotPower = initialRiotPower;
    }

    public void IncreaseRiotPower(float amount)
    {
        RiotPower += amount;
        Debug.Log($"RiotPower increased by {amount}. Current RiotPower: {RiotPower}");
    }

    public void DecreaseRiotPower(float amount)
    {
        RiotPower -= amount;
        Debug.Log($"RiotPower decreased by {amount}. Current RiotPower: {RiotPower}");
    }

    public void PrintRiotPower()
    {
        Debug.Log($"Current RiotPower: {RiotPower}");
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
