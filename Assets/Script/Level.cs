using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{

    /// <summary>
    /// 放水的杯子数量
    /// </summary>
    public int cupCount = 4;

    public GameObject[] cups = { };

    // Start is called before the first frame update
    void Start()
    {
        initCup();
    }

    /// <summary>
    /// 初始生成杯子里的水
    /// </summary>
    private void initCup()
    {
        List<Color> allColors = new List<Color> { };

        // 每个元素复制cupCount次
        foreach (var item in Global.WATER_COLOR)
        {
            for (int i = 0; i < cupCount; i++)
            {
                allColors.Add(item);
                Debug.Log("[zzzz]allColors添加了：" + item);
            }
        }

        // 打乱颜色顺序
        // 使用Fisher-Yates洗牌算法打乱List的顺序
        System.Random random = new System.Random();
        int n = allColors.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Color value = allColors[k];
            allColors[k] = allColors[n];
            allColors[n] = value;
        }

        GameObject cup = GameObject.Find("Cup");
        for (int i = 0; i < cupCount; i++)
        {
            cup.transform.GetChild(i).gameObject.GetComponent<Cup>().CreatWater(allColors.GetRange(i * 4, 4));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
