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
    RiotPowerManager riotPowerManager;
    Fans fans;
    // Start is called before the first frame update
    Button button1;
    Button button2;
    void Start()
    {
        this.riotPowerManager = new RiotPowerManager();
        gameObject.AddComponent<Fans>();

        this.fans = gameObject.GetComponent<Fans>();
        this.button1 = GameObject.Find("ChoiceA").GetComponent<Button>();
        this.button2 = GameObject.Find("ChoiceB").GetComponent<Button>();

        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("amount", 100);
        data.Add("hashtag", 100);
        data.Add("like", 100);
        List<BuffBase> buffs = new List<BuffBase>();
        buffs.Add(new BuffBase(BuffBase.buffType.Addition, 1, "生育警察", 10, this.fans, new Dictionary<string, float> { { "amount", 100 } }));
        buffs.Add(new BuffBase(BuffBase.buffType.Multiplication, 2, "计划生育", 30, this.fans, new Dictionary<string, float> { { "amount", 0.9f },
        {"like", 0.7f} }));
        this.fans.Init(data, buffs);
        InvokeRepeating("RefreshAndReval", 1, 1);

        amountText.text = this.fans.GetData("amount").ToString();
        hashtagText.text = this.fans.GetData("hashtag").ToString();
        likeText.text = this.fans.GetData("like").ToString();
        riotPowerText.text = this.riotPowerManager.RiotPower.ToString();

        this.button1.onClick.AddListener(() => {
            this.riotPowerManager.DecreaseRiotPower(10);
        });

        this.button2.onClick.AddListener(() => {
            this.riotPowerManager.IncreaseRiotPower(10);
        });

    }

    void RefreshAndReval()
    {
        this.fans.Refresh();
        this.fans.Reveal();
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
