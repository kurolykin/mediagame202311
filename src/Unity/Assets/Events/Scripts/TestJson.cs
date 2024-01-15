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
        EventBase eventBase;
        string jsonpath = "Assets/configs/EventEG.json";
        string json_str = System.IO.File.ReadAllText(jsonpath);
        JsonTextReader reader = new JsonTextReader(new StringReader(json_str));
        // while(reader.Read())
        // {
        //     Debug.Log("find token: " + reader.TokenType + " value: " + reader.Value);
        // }
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                Debug.Log(reader.Value);
                string propertyName = reader.Value.ToString();
                if (propertyName == "eventID")
                {
                    Debug.Log("Start reading event");
                    eventBase = new EventBase();
                    eventBase.eventID = reader.ReadAsInt32().Value;
                    //skip next property name
                    reader.Read();
                    eventBase.title = reader.ReadAsString();
                    Debug.Log("find title: " + eventBase.title);
                    //skip next property name
                    reader.Read();
                    eventBase.content = reader.ReadAsString();
                    //skip next property name
                    reader.Read();
                    string image_path = reader.ReadAsString();
                    Debug.Log("find image path: " + image_path);
                    //skip next property name
                    reader.Read();
                    eventBase.optionNumbers = reader.ReadAsInt32().Value;
                    //skip next property name
                    reader.Read();
                    eventBase.isTriggered = reader.ReadAsBoolean().Value;
                    //skip next property name
                    reader.Read();
                    eventBase.isRepeatable = reader.ReadAsBoolean().Value;
                    //skip next property name
                    reader.Read();
                    eventBase.scheduledTurn = reader.ReadAsInt32().Value;
                    //skip next property name
                    reader.Read();
                    //now iterate to read the options
                    reader.Read();
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        Debug.Log("Start array of options");
                        reader.Read();
                        Debug.Log(reader.TokenType);
                        //start iterating the options
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            if (reader.TokenType==JsonToken.PropertyName)
                            {
                                Debug.Log("Start reading option");
                                string optionName = reader.Value.ToString();
                                Debug.Log("find option name: " + optionName);
                                if (optionName == "name")
                                {
                                    //name indecates a new option object
                                    Option option = new Option();
                                    option.name = reader.ReadAsString();
                                    Debug.Log("find option name: " + option.name);
                                    //skip next property name
                                    reader.Read();
                                    option.description = reader.ReadAsString();
                                    //skip next property name
                                    reader.Read();
                                    //now we read buffs
                                    string buffs_str = reader.ReadAsString();
                                    //split the buffs_str by ',' to get the buffs
                                    string[] buffs = buffs_str.Split(',');
                                    reader.Read();
                                    //now we read delay of buffs
                                    string buffs_delay_str = reader.ReadAsString();
                                    //split the buffs_delay_str by ',' to get the buffs_delay
                                    string[] buffs_delay = buffs_delay_str.Split(',');
                                    //store buffs and buffs_delay in the option object
                                    if (option.buffs == null)
                                    {
                                        option.buffs = new Dictionary<int, int>();
                                    }
                                    for (int i = 0; i < buffs.Length; i++)
                                    {
                                        option.buffs.Add(int.Parse(buffs[i]), int.Parse(buffs_delay[i]));
                                    }
                                    // do the same as buffs on upcoming_events
                                    reader.Read();
                                    string upcoming_events_str = reader.ReadAsString();
                                    string[] upcoming_events = upcoming_events_str.Split(',');
                                    reader.Read();
                                    string upcoming_events_delay_str = reader.ReadAsString();
                                    string[] upcoming_events_delay = upcoming_events_delay_str.Split(',');
                                    if (option.upcoming_events == null)
                                    {
                                        option.upcoming_events = new Dictionary<int, int>();
                                    }
                                    for (int i = 0; i < upcoming_events.Length; i++)
                                    {
                                        option.upcoming_events.Add(int.Parse(upcoming_events[i]), int.Parse(upcoming_events_delay[i]));
                                    }
                                    //add the option to the eventBase
                                    eventBase.eventOptions.Add(option);
                                }
                            }
                            //read the next token
                            reader.Read();
                        }
                    }
                    //now the token should be EndArray
                    Debug.Log(reader.TokenType);
                    //next token should be PropertyName
                    reader.Read();
                    Debug.Log(reader.TokenType);
                    //now we read the conditions
                    reader.Read();
                    Debug.Log(reader.TokenType);
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        Debug.Log("Start array of conditions");
                        reader.Read();
                        //start iterating the conditions
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                string target_object = reader.Value.ToString();
                                if (target_object == "target_object")
                                {
                                    //target_object indecates a new condition object
                                    string target_object_name = reader.ReadAsString();
                                    Debug.Log("find target object name: " + target_object_name);
                                    //skip next property name
                                    reader.Read();
                                    string target_data = reader.ReadAsString();
                                    Debug.Log("find target data: " + target_data);
                                    //skip next property name
                                    reader.Read();
                                    int threshold = reader.ReadAsInt32().Value;
                                    //skip next property name
                                    reader.Read();
                                    string operator_str = reader.ReadAsString();
                                    //transfer the string to enum
                                    EventBase.compareOperator compareOperator = (EventBase.compareOperator)System.Enum.Parse(typeof(EventBase.compareOperator), operator_str);
                                    Tuple<AutoRunObjectBase, string, int, EventBase.compareOperator> condition = new Tuple<AutoRunObjectBase, string, int, EventBase.compareOperator>(null, target_data, threshold, compareOperator);
                                }
                            }
                            //read the next token
                            reader.Read();
                        }
                    }
                }
            }
        }

    }
}

