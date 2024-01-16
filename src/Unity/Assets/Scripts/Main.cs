using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BuffSystem;
public class Main : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI amountText;
    [SerializeField]
    TextMeshProUGUI hashtagText;
    [SerializeField]
    TextMeshProUGUI likeText;
    [SerializeField]
    TextMeshProUGUI riotPowerText;
    [SerializeField]
    Text buffText;
    AROManager aroManager;
    RiotPowerManager riotPowerManager;
    EventManager eventManager;
    BuffManager buffManager;
    Fans fans;
    // Start is called before the first frame update
    Button button1;
    Button button2;
    void Start()
    {
        

        gameObject.AddComponent<Fans>();
        this.fans = gameObject.GetComponent<Fans>();

        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("amount", 100);
        data.Add("hashtag", 100);
        data.Add("like", 100);
        List<BuffBase> buffs = new List<BuffBase>();
        this.fans.Init(data, buffs);

        this.aroManager = gameObject.GetComponent<AROManager>();
        this.aroManager.Register("fans",this.fans);

        this.buffManager = gameObject.GetComponent<BuffManager>();
        this.buffManager.ReadBuffsFromJson("Assets/configs/Buffs.json");
        this.buffManager.PrintBuffs();

        this.buffManager.ActivateBuff(1);

        this.eventManager = gameObject.GetComponent<EventManager>();
        this.eventManager.ReadEventsFromJson("Assets/configs/EventEG.json");
        this.eventManager.PrintEvents();

        this.eventManager.AbsoluteSchedule(1, 10);
        
        this.riotPowerManager = new RiotPowerManager();
        //重复调用RefreshAndReval方法 3秒一次
        InvokeRepeating("RefreshAndReval", 1.5f,1.5f);

        amountText.text = this.fans.GetData("amount").ToString();
        hashtagText.text = this.fans.GetData("hashtag").ToString();
        likeText.text = this.fans.GetData("like").ToString();
        riotPowerText.text = this.riotPowerManager.RiotPower.ToString();

        
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
        amountText.text = this.fans.GetData("amount").ToString();
        hashtagText.text = this.fans.GetData("hashtag").ToString();
        likeText.text = this.fans.GetData("like").ToString();
        riotPowerText.text = this.riotPowerManager.RiotPower.ToString();
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
