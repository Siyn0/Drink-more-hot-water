using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Cup : MonoBehaviour
{


    /// <summary>
    /// 水方块预制件
    /// </summary>
    public GameObject waterPrefab = null;

    /// <summary>
    /// 这个杯子里有的颜色
    /// </summary>

    public List<Color> hasColor = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        // CreatWater();
    }

    /// <summary>
    /// 初始生成方块
    /// </summary>
    public void CreatWater(List<Color> colorArray)
    {
        // 生成4个方块
        // for (int i = 0; i < 4; i++)
        // {
        //     Vector3 newPos = transform.position + new Vector3(0, Global.FIRST_Y + Global.WATER_HEIGHT * i, 0);
        //     GameObject newWater = Instantiate(waterPrefab, newPos, transform.rotation, transform);

        //     Debug.Log("[zzzz]生成了一个方块，位置是：" + newPos);
        //     // StartCoroutine("SetColor", newWater);
        //     int seed = (int)DateTime.Now.Ticks + i;
        //     Debug.Log("随机种子是:" + seed);
        //     System.Random random = new System.Random(seed);
        //     int randomInt = random.Next(0, 4);
        //     Color newColor = Global.WATER_COLOR[randomInt];

        //     newWater.GetComponent<SpriteRenderer>().color = newColor;
        // }

        int n = 0;
        foreach (var color in colorArray)
        {
            Vector3 newPos = transform.position + new Vector3(0, Global.FIRST_Y + Global.WATER_HEIGHT * n, -1);
            GameObject newWater = Instantiate(waterPrefab, newPos, transform.rotation, transform);
            newWater.GetComponent<SpriteRenderer>().color = color;
            hasColor.Add(color);
            n++;
        }

    }

    /*
        /// <summary>
        /// 设置生成的方块颜色 
        /// </summary>
        /// <param name="newWater">水方块</param>
        /// <returns></returns>
        IEnumerator SetColor(GameObject newWater)
        {
            Debug.Log("setColor");
            yield return new WaitForSeconds(.2f);
            int seed = (int)DateTime.Now.Ticks;
            Debug.Log("随机种子是:" + seed);
            System.Random random = new System.Random(seed);
            int randomInt = random.Next(0, 4);
            Color newColor = Global.WATER_COLOR[randomInt];

            newWater.GetComponent<SpriteRenderer>().color = newColor;
        }

        */

    /// <summary>
    /// 玩家点到杯子
    /// </summary>
    public void onClickCup()
    {
        Debug.Log("[zzzz]点击了" + gameObject.GetInstanceID());

        // TODO: 收发事件
        // SendMessage("CLICK_CUP", hasColor);
    }


    /// <summary>
    /// 哈哈，不知道咋监听，手动实现
    /// </summary>
    public void onChangedColor()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
