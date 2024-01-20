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
    [SerializeField]
    public Button button1;
    [SerializeField]
    public Button button2;
    [SerializeField]
    public Button button3;
    [SerializeField] 
    public Button button4;
    [SerializeField]
    public Button button5;
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
        this.aroManager.Register("fans", this.fans);
        this.aroManager.Register("riotpower", this.riotPowerManager);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        this.buffManager.ReadBuffsFromJson("Assets/configs/Buffs.json");
        this.buffManager.PrintBuffs();

        //this.buffManager.ActivateBuff(1);

        this.eventManager = gameObject.GetComponent<EventManager>();
        //this.eventManager.ReadEventsFromJson("Assets/configs/EventEG.json");
        this.eventManager.PrintEvents();

        //this.eventManager.AbsoluteSchedule(1, 10);


        UpdateDisplay();

        //循环刷新数值，开启下一回合
        //后面需要改成按钮触发
        //InvokeRepeating("RefreshAndReval", 1.5f, 1.5f);

        this.button1.onClick.AddListener(Advertise);
        this.button2.onClick.AddListener(Interact);
        this.button3.onClick.AddListener(PublicRelation);
        this.button4.onClick.AddListener(Learn);
        this.button5.onClick.AddListener(RefreshAndReval);

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
        ZBFans.text = this.fans.GetData("僵尸粉").ToString();
        RLFans.text = this.fans.GetData("真爱粉").ToString();
        NMFans.text = this.fans.GetData("路人粉").ToString();
        Haters.text = this.fans.GetData("黑粉").ToString();
        riotPowerText.text = this.riotPowerManager.GetData("riotPower").ToString();
        buffText.text = this.fans.RevealBuff();
        //this.GetComponent("体力值") = 
        if (valueA < 10)
        {
            button1.interactable = false;
        }
        else
        {
            button1.interactable = true;
        }
        if (valueA < 20)
        {
            button2.interactable = false;
        }
        else
        {
            button2.interactable = true;
        }
        if (valueA < 100)
        {
            button3.interactable = false;
        }
        else
        {
            button3.interactable = true;
        }
        if (valueA < 50)
        {
            button4.interactable = false;
        }
        else
        {
            button4.interactable = true;
        }
    }

    void RefreshAndReval()
    {
        this.valueA = 100;
        this.aroManager.Refresh();
        //this.aroManager.Reveal();
        this.UpdateDisplay();
    }

    void Advertise()
    {
        float percent = Random.Range(0f, 2f)/100;
        float cur_fans = this.fans.GetData("路人粉");
        this.fans.SetData("路人粉", cur_fans * (1 + percent));
        this.valueA = this.valueA - 10;
        this.UpdateDisplay();
    }

    void Interact()
    {
        float percent = Random.Range(0f, 5f)/100;
        float cur_fans = this.fans.GetData("路人粉");
        float cur_love = this.fans.GetData("真爱粉");
        this.fans.SetData("真爱粉", cur_love + cur_fans * percent);
        this.fans.SetData("路人粉", cur_fans * (1 - percent));
        this.valueA = this.valueA - 20;
        this.UpdateDisplay();
    }
    void PublicRelation()
    {
        float percent = Random.Range(0f, 5f);
        float cur_riotPower = this.riotPowerManager.GetData("riotpower");
        this.riotPowerManager.SetData("riotpower", cur_riotPower - percent);
        this.valueA = this.valueA - 100;
        this.UpdateDisplay();
    }
    void Learn()
    {
        float percent = Random.Range(0f, 5f)/100;
        float cur_hatred = this.fans.GetData("黑粉");
        this.fans.SetData("黑粉", cur_hatred * (1 - percent));
        float percent2 = Random.Range(0f, 10f)/100;
        float cur_fans = this.fans.GetData("路人粉");
        float cur_love = this.fans.GetData("真爱粉");
        this.fans.SetData("真爱粉", cur_love + cur_fans * percent);
        this.fans.SetData("路人粉", cur_fans * (1 - percent));
        this.valueA = this.valueA - 50;
        this.UpdateDisplay();
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
