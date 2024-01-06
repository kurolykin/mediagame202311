using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fans : AutoRunObjectBase
{
    private bool _status;
    void Start()
    {
        Debug.Log("Fans Start");
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
}
