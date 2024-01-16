using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

// 这个文件还没修改完！

// 操作类型枚举类型
// 新增一个枚举类型，表示不同的操作符
public enum Operators
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

// 选择结构体定义
public struct Option
{
    public string name; // 选项名称
    public string element; // buff效果名称
    public float value;  // buff效果数值
    public string description; // 效果描述
    public float durationTime; // 效果持续时间
}

// 事件结构体定义
public struct Event
{
    public string title; // 事件标题
    public string content; // 事件文本
    public Image image; // 事件插图
    public int optionNumbers; // 包含选择数
    public Option[] eventOptions; // 包含若干选择的数组
    public Dictionary<string, float> eventTriggers; // 包含该事件若干触发条件的数组
}

// 事件链结构体定义
public struct EventChain
{
    public int eventCounts;
    public Event[] events;  // 包含多个事件的数组
    public Event* pt_event;
}

public class EventManager
{

    private EventChain[] _eventChain;
    public GameObject eventPanel;
    // Start is called before the first frame update
    void Start()
    {
        eventPanel = gameObject.Find("EventPanel");
        
        eventPanel.title = gameObject.transform.Find("Title").GetComponent<Text>();
        eventPanel.contents = gameObject.transform.Find("Contents").GetComponent<Text>();
        eventPanel.image = gameObject.transform.Find("Image").GetComponent<Image>();
        eventPanel.choice_buttons = new Button[3];
        eventPanel.choice_buttons[0] = gameObject.transform.Find("Choice1").GetComponent<Button>();
        eventPanel.choice_buttons[1] = gameObject.transform.Find("Choice2").GetComponent<Button>();
        eventPanel.choice_buttons[2] = gameObject.transform.Find("Choice3").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 事件链导入函数
    public EventChain LoadEventChain(string filePath)
    {
        
        EventChain eventChain;

        // 读取 JSON 文件内容
        string jsonContent = File.ReadAllText(filePath);

        // 使用JsonUtility解析JSON字符串
        eventChain = JsonUtility.FromJson<EventChain>(jsonContent);

        return eventChain;
    }

    // 设置时间窗口并显示时间窗口
    public void SetEventPanel(Event currentEvent)
    {
        eventPanel.title.text = currentEvent.title;
        eventPanel.contents.text = currentEvent.content;
        eventPanel.image = currentEvent.image;
        for (int i = 0; i < currentEvent.optionNumbers; i++)
        {
            eventPanel.choice_buttons[i].GetComponentInChildren<Text>().text = currentEvent.eventOptions[i].name;
        }

        eventPanel.SetActive(true);
    }
    // 判定触发条件
    public bool IsTrigger(Event currentEvent)
    {
        bool isTrigger = false;

        if(currentEvent.eventTriggers.Count > 0)
        {
            foreach (KeyValuePair<string, float> kvp in currentEvent.eventTriggers)
            {
                if(_data[currentEvent.eventTriggers.key]>=currentEvent.eventTriggers.value)
                {
                    isTrigger = true;
                }
                else{
                    isTrigger = false;
                }
            }
        }
        return isTrigger;
    }
    // 选择按钮1
    public bool ChoiceButtonFirst()
    {
        return 1;
    }
    // 选择按钮2
    public bool ChoiceButtonSecond()
    {
        return 2;
    }
    // 选择按钮3
    public bool ChoiceButtonThird()
    {
        return 3;
    }
    // 选择
    public void SelectedOption(Event currentEvent)
    {
        int optionIndex;
        Option currentOption = currentEvent.eventOptions[optionIndex];
    }
    // 遍历待选事件
    public void Circuit()
    {
        foreach(EventChain currentChain in _eventChain)
        {
            bool isTrigger = IsTrigger(pt_event);

            if(isTrigger)
            {
                SetEventPanel(Event pt_event);
            }

        }
    }
    // buff导入函数
    public void importBuff(Event eventInstance, int playerChioceIndex)
    {
    //     AutoRunObjectBase autoRunObjectBase = new AutoRunObjectBase();

    //     // 检查选择索引是否在合法范围内
    //     if (playerChoiceIndex >= 0 && playerChoiceIndex < eventInstance.eventOptions.Length)
    //     {
    //         Option selectedOption = eventInstance.eventOptions[playerChioceIndex];

    //         Buff buff = new Buff(
    //             eventInstance.eventOptions[playerChioceIndex].element,
    //             eventInstance.eventOptions[playerChioceIndex].value,
    //             eventInstance.eventOptions[playerChioceIndex].durationTime,
    //             eventInstance.eventOptions[playerChioceIndex].description,
    //             eventInstance.eventOptions[playerChioceIndex].operationType
    //             );
        
    //         autoRunObjectBase.SetBuff(buffGlobalIndex, buff);
    //     }
    //     else
    //     {
    //         Debug.Log("Failed to Add Buff!");
    //     }
    }
}
