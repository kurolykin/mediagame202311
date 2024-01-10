
using System;
using System.Collections.Generic;

public class BuffManager
{
    private Dictionary<string, bool> buffStatus;

    public BuffManager()
    {
        buffStatus = new Dictionary<string, bool>();
    }

    public void AddBuff(string buffName)
    {
        if (!buffStatus.ContainsKey(buffName))
        {
            buffStatus.Add(buffName, false);
            Console.WriteLine($"{buffName} added to BuffManager.");
        }
        else
        {
            Console.WriteLine($"{buffName} is already present in BuffManager.");
        }
    }

    public void RemoveBuff(string buffName)
    {
        if (buffStatus.ContainsKey(buffName))
        {
            buffStatus.Remove(buffName);
            Console.WriteLine($"{buffName} removed from BuffManager.");
        }
        else
        {
            Console.WriteLine($"{buffName} is not present in BuffManager.");
        }
    }

    public void ToggleBuff(string buffName)
    {
        if (buffStatus.ContainsKey(buffName))
        {
            buffStatus[buffName] = !buffStatus[buffName];
            Console.WriteLine($"{buffName} toggled. Current status: {buffStatus[buffName]}");
        }
        else
        {
            Console.WriteLine($"{buffName} is not present in BuffManager.");
        }
    }

    public void PrintBuffStatus()
    {
        Console.WriteLine("BuffManager Status:");
        foreach (var kvp in buffStatus)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}

public abstract class Buff
{
    public string Name { get; protected set; }
    public bool IsActive { get; protected set; }

    public Buff(string name)
    {
        Name = name;
        IsActive = false;
    }

    public void Toggle()
    {
        IsActive = !IsActive;
        Console.WriteLine($"{Name} toggled. Current status: {IsActive}");
    }
}

public class Buff1 : Buff
{
    public Buff1() : base("Buff1")
    {
    }
}