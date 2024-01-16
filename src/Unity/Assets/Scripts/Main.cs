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
    AROManager aroManager;
    RiotPowerManager riotPowerManager;
    EventManager eventManager;
    BuffManager buffManager;
    Fans fans;
    Button button1;
    Button button2;
    void Start()
    {
        gameObject.AddComponent<Fans>();
        this.fans = gameObject.GetComponent<Fans>();
        gameObject.AddComponent<RiotPowerManager>();
        this.riotPowerManager = gameObject.GetComponent<RiotPowerManager>();

        this.aroManager = gameObject.GetComponent<AROManager>();
        this.aroManager.Register("fans",this.fans);
        this.aroManager.Register("riotPower",this.riotPowerManager);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        this.buffManager.ReadBuffsFromJson("Assets/configs/Buffs.json");
        this.buffManager.PrintBuffs();

        this.buffManager.ActivateBuff(1);

        this.eventManager = gameObject.GetComponent<EventManager>();
        this.eventManager.ReadEventsFromJson("Assets/configs/EventEG.json");
        this.eventManager.PrintEvents();

        this.eventManager.AbsoluteSchedule(1, 10);
        

        //循环刷新数值，开启下一回合
        //后面需要改成按钮触发
        InvokeRepeating("RefreshAndReval", 1.5f,1.5f);

        this.button1 = GameObject.Find("ChoiceA").GetComponent<Button>();
        this.button2 = GameObject.Find("ChoiceB").GetComponent<Button>();
        this.button1.onClick.AddListener(() => {
            this.riotPowerManager.DecreaseRiotPower(10);
        });

        this.button2.onClick.AddListener(() => {
            this.riotPowerManager.IncreaseRiotPower(10);
        });

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
