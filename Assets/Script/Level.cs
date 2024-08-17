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

    /// <summary>
    /// 当前点击的杯子最上层颜色
    /// </summary>
    private Color clickColor = Color.black;

    /// <summary>
    /// 最上面几层相同颜色
    /// </summary>
    private int clickLayer = 0;

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

    /// <summary>
    /// 点击鼠标时
    /// </summary>
    private void onClick()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log("点击到物体：" + obj.name);
            if (!!obj.GetComponent<Cup>())
            {
                // obj.GetComponent<Cup>().onClickCup();
                onClickCup(obj.GetComponent<Cup>());
            }
        }
    }

    /// <summary>
    /// 点击到的是杯子
    /// </summary>
    /// <param name="cup">杯子组件</param>
    private void onClickCup(Cup cup)
    {
        if (clickColor != Color.black)
        {
            // 是第二次点击
            // TODO: 判断空位+下层颜色，上次点击杯子最上层相同颜色的方块移动到这次点击的杯子里(出栈)
        }
        else
        {
            // 是第一次点击
            clickColor = cup.hasColor[-1];// 哈哈不知道这样写行不？
            Debug.Log("[zzzz]-1的值是" + clickColor);
            // TODO: 判断下一层颜色，存数量
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClick();
        }
    }
}
