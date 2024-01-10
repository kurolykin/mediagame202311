using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct EventData
{
    public string title;
    public string contents;

    public Image image;

    public int numchoices;

    public Dictionary<int, string> choices_names; //存储选项的名字

    public Dictionary<int, int> effects; //存储选项的效果，key为选项的序号，value为效果的ID


}