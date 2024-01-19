using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
class ImageUtil
{
    public static byte[] ReadImage(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        //创建文件长度的buffer
        byte[] binary = new byte[fileStream.Length];
        fileStream.Read(binary, 0, (int)fileStream.Length);
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;

        return binary;
    }

    public static Sprite LoadFromFile(string path)
    {
        Texture2D m_Tex = new Texture2D(1, 1);
        //读取图片字节流
        m_Tex.LoadImage(ReadImage(path));

        //变换格式
        Sprite tempSprite = Sprite.Create(m_Tex, new Rect(0, 0, m_Tex.width, m_Tex.height), new Vector2(10, 10));
        return tempSprite;
    }
}