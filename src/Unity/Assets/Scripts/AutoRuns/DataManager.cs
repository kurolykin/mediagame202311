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
        float randomChoice = 0;
        switch (cur_stage)
        {
            case 0:
                break; // 等级0说明游戏还没开始，不触发
            case 1:
                break; // 等级-酝酿阶段，不触发
            case 2:
                // 50%概率触发等级1的骂街
                randomChoice = Random.Range(0f, 1f);
                // Debug.Log("randomChoice: " + randomChoice);
                if (randomChoice < 0.5)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(6);
                }
                break;
            case 3:
                // 25%概率触发等级2的骂街 25%概率触发门口泼粪 25概率出现门口喷漆
                randomChoice = Random.Range(0f, 1f);
                Debug.Log("randomChoice: " + randomChoice);
                if (randomChoice < 0.25)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(7);
                }
                else if (randomChoice < 0.5 && randomChoice >= 0.25)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(16);
                }
                else if (randomChoice < 0.75 && randomChoice >= 0.5)
                {
                    gameObject.GetComponent<EventManager>().ShowEvent(19);
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
