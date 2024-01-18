using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuffSystem;
public class Main : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI popularityText;
    [SerializeField]
    TextMeshProUGUI pressureText;
    [SerializeField]
    TextMeshProUGUI evidenceText;
    [SerializeField]
    TextMeshProUGUI popularityIncreaseText;
    [SerializeField]
    TextMeshProUGUI stageText;
    [SerializeField]
    Text buffText;
    DataManager dataManager;
    EventManager eventManager;
    BuffManager buffManager;
    DataObject player;
    Button button1;
    Button button2;
    Button button3;
    //体力值
    [SerializeField]
    TextMeshProUGUI strength;
    //体力值不足弹窗
    [SerializeField]
    TextMeshProUGUI popupText;

    void Start()
    {
        gameObject.AddComponent<DataObject>();
        this.player = gameObject.GetComponent<DataObject>();

        this.dataManager = gameObject.GetComponent<DataManager>();
        this.dataManager.Register("player", this.player);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        //批量读取buff文件
        this.buffManager.ReadBuffsFromJson("Assets/configs/StageBuffs.json");
        this.buffManager.ReadBuffsFromJson("Assets/configs/骂街-Buffs.json");
        this.buffManager.ReadBuffsFromJson("Assets/configs/快捷buff.json");
        this.buffManager.ReadBuffsFromJson("Assets/configs/阶段3buff.json");
        this.buffManager.ReadBuffsFromJson("Assets/configs/死亡.json");
        
        this.buffManager.PrintBuffs();

        this.eventManager = gameObject.GetComponent<EventManager>();
        this.eventManager.ReadEventsFromJson("Assets/configs/骂街.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/Stage1.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/Stage2.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/Stage3.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/家门破坏.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/阶段3随机.json");
        this.eventManager.PrintEvents();


        this.eventManager.ShowEvent(1);

        
        UpdateUI();

        this.button1 = GameObject.Find("ChoiceA").GetComponent<Button>();
        this.button2 = GameObject.Find("ChoiceB").GetComponent<Button>();
        this.button3 = GameObject.Find("ChoiceC").GetComponent<Button>();
        this.button1.onClick.AddListener(() => {
            this.DecreaseBy5();
        });

        this.button2.onClick.AddListener(() => {
            this.DecreaseBy10();
        });
        this.button3.onClick.AddListener(() => {
            this.RefreshAndReval(); //下一回合
        });

    }

    // void ShowPopup(string message)
    // {
    //     popupText.text = message;
    //     gameObject.SetActive(true);
    // }

    // void ClosePopup()
    // {
    //     gameObject.SetActive(false);
    // }

    void DecreaseBy5()
    {
        if (this.player.GetData("精力") >= 5)
        {
            this.player.SetData("精力", this.player.GetData("精力") - 5);
        }
        else
        {
            popupText.text = "No strength!";
            Debug.Log("体力值已不足");
        }

        UpdateUI();
    }

    void DecreaseBy10()
    {
        if (this.player.GetData("精力") >= 10)
        {
            this.player.SetData("精力", this.player.GetData("精力") - 10);
        }
        else
        {
            popupText.text = "No strength!";
            Debug.Log("体力值已不足");
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        strength.text = this.player.GetData("精力").ToString();
        popularityText.text = this.player.GetData("热度").ToString();
        pressureText.text = this.player.GetData("心理压力").ToString();
        evidenceText.text = this.player.GetData("证据").ToString();
        popularityIncreaseText.text = this.player.GetData("热度增速").ToString();
        stageText.text = this.player.GetData("热度等级").ToString();
        buffText.text = this.player.RevealBuff();
    }

    void RefreshAndReval()
    {
        this.dataManager.Refresh();
        this.UpdateUI();
    }
    
    void OnDisable()
    {
        CancelInvoke("RefreshAndReval");
    }

    void OnDestroy()
    {
        CancelInvoke("RefreshAndReval");
    }
}
