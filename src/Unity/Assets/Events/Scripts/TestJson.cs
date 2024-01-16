using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
public class TestJson : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Write()
    {
        EventBase eventBase = new EventBase();
        eventBase.eventID = 1;
        eventBase.title = "事件1";
        eventBase.content = "这是一个测试事件";
        eventBase.optionNumbers = 1;
        eventBase.eventOptions.Add(new Option("选项1", "这是一个测试选项", new Dictionary<int, int> { { 1, 10 } }, new Dictionary<int, int> { { 2, 10 } }));
        eventBase.isRepeatable = true;
        eventBase.isTriggered = false;
        eventBase.scheduledTurn = 1;

        string jsonpath = "Assets/configs/event1.json";

        if (!System.IO.File.Exists(jsonpath))
        {
            System.IO.File.Create(jsonpath);
        }
        string json_str = JsonUtility.ToJson(eventBase);
        System.IO.File.WriteAllText(jsonpath, json_str);
    }
    public void Read()
    {
        string jsonpath = "Assets/configs/EventEG.json";
        gameObject.GetComponent<EventManager>().ReadEventsFromJson(jsonpath);
        gameObject.GetComponent<EventManager>().PrintEvents();
    }
}

