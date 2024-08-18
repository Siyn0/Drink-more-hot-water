using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    /// <summary>
    /// 放水的杯子数量
    /// </summary>
    public int cupCount = 4;

    /// <summary>
    /// “下一关”按钮
    /// </summary>
    public Button button = null;

    /// <summary>
    /// 当前点击的杯子最上层颜色
    /// </summary>
    private Color clickColor = Color.black;

    /// <summary>
    /// 最上面几层相同颜色
    /// </summary>
    private int clickLayer = 1;

    /// <summary>
    /// 上一次点击的杯子
    /// </summary>
    private Cup lastCup = null;

    // Start is called before the first frame update
    void Start()
    {
        initCup();
        initButton();
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
                // Debug.Log("[zzzz]allColors添加了：" + item);
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
            // Debug.Log("点击到物体：" + obj.name);
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
        /// <summary>
        /// 最上层颜色的下标
        /// </summary>
        int lastIndex = cup.hasColor.Count - 1;

        /// <summary>
        /// 准备进行移动的颜色
        /// </summary>
        Color currentColor = Color.black;

        if (lastIndex != -1)
        {
            currentColor = cup.hasColor[lastIndex];

        }

        // 为什么局部变量不显示xml注释啊？？？
        // Debug.Log("[zzzz]-1的值是" + currentColor);

        if (clickColor.Equals(Color.black))
        {
            // 是第一次点击
            Debug.Log("[zzzz]第一次点击");


            // clickColor = cup.hasColor[-1];// 哈哈不知道这样写行不？
            // Debug.Log("[zzzz]-1的值是" + clickColor);// md，果然不行

            clickColor = currentColor;
            lastCup = cup;
            for (int i = lastIndex; i > 0; i--)
            {
                if (currentColor.Equals(cup.hasColor[i - 1]))
                {
                    clickLayer += 1;
                }
                else
                {
                    break;// 第二层颜色相同，+1层，否则停止判断
                }
                Debug.Log("[zzzz]存的层数：" + clickLayer);
            }
        }
        else
        {
            // 是第二次点击
            Debug.Log("[zzzz]第二次点击");

            // 判断空位+下层颜色，上次点击杯子最上层相同颜色的方块移动到这次点击的杯子里
            if (clickColor.Equals(currentColor) || currentColor.Equals(Color.black))
            {
                Debug.Log("[zzzz]可以移动");
                int moveLayer = 1;
                while (moveLayer <= clickLayer)
                {
                    if (cup.hasColor.Count >= 4)
                    {
                        Debug.Log("[zzzz]没空位");
                        break;
                    }

                    cup.hasColor.Add(clickColor);// 向第二次点击的杯子加水
                    Transform moveWater = lastCup.gameObject.transform.GetChild(lastCup.hasColor.Count - 1);
                    moveWater.SetParent(cup.gameObject.transform);
                    Vector3 newPos = cup.gameObject.transform.position + new Vector3(0, Global.FIRST_Y + Global.WATER_HEIGHT * (cup.hasColor.Count - 1), 0);
                    moveWater.SetPositionAndRotation(newPos, moveWater.rotation);
                    moveLayer++;
                    Debug.Log("[zzzz]移动了一层");

                    lastCup.hasColor.RemoveAt(lastCup.hasColor.Count - 1);


                }
                clickColor = Color.black;
            }
            clickLayer = 1;
        }
    }

    /// <summary>
    /// 给按钮加功能
    /// </summary>
    private void initButton()
    {
        button.onClick.AddListener(() =>
        {
            // 哈哈我真会偷懒啊
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
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
