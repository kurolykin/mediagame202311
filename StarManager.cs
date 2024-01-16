using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;
public abstract class StarManager : AutoRunObjectBase
{

    public Text conditionText;
    int seed = 1234;
    Random _random = new Random(seed);

    public void Study()
    {
        int proccessNumber = _random.Next(-100, 100);
        if(proccessNumber > 0)
        {
            conditionText.text = $"你刻苦努力的学习收受到了神明的眷顾，能力 {processNumber} 。";
        }
        else if(proccessNumber = 0)
        {
            conditionText.text = $"你刻苦努力的学习,但是并没有什么卵用。";
        }
        else
        {
            conditionText.text = $"你学习的过程中谁的很香，梦中的美味大餐让你忘记了什么，能力 {processNumber} .";
        }
        _data["Ability"] += proccessNumber;
    }

    public void performance()
    {
        int proccessNumber = _random.Next(-200, 200);
        if(proccessNumber > 0)
        {
            conditionText.text = $"你刻苦努力的学习收受到了神明的眷顾，能力 {processNumber} 。";
        }
        else if(proccessNumber = 0)
        {
            conditionText.text = $"你刻苦努力的学习,但是并没有什么卵用。";
        }
        else
        {
            conditionText.text = $"你学习的过程中谁的很香，梦中的美味大餐让你忘记了什么，能力 {processNumber} .";
        }
        _data["Activity"] += proccessNumber;
    }

    public void PR()
    {
        int proccessNumber = _random.Next(-100, 100);
        if(proccessNumber > 0)
        {
            conditionText.text = $"你刻苦努力的学习收受到了神明的眷顾，能力 {processNumber} 。";
        }
        else if(proccessNumber = 0)
        {
            conditionText.text = $"你刻苦努力的学习,但是并没有什么卵用。";
        }
        else
        {
            conditionText.text = $"你学习的过程中谁的很香，梦中的美味大餐让你忘记了什么，能力 {processNumber} .";
        }
        _data["PR"] += proccessNumber;
    }

    public void CharacterSet()
    {
        int proccessNumber = _random.Next(-50, 50);
        if(proccessNumber > 0)
        {
            conditionText.text = $"你刻苦努力的学习收受到了神明的眷顾，能力 {processNumber} 。";
        }
        else if(proccessNumber = 0)
        {
            conditionText.text = $"你刻苦努力的学习,但是并没有什么卵用。";
        }
        else
        {
            conditionText.text = $"你学习的过程中谁的很香，梦中的美味大餐让你忘记了什么，能力 {processNumber} .";
        }
        _data["CharacterSet"] += proccessNumber;
    }
}
