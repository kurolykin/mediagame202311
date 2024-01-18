using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuffSystem;
public class Main : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Haters;
    [SerializeField]
    TextMeshProUGUI RLFans;
    [SerializeField]
    TextMeshProUGUI ZBFans;
    [SerializeField]
    TextMeshProUGUI NMFans;
    [SerializeField]
    TextMeshProUGUI riotPowerText;
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
    private int valueA = 100;

    void Start()
    {
        gameObject.AddComponent<DataObject>();
        this.player = gameObject.GetComponent<DataObject>();

        this.dataManager = gameObject.GetComponent<DataManager>();
        this.dataManager.Register("player", this.player);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        this.buffManager.ReadBuffsFromJson("Assets/configs/骂街-Buffs.json");
        this.buffManager.ReadBuffsFromJson("Assets/configs/StageBuffs.json");
        this.buffManager.PrintBuffs();

        this.eventManager = gameObject.GetComponent<EventManager>();
        this.eventManager.ReadEventsFromJson("Assets/configs/骂街.json");
        this.eventManager.ReadEventsFromJson("Assets/configs/Stage0.json");
        this.eventManager.PrintEvents();

        //this.eventManager.AbsoluteSchedule(1, 1);
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
            valueA = 100;
            strength.text = "strength:" + valueA.ToString();
            popupText.text = "";
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
        if (valueA >= 5)
        {
            valueA -= 5;
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
        if (valueA >= 10)
        {
            valueA -= 10;
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
        strength.text = "strength:" + valueA.ToString();
        ZBFans.text = this.player.GetData("热度").ToString();
        NMFans.text = this.player.GetData("心理压力").ToString();
        RLFans.text = this.player.GetData("取证进度").ToString();
        Haters.text = this.player.GetData("热度增速").ToString();
        riotPowerText.text = this.player.GetData("热度等级").ToString();
        buffText.text = this.player.RevealBuff();
        //this.GetComponent("体力值") = 
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
