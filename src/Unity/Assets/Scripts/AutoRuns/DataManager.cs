using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int turn = 0;
    KeyValuePair<string, DataObject> dt_obj;

    int cur_stage = 0;
    public void Register(string name, DataObject DataObject)
    {
        dt_obj = new KeyValuePair<string, DataObject>(name, DataObject);
    }

    public void Unregister(string name)
    {
        dt_obj = new KeyValuePair<string, DataObject>();
    }

    public void Refresh()
    {
        turn++;
        cur_stage = (int)dt_obj.Value.GetData("热度等级");
        dt_obj.Value.Refresh();
        float curseProb = 0.0f;
        switch (cur_stage)
        {
            case 0:
                break; // 等级0说明游戏还没开始，不触发
            case 1:
                curseProb = 0.35f;
                if (Random.Range(0, 1) < curseProb)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(6); //触发等级1的骂街
                }
                break;
            case 2:
                curseProb = 0.50f;
                if (Random.Range(0, 1) < curseProb)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(7); //触发等级2的骂街
                }
                break;
            case 3:
                curseProb = 0.65f;
                if (Random.Range(0, 1) < curseProb)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(8); //触发等级3的骂街
                }
                break;
            default:
                throw new System.Exception("热度等级错误");
        }
    }

    public void Reveal()
    {
        dt_obj.Value.Reveal();
    }

    public float GetData(string name, string key)
    {
        return dt_obj.Value.GetData(key);
    }
    public void SetData(string name, string key, float value)
    {
        dt_obj.Value.SetData(key, value);
    }
    public DataObject GetInstance(string name)
    {
        return dt_obj.Value;
    }
}
