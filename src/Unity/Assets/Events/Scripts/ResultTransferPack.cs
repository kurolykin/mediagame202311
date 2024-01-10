using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTransferPack
{
    public string title;
    public string target;
    public Dictionary<string, float> buffs; //用于传递buff的字典
    public uint lastTime;
}
