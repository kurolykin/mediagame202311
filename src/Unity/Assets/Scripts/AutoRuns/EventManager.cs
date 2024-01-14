using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 添加文件操作的命名空间

// 操作类型枚举类型
// 新增一个枚举类型，表示不同的操作符
public enum OperationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

// 选择结构体定义
public struct Option
{
    public string element; //效果名称
    public float value;  // 效果数值
    public string description; // 效果描述
    public float durationTime; // 效果持续时间
    public OperationType operationType;
}

// 触发条件结构体定义
public struct Trigger
{
    string triggerElement;
    float triggerValue;
}
// 事件结构体定义
public struct Event
{
    public string text;                  // 事件文本
    public Option[] eventOptions;  // 包含三个选择的数组

    public Trigger[] eventTriggers; // 包含该事件若干触发条件
}

// 事件链结构体定义
public struct EventChain
{
    public int eventCounts;
    public Event[] events;  // 包含多个事件的数组
}

public class EventManager : AutoRunObjectBase
{
    private EventChain _eventChain;
    // Start is called before the first frame update
    void Start()
    {
        
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

    // buff导入函数
    public override void BuffManager(Event eventInstance, int playerChioceIndex)
    {
        AutoRunObjectBase autoRunObjectBase = new AutoRunObjectBase();

        // 检查选择索引是否在合法范围内
        if (playerChoiceIndex >= 0 && playerChoiceIndex < eventInstance.eventOptions.Length)
        {
            Option selectedOption = eventInstance.eventOptions[playerChioceIndex];

            Buff buff = new Buff(
                eventInstance.eventOptions[playerChioceIndex].element,
                eventInstance.eventOptions[playerChioceIndex].value,
                eventInstance.eventOptions[playerChioceIndex].durationTime,
                eventInstance.eventOptions[playerChioceIndex].description,
                eventInstance.eventOptions[playerChioceIndex].operationType
                );
        
            autoRunObjectBase.SetBuff(buffGlobalIndex, buff);
        }
        else
        {
            Debug.Log("Failed to Add Buff!");
        }
    }
}
