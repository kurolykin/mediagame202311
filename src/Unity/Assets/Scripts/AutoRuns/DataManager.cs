using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int turn = 0;
    KeyValuePair<string, DataObject> dt_obj;
    EventManager eventManager;
    int cur_stage = 0;

    void Start()
    {
        eventManager = gameObject.GetComponent<EventManager>();
    }
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
                    eventManager.ShowEvent(6);
                }
                break;
            case 3:
                // 25%概率触发等级2的骂街 25%概率触发门口泼粪 25概率出现门口喷漆
                randomChoice = Random.Range(0f, 1f);
                Debug.Log("randomChoice: " + randomChoice);
                if (randomChoice < 0.25)
                {
                    eventManager.ShowEvent(7);
                }
                else if (randomChoice < 0.5 && randomChoice >= 0.25)
                {
                    eventManager.ShowEvent(16);
                }
                else if (randomChoice < 0.75 && randomChoice >= 0.5)
                {
                    eventManager.ShowEvent(19);
                }
                break;
            case 4:
                // 前置条件：触发热搜
                // 20%概率触发等级3的骂街 20%概率触发父亲生病 20%概率触发母亲生病 20%概率触发被辞退
                //除了骂街 都只能触发一次
                randomChoice = Random.Range(0f, 1f);
                Debug.Log("randomChoice: " + randomChoice);
                if (randomChoice < 0.2)
                {
                    if (eventManager.allEvents.ContainsKey(31) && eventManager.allEvents[31].isTriggered == false)
                    {
                        eventManager.ShowEvent(31);
                    }
                    else
                    {
                        eventManager.ShowEvent(8);
                    }
                }
                else if (randomChoice < 0.4 && randomChoice >=0.2)
                {
                    if (eventManager.allEvents.ContainsKey(32) && eventManager.allEvents[32].isTriggered == false)
                    {
                        eventManager.ShowEvent(33);
                    }
                    else
                    {
                        eventManager.ShowEvent(8);
                    }
                }
                else if (randomChoice < 0.6 && randomChoice >= 0.4)
                {
                    if (eventManager.allEvents.ContainsKey(33) && eventManager.allEvents[33].isTriggered == false)
                    {
                        eventManager.ShowEvent(34);
                    }
                    else
                    {
                        eventManager.ShowEvent(8);
                    }
                }
                else if (randomChoice < 0.8 && randomChoice >= 0.6)
                {
                        eventManager.ShowEvent(8);
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
