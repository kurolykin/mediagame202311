using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuffSystem;
using System.IO;
using Newtonsoft.Json;
// 这个文件还没修改完！

// 选择结构体定义
public struct Option
{
    public string name; // 选项名称
    public string description; // 选项描述
    public Dictionary<int, int> buffs; // 选项附带的buffID和持续时间
    public Dictionary<int, int> upcoming_events; // 选项附带的eventID和event发生需要延后的回合数
    public Option(string name, string description, Dictionary<int, int> buffs, Dictionary<int, int> upcoming_events)
    {
        this.name = name;
        this.description = description;
        this.buffs = buffs;
        this.upcoming_events = upcoming_events;
    }
}

// 事件结构体定义
public class EventBase
{
    public enum compareOperator
    {
        GreaterThan,
        LessThan,
        EqualTo,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo
    }
    public int eventID; // 事件ID
    public string title; // 事件标题
    public string content; // 事件文本
    public string image; // 事件插图
    public int optionNumbers; // 包含选择数
    public List<Option> eventOptions; // 包含若干选择的数组
    public List<Tuple<string, string, int, compareOperator>> conditions; // 事件触发条件,包含目标对象，属性名称，阈值, 比较符
    public bool isTriggered; // 事件是否被触发
    public bool isRepeatable; // 事件是否可重复触发
    public int scheduledTurn = -1; // 事件被触发的回合数,-1表示不被触发

    public EventBase(string title, string content, string image, int optionNumbers, List<Option> eventOptions, List<Tuple<string, string, int, compareOperator>> conditions, bool isTriggered, bool isRepeatable, int scheduledTurn)
    {
        this.title = title;
        this.content = content;
        this.image = image;
        this.optionNumbers = optionNumbers;
        this.eventOptions = eventOptions;
        this.conditions = conditions;
        this.isTriggered = isTriggered;
        this.isRepeatable = isRepeatable;
        this.scheduledTurn = scheduledTurn;
    }

    public EventBase()
    {
        this.title = "";
        this.content = "";
        this.image = null;
        this.optionNumbers = 0;
        this.eventOptions = new List<Option>();
        this.conditions = new List<Tuple<string, string, int, compareOperator>>();
        this.isTriggered = false;
        this.isRepeatable = false;
        this.scheduledTurn = -1;
    }
    public void Print()
    {
        string str = "";
        str += "Title: " + this.title + "\n";
        str += "Content: " + this.content + "\n";
        str += "Image: " + this.image + "\n";
        str += "OptionNumbers: " + this.optionNumbers + "\n";
        str += "isTriggered: " + this.isTriggered + "\n";
        str += "isRepeatable: " + this.isRepeatable + "\n";
        str += "ScheduledTurn: " + this.scheduledTurn + "\n";
        str += "EventOptions: " + "\n";
        foreach (Option option in this.eventOptions)
        {
            str += "OptionName: " + option.name + "\n";
            str += "OptionDescription: " + option.description + "\n";
            str += "OptionBuffs: " + "\n";
            foreach (KeyValuePair<int, int> buff in option.buffs)
            {
                str += "BuffID: " + buff.Key + ", ";
                str += "BuffDuration: " + buff.Value + "\n";
            }
            str += "OptionUpcomingEvents: " + "\n";
            foreach (KeyValuePair<int, int> upcoming_event in option.upcoming_events)
            {
                str += "UpcomingEventID: " + upcoming_event.Key + ", ";
                str += "UpcomingEventDelay: " + upcoming_event.Value + "\n";
            }
        }
        str += "Conditions: " + "\n";
        foreach (Tuple<string, string, int, compareOperator> condition in this.conditions)
        {
            str += "ConditionTargetObject: " + condition.Item1 + "\n";
            str += "ConditionTargetDataName: " + condition.Item2 + "\n";
            str += "ConditionThreshold: " + condition.Item3 + "\n";
            str += "ConditionCompareOperator: " + condition.Item4 + "\n";
        }
        Debug.Log(str);
    }
}


public class EventManager : MonoBehaviour
{
    private Dictionary<int, EventBase> pendingEvents = new Dictionary<int, EventBase>(); // 待选事件列表
    public Dictionary<int, EventBase> allEvents = new Dictionary<int, EventBase>(); // 所有事件列表
    [SerializeField]
    GameObject eventPanel;
    public int userChoiceIndex = -1;
    [SerializeField]
    public DataManager dataManager;
    [SerializeField]
    BuffManager buffManager;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: 从文件中读取所有事件
        // 绑定按钮
        Button[] buttons = eventPanel.transform.Find("Buttons").GetComponentsInChildren<Button>();
    }
    public bool JudgeCondition(List<Tuple<string, string, int, EventBase.compareOperator>> conditions)
    {
        if (conditions.Count > 0)
        {
            foreach (Tuple<string, string, int, EventBase.compareOperator> condition in conditions)
            {
                switch (condition.Item4)
                {
                    case EventBase.compareOperator.GreaterThan:
                        if (dataManager.GetData(condition.Item1, condition.Item2) <= condition.Item3)
                        {
                            return false;
                        }
                        break;
                    case EventBase.compareOperator.LessThan:
                        if (dataManager.GetData(condition.Item1, condition.Item2) >= condition.Item3)
                        {
                            return false;
                        }
                        break;
                    case EventBase.compareOperator.EqualTo:
                        if (dataManager.GetData(condition.Item1, condition.Item2) != condition.Item3)
                        {
                            return false;
                        }
                        break;
                    case EventBase.compareOperator.GreaterThanOrEqualTo:
                        if (dataManager.GetData(condition.Item1, condition.Item2) < condition.Item3)
                        {
                            return false;
                        }
                        break;
                    case EventBase.compareOperator.LessThanOrEqualTo:
                        if (dataManager.GetData(condition.Item1, condition.Item2) > condition.Item3)
                        {
                            return false;
                        }
                        break;
                    default:
                        throw new Exception("Invalid compare operator!");
                }
            }
        }
        return true;
    }
    // 刷新函数，用于检查待选事件列表中是否有事件需要触发
    void Update()
    {
        if (pendingEvents.Count > 0)
        {
            foreach (KeyValuePair<int, EventBase> pendingEvent in pendingEvents)
            {
                if (pendingEvent.Value.scheduledTurn <= dataManager.turn && this.JudgeCondition(pendingEvent.Value.conditions))
                {
                    //显示事件窗口
                    showWindow(pendingEvent.Value);
                }

            }
        }
    }


    //强制触发事件
    public void ShowEvent(int eventID)
    {
        EventBase currentEvent = allEvents[eventID];
        showWindow(currentEvent);
    }
    public void ClickChoice(int choiceIndex, int eventID = -1)
    {
        userChoiceIndex = choiceIndex;
        if (eventID >= 0)
        {
            EventBase currentEvent = allEvents[eventID];
            if (userChoiceIndex >= 0 && userChoiceIndex < currentEvent.optionNumbers)
            {
                Debug.Log("User choice: " + userChoiceIndex);
                Option selectedOption = currentEvent.eventOptions[userChoiceIndex];
                if (selectedOption.buffs.Count > 0)
                {
                    foreach (KeyValuePair<int, int> buff in selectedOption.buffs)
                    {
                        // 批量设置buff
                        buffManager.ActivateBuff(buff.Key, buff.Value);
                    }
                }
                if (selectedOption.upcoming_events.Count > 0)
                {
                    foreach (KeyValuePair<int, int> upcoming_event in selectedOption.upcoming_events)
                    {
                        //批量设置接续
                        RelativeSchedule(upcoming_event.Key, upcoming_event.Value);
                    }
                }
                //关闭事件窗口
                eventPanel.SetActive(false);
                //从待选事件列表中移除
                if (this.pendingEvents.ContainsKey(eventID))
                {
                    this.pendingEvents.Remove(eventID);
                }
                //重置玩家选择
                currentEvent.isTriggered = true;
                userChoiceIndex = -1;
                gameObject.GetComponent<Main>().UpdateUI();
            }
        }
    }

    // 设置时间窗口并显示时间窗口
    public void showWindow(EventBase currentEvent)
    {
        // 设置事件窗口的标题
        eventPanel.transform.Find("Title").GetComponent<Text>().text = currentEvent.title;
        // 设置事件窗口的内容
        eventPanel.transform.Find("Content").GetComponent<Text>().text = currentEvent.content;
        // 设置事件窗口的图片
        eventPanel.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(currentEvent.image);
        Transform ButtonPanel = eventPanel.transform.Find("Buttons");
        // 设置事件窗口的选项
        for (int i = 0; i < currentEvent.optionNumbers; i++)
        {
            int choiceIndex = i;
            int eventID = currentEvent.eventID;
            Button choiceButton = ButtonPanel.Find("Choice" + (i + 1)).GetComponent<Button>();
            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() => { ClickChoice(choiceIndex, eventID); });
            choiceButton.GetComponentInChildren<Text>().text = currentEvent.eventOptions[i].name + "\n" + currentEvent.eventOptions[i].description; 
        }
        // 调整选项大小和位置，并且将多余的选项隐藏
        if (currentEvent.optionNumbers == 1)
        {
            ButtonPanel.Find("Choice1").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            ButtonPanel.Find("Choice1").gameObject.SetActive(true);
            ButtonPanel.Find("Choice2").gameObject.SetActive(false);
            ButtonPanel.Find("Choice3").gameObject.SetActive(false);
        }
        else if (currentEvent.optionNumbers == 2)
        {
            ButtonPanel.Find("Choice1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-195, 0);
            ButtonPanel.Find("Choice2").GetComponent<RectTransform>().anchoredPosition = new Vector2(195, 0);
            ButtonPanel.Find("Choice1").gameObject.SetActive(true);
            ButtonPanel.Find("Choice2").gameObject.SetActive(true);
            ButtonPanel.Find("Choice3").gameObject.SetActive(false);
        }
        else if (currentEvent.optionNumbers == 3)
        {
            ButtonPanel.Find("Choice1").GetComponent<RectTransform>().anchoredPosition = new Vector2(-195, 0);
            ButtonPanel.Find("Choice2").GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            ButtonPanel.Find("Choice3").GetComponent<RectTransform>().anchoredPosition = new Vector2(195, 0);
            ButtonPanel.Find("Choice1").gameObject.SetActive(true);
            ButtonPanel.Find("Choice2").gameObject.SetActive(true);
            ButtonPanel.Find("Choice3").gameObject.SetActive(true);
        }
        // 显示事件窗口
        eventPanel.SetActive(true);
    }

    public void RelativeSchedule(int eventID, int delayTurn)
    {
        if (delayTurn < 0)
        {
            throw new Exception("Invalid delay turn!");
        }
        if (allEvents.ContainsKey(eventID))
        {
            if (pendingEvents.ContainsKey(eventID))
            {
                allEvents[eventID].scheduledTurn = dataManager.turn + delayTurn;  // 如果已经在pendingEvents里了，就直接改scheduledTurn
            }
            else
            {
                if (allEvents[eventID].isTriggered && !allEvents[eventID].isRepeatable)
                {
                    throw new Exception("Attempt to schedule a triggered unrepeatable event!");
                }
                EventBase eventBase = allEvents[eventID];
                eventBase.scheduledTurn = dataManager.turn + delayTurn;
                pendingEvents.Add(eventID, eventBase);
                Debug.Log("Add event " + eventID + " to pendingEvents " + "at turn " + eventBase.scheduledTurn);
            }
        }
        else
        {
            throw new Exception("Invalid event ID!");
        }
    }
    public void AbsoluteSchedule(int eventID, int turn)
    {
        if (turn < dataManager.turn)
        {
            throw new Exception("Invalid turn!");
        }
        if (allEvents.ContainsKey(eventID))
        {
            if (pendingEvents.ContainsKey(eventID))
            {
                allEvents[eventID].scheduledTurn = turn;    // 如果已经在pendingEvents里了，就直接改scheduledTurn
            }
            else
            {
                if (allEvents[eventID].isTriggered && !allEvents[eventID].isRepeatable)
                {
                    throw new Exception("Attempt to schedule a triggered unrepeatable event!");
                }
                EventBase eventBase = allEvents[eventID];
                eventBase.scheduledTurn = turn;
                pendingEvents.Add(eventID, eventBase);
            }
        }
        else
        {
            throw new Exception("Invalid event ID!");
        }
    }

    // 从json文件中读取所有事件
    public void ReadEventsFromJson(string jsonpath)
    {
        string json_str = System.IO.File.ReadAllText(jsonpath);
        JsonTextReader reader = new JsonTextReader(new StringReader(json_str));
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                // Debug.Log(reader.Value);
                string propertyName = reader.Value.ToString();
                if (propertyName == "eventID")
                {
                    // Debug.Log("Start reading event");
                    EventBase eventBase = new EventBase();
                    eventBase.eventID = reader.ReadAsInt32().Value;
                    reader.Read(); // 跳过property name
                    eventBase.title = reader.ReadAsString();
                    // Debug.Log("find title: " + eventBase.title);
                    reader.Read(); // 跳过property name
                    eventBase.content = reader.ReadAsString();
                    reader.Read(); // 跳过property name
                    eventBase.image = reader.ReadAsString();
                    reader.Read(); // 跳过property name
                    eventBase.optionNumbers = reader.ReadAsInt32().Value;
                    reader.Read(); // 跳过property name
                    eventBase.isTriggered = reader.ReadAsBoolean().Value;
                    reader.Read(); // 跳过property name
                    eventBase.isRepeatable = reader.ReadAsBoolean().Value;
                    reader.Read(); // 跳过property name
                    eventBase.scheduledTurn = reader.ReadAsInt32().Value;
                    reader.Read(); // 跳过property name
                    //now iterate to read the options
                    reader.Read();
                    if (reader.TokenType == JsonToken.StartArray)
                    {
                        // Debug.Log("Start array of options");
                        reader.Read();
                        Debug.Log(reader.TokenType);
                        //start iterating the options
                        while (reader.TokenType != JsonToken.EndArray)
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                // Debug.Log("Start reading option");
                                string optionName = reader.Value.ToString();
                                // Debug.Log("find option name: " + optionName);
                                if (optionName == "name")
                                {
                                    //name indecates a new option object
                                    Option option = new Option();
                                    option.name = reader.ReadAsString();
                                    Debug.Log("find option name: " + option.name);
                                    reader.Read(); // 跳过property name
                                    option.description = reader.ReadAsString();
                                    reader.Read(); // 跳过property name

                                    //now we read buffs
                                    if (option.buffs == null)
                                    {
                                        option.buffs = new Dictionary<int, int>();
                                    }
                                    string buffs_str = reader.ReadAsString();
                                    if (buffs_str != "none") // none 代表没有buff，遇到none就不读了
                                    {
                                        //split the buffs_str by ',' to get the buffs
                                        string[] buffs = buffs_str.Split(',');
                                        reader.Read(); // 跳过property name
                                        //now we read delay of buffs
                                        string buffs_delay_str = reader.ReadAsString();
                                        Debug.Log("Buffs Delay: " + buffs_delay_str);
                                        //split the buffs_delay_str by ',' to get the buffs_delay
                                        string[] buffs_delay = buffs_delay_str.Split(',');
                                        //store buffs and buffs_delay in the option object
                                        for (int i = 0; i < buffs.Length; i++)
                                        {
                                            option.buffs.Add(int.Parse(buffs[i]), int.Parse(buffs_delay[i]));
                                        }
                                    }
                                    else
                                    {
                                        reader.Read(); // 跳过buffs_delay的property name
                                        reader.Read(); // 跳过buffs_delay
                                    }
                                    // do the same as buffs on upcoming_events
                                    if (option.upcoming_events == null)
                                    {
                                        option.upcoming_events = new Dictionary<int, int>();
                                    }
                                    reader.Read(); // 跳过upcoming_events的property name
                                    string upcoming_events_str = reader.ReadAsString();
                                    if (upcoming_events_str != "none")
                                    {
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
                                    }
                                    else
                                    {
                                        reader.Read(); // 跳过upcoming_events_delay的property name
                                        reader.Read(); // 跳过upcoming_events_delay
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
                                string property = reader.Value.ToString();
                                if (property == "target_object")
                                {
                                    //target_object indecates a new condition object
                                    string target_object_name = reader.ReadAsString();
                                    Debug.Log("find target object name: " + target_object_name);
                                    reader.Read(); // 跳过property name
                                    string target_data = reader.ReadAsString();
                                    Debug.Log("find target data: " + target_data);
                                    reader.Read(); // 跳过property name
                                    int threshold = reader.ReadAsInt32().Value;
                                    reader.Read(); // 跳过property name
                                    string operator_str = reader.ReadAsString();
                                    //transfer the string to enum
                                    EventBase.compareOperator compareOperator = (EventBase.compareOperator)System.Enum.Parse(typeof(EventBase.compareOperator), operator_str);
                                    Tuple<string, string, int, EventBase.compareOperator> condition = new Tuple<string, string, int, EventBase.compareOperator>(target_object_name, target_data, threshold, compareOperator);
                                    eventBase.conditions.Add(condition);
                                }
                            }
                            //read the next token
                            reader.Read();
                        }
                    }
                    this.allEvents.Add(eventBase.eventID, eventBase);
                    // 看看读出来的数据对不对
                    eventBase.Print();
                }
            }
        }
    }
    public void PrintEvents()
    {
        Debug.Log("EventManager: Print all, count: " + allEvents.Count);
        foreach (KeyValuePair<int, EventBase> eventBase in allEvents)
        {
            eventBase.Value.Print();
        }
    }
}
