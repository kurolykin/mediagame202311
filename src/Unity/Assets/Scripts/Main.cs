using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    AROManager aroManager;
    RiotPowerManager riotPowerManager;
    EventManager eventManager;
    BuffManager buffManager;
    Fans fans;
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
        gameObject.AddComponent<Fans>();
        this.fans = gameObject.GetComponent<Fans>();
        gameObject.AddComponent<RiotPowerManager>();
        this.riotPowerManager = gameObject.GetComponent<RiotPowerManager>();

        this.aroManager = gameObject.GetComponent<AROManager>();
        this.aroManager.Register("fans",this.fans);
        this.aroManager.Register("riotpower",this.riotPowerManager);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        this.buffManager.ReadBuffsFromJson("Assets/configs/Buffs.json");
        this.buffManager.PrintBuffs();

        this.buffManager.ActivateBuff(1);

        this.eventManager = gameObject.GetComponent<EventManager>();
        this.eventManager.ReadEventsFromJson("Assets/configs/EventEG.json");
        this.eventManager.PrintEvents();

        this.eventManager.AbsoluteSchedule(1, 10);
        
        
        UpdateDisplay();

        //循环刷新数值，开启下一回合
        //后面需要改成按钮触发
        InvokeRepeating("RefreshAndReval", 1.5f,1.5f);

        this.button1 = GameObject.Find("ChoiceA").GetComponent<Button>();
        this.button2 = GameObject.Find("ChoiceB").GetComponent<Button>();
        this.button3 = GameObject.Find("ChoiceC").GetComponent<Button>();
        this.button1.onClick.AddListener(() => {
            this.riotPowerManager.DecreaseRiotPower(10);
            this.DecreaseBy5();
        });

        this.button2.onClick.AddListener(() => {
            this.riotPowerManager.IncreaseRiotPower(10);
            this.DecreaseBy10();
        });
        this.button3.onClick.AddListener(() => {
            valueA = 100;
            strength.text = "strength:" + valueA.ToString();
            popupText.text = "";
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

        UpdateDisplay();
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

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        strength.text = "strength:" + valueA.ToString();
        //this.GetComponent("体力值") = 
    }

    void RefreshAndReval()
    {
        this.aroManager.Refresh();
        //this.aroManager.Reveal();
        ZBFans.text = this.fans.GetData("僵尸粉").ToString();
        RLFans.text = this.fans.GetData("真爱粉").ToString();
        NMFans.text = this.fans.GetData("路人粉").ToString();
        Haters.text = this.fans.GetData("黑粉").ToString();
        riotPowerText.text = this.riotPowerManager.GetData("riotPower").ToString();
        buffText.text = this.fans.RevealBuff();
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
