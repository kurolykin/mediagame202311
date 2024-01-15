using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuffSystem;
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
    public Sprite image; // 事件插图
    public int optionNumbers; // 包含选择数
    public List<Option> eventOptions; // 包含若干选择的数组
    public List<Tuple<AutoRunObjectBase, string, int, compareOperator>> conditions; // 事件触发条件,包含目标对象，属性名称，阈值, 比较符
    public bool isTriggered; // 事件是否被触发
    public bool isRepeatable; // 事件是否可重复触发
    public int scheduledTurn = -1; // 事件被触发的回合数,-1表示不被触发

    public EventBase(string title, string content, Sprite image, int optionNumbers, List<Option> eventOptions, List<Tuple<AutoRunObjectBase, string, int, compareOperator>> conditions, bool isTriggered, bool isRepeatable, int scheduledTurn)
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
        this.conditions = new List<Tuple<AutoRunObjectBase, string, int, compareOperator>>();
        this.isTriggered = false;
        this.isRepeatable = false;
        this.scheduledTurn = -1;
    }

    public bool JudgeCondition()
    {
        if (this.conditions.Count > 0)
        {
            foreach (Tuple<AutoRunObjectBase, string, int, compareOperator> condition in this.conditions)
            {
                switch (condition.Item4)
                {
                    case compareOperator.GreaterThan:
                        if (!(condition.Item1.GetData(condition.Item2) > condition.Item3))
                        {
                            return false;
                        }
                        break;
                    case compareOperator.LessThan:
                        if (!(condition.Item1.GetData(condition.Item2) < condition.Item3))
                        {
                            return false;
                        }
                        break;
                    case compareOperator.EqualTo:
                        if (!(condition.Item1.GetData(condition.Item2) == condition.Item3))
                        {
                            return false;
                        }
                        break;
                    case compareOperator.GreaterThanOrEqualTo:
                        if (!(condition.Item1.GetData(condition.Item2) >= condition.Item3))
                        {
                            return false;
                        }
                        break;
                    case compareOperator.LessThanOrEqualTo:
                        if (!(condition.Item1.GetData(condition.Item2) <= condition.Item3))
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
}


public class EventManager
{
    private Dictionary<int, EventBase> pendingEvents; // 待选事件列表
    private Dictionary<int, EventBase> allEvents; // 所有事件列表
    [SerializeField]
    public EventUI eventPanel;
    private Button[] choice_buttons;
    public int userChoiceIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: 从文件中读取所有事件
    }

    // 刷新函数，用于检查待选事件列表中是否有事件需要触发
    void Refresh()
    {
        if (pendingEvents.Count > 0)
        {
            foreach (KeyValuePair<int, EventBase> pendingEvent in pendingEvents)
            {
                if (pendingEvent.Value.JudgeCondition())
                {
                    //显示事件窗口
                    showWindow(pendingEvent.Value);
                    //暂停时间，等待玩家选择
                    Time.timeScale = 0;
                    //根据玩家选择，施加效果
                    if (userChoiceIndex >= 0 && userChoiceIndex < pendingEvent.Value.optionNumbers)
                    {
                        Option selectedOption = pendingEvent.Value.eventOptions[userChoiceIndex];
                        if (selectedOption.buffs.Count > 0)
                        {
                            foreach (KeyValuePair<int, int> buff in selectedOption.buffs)
                            {
                                // TODO: 用buffmanager添加buff
                            }
                        }
                        if (selectedOption.upcoming_events.Count > 0)
                        {
                            foreach (KeyValuePair<int, int> upcoming_event in selectedOption.upcoming_events)
                            {
                                //批量设置接续
                                ScheduleEvent(upcoming_event.Key, upcoming_event.Value);
                            }
                        }
                    }
                    //效果施加完毕，从待选事件列表中移除
                    pendingEvents.Remove(pendingEvent.Key);
                    //关闭事件窗口
                    //eventPanel.setActive(false);
                    //重置玩家选择
                    userChoiceIndex = -1;
                }
            }
        }
    }

    public void ClickChoice(int choiceIndex)
    {
        userChoiceIndex = choiceIndex;
        Time.timeScale = 1;
    }

    // 设置时间窗口并显示时间窗口
    public void showWindow(EventBase currentEvent)
    {
        // TODO: 显示事件窗口
    }

    public void ScheduleEvent(int eventID, int turn)
    {
        if (allEvents.ContainsKey(eventID))
        {
            allEvents[eventID].scheduledTurn = turn;
            pendingEvents.Add(eventID, allEvents[eventID]);
        }
        else
        {
            throw new Exception("Invalid event ID!");
        }
    }

    // buff导入函数
    public void ImportEvent(string filepath)
    {

    }

    public void ExportEvent(EventBase eventBase, string jsonpath)
    {
        if (!System.IO.File.Exists(jsonpath))
        {
            System.IO.File.Create(jsonpath);
        }
        string json_str = JsonUtility.ToJson(eventBase);
        System.IO.File.WriteAllText(jsonpath, json_str);
    }
}
