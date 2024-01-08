using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Main : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI amountText;
    [SerializeField]
    TextMeshProUGUI hashtagText;
    [SerializeField]
    TextMeshProUGUI likeText;

    Fans fans;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Fans>();
        this.fans = gameObject.GetComponent<Fans>();
        Dictionary<string, float> data = new Dictionary<string, float>();
        data.Add("amount", 100);
        data.Add("hashtag", 100);
        data.Add("like", 100);
        Dictionary<string, float> buffs = new Dictionary<string, float>();
        buffs.Add("amount_bonus", 1.1f);
        buffs.Add("hashtag_bonus", 1.005f);
        buffs.Add("like_bonus", 1.2f);
        this.fans.Init(data, buffs);
        InvokeRepeating("RefreshAndReval", 1, 1);

        amountText.text = this.fans.GetData("amount").ToString();
        hashtagText.text = this.fans.GetData("hashtag").ToString();
        likeText.text = this.fans.GetData("like").ToString();
    }

    void RefreshAndReval()
    {
        this.fans.Refresh();
        this.fans.Reveal();
        amountText.text = this.fans.GetData("amount").ToString();
        hashtagText.text = this.fans.GetData("hashtag").ToString();
        likeText.text = this.fans.GetData("like").ToString();
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
