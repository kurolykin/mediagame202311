using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventUI : MonoBehaviour
{
    // Start is called before the first frame update
    EventData _eventData;
    Text title;
    Text contents;
    Image image;
    Button[] choice_buttons;
    void Start()
    {
        this.title = gameObject.transform.Find("Title").GetComponent<Text>();
        this.contents = gameObject.transform.Find("Contents").GetComponent<Text>();
        this.image = gameObject.transform.Find("Image").GetComponent<Image>();
        this.choice_buttons = new Button[3];
        this.choice_buttons[0] = gameObject.transform.Find("Choice1").GetComponent<Button>();
        this.choice_buttons[1] = gameObject.transform.Find("Choice2").GetComponent<Button>();
        this.choice_buttons[2] = gameObject.transform.Find("Choice3").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    void SetContents(EventData eventData)
    {
        this._eventData = eventData;
        this.title.text = eventData.title;
        this.contents.text = eventData.contents;
        this.image = eventData.image;
        for (int i = 0; i < eventData.numchoices; i++)
        {
            this.choice_buttons[i].GetComponentInChildren<Text>().text = eventData.choices_names[i];
        }
    }

    public void emitChoice(int choice)
    {
        int globalEffectID = this._eventData.effects[choice];
        // EventSystem.Instance.EmitEvent(globalEffectID);
        // 这部分还要修改，等我想明白事件系统怎么设计
    }

}
