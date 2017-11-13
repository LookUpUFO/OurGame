using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包的数据的显示 ：其数据来源于人物。
///                    这里只是显示功能与交互功能。
///                    不保存任何的装备的数据
/// </summary>


public class BackagePanelInforShow : MonoBehaviour {
    //玩家携带的背包：数据都来源于此处
     Backage   playerBackage;
    //因为采用的是Scroll View :所以数据都存放于其子物体Content下：采用的是只能使用固定类型的物品
     Transform Content;
     int backageSize;
     public int BackageSize { get { return backageSize; } }

    private void Start()
    {

        playerBackage = GameObject.FindWithTag("Player").GetComponent<Backage>();
        Content       = transform.GetComponentInChildren<ScrollRect>().content;
        backageSize   = Content.childCount;

        //刷新方式：调用面板刷新，背包数据改变刷新
        playerBackage.OnBackageItemChangeEvent += LoadInventory;
        GetComponent<PanelController>().RefeshTheInforToPanelEvent += LoadInventory;
    }
    //加载背包中的物品到背包中 :并修改名字为物品的名字
    void LoadInventory()
    {

        List<GameObject> inBackageInventory = playerBackage.GetAllInvertory();
        for (int i = 0; i < inBackageInventory.Count; i++)
        { 
            inBackageInventory[i].transform.SetParent(Content.GetChild(i));

            inBackageInventory[i].transform.localPosition = Vector3.zero;
            inBackageInventory[i].transform.localScale = Content.GetChild(i).localScale;
            
            Content.GetChild(i).name = inBackageInventory[i].name;

            //刷新每个物品
            inBackageInventory[i].GetComponent<Inventory>().Refesh();
        }
        //加载金币的数量
        transform.Find("Coin").GetComponentInChildren<Text>().text= playerBackage.Coin.ToString();


    }
   
}
