
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    /// <summary>
    ///  倒水高度
    /// </summary>
    public const int FALL_HEIGHT = 10;

    /// <summary>
    ///  第一个水方块的y坐标
    /// </summary>
    public const float FIRST_Y = -1.29f;

    /// <summary>
    /// 一个水方块多高
    /// </summary>
    public const float WATER_HEIGHT = 0.84f;

    /// <summary>
    /// 水颜色
    /// </summary>
    public static Color[] WATER_COLOR = {
        new Color(107/255f, 186/255f, 255/255f),
        new Color(255/255f, 107/255f, 253/255f),
        new Color(107/255f, 255/255f, 204/255f),
        new Color(146/255f, 107/255f, 255/255f)
    };// tmd我写List习惯了 = [1,2,3,4]为什么tmd没有报错，排查半天才发现C#用大括号
}
