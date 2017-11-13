using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// UI的管理：用于显示具体哪个UIPanel被显示
/// 商城与背包不同时显示
/// </summary>


public class UIManager : MonoBehaviour {
    static   UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }
    Stack<GameObject> UIPannelStack = new Stack<GameObject>();

    public void OpenPanel(GameObject pannel)
    {

        //检出是否多次点击相同的Pannel 
        if (UIPannelStack.Contains(pannel))
            return;
                
        if (UIPannelStack.Count > 0)
        { 
            //关闭当前背包     
              ClosePanel();
        }

       //显示刚进入的背包
        UIPannelStack.Push(pannel);
        UIPannelStack.Peek().GetComponent<PanelController>().ShowPanel();
    }


    public void ClosePanel()
    {
       
        //隐藏本面板
         UIPannelStack.Peek().GetComponent<PanelController>().HidePanel();
         UIPannelStack.Pop();
        //显示新的面板：若还有新的面板可以显示的话
        if (UIPannelStack.Count > 0)
            UIPannelStack.Peek().GetComponent<PanelController>().ShowPanel();

    }





    //关于道具的所有对象的加载
    public void LoadAllInvertory(GameObject invertory)
    {
        GameObject Player = GameObject.FindWithTag("Player");
        if (invertory && invertory.GetComponent<Inventory>())
        {
            //根据所在位置判定此物品应该所在的位置
            switch (invertory.GetComponent<Inventory>().InventoryStoreState)
            {
                case InventoryStoreState.Backage:
                    Player.GetComponent<Backage>().LoadInventoryToBackage(invertory);
                    break;
                
                case InventoryStoreState.Shop:
                   
                    GetComponentInChildren<ShopFunctionScripts>().LoadAnGoodsToShop(invertory);
                    break;
                case InventoryStoreState.BeEquiped:
                    Player.GetComponent<PlayerInfor>().EquipTheInvenmtory(invertory);
                    break;
                case InventoryStoreState.Monster:
                    break;
            }
        }

    }


    /// <summary>
    /// /测试
    /// </summary>
    public GameObject Prefab;
    bool once = true;
    private void Update()
    {
        if (once)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject temp = Instantiate(Prefab);
                InventoryInfor infor = new InventoryInfor(i, "上天" + i.ToString(), "we", (EquipType)Random.Range(1, 5), i * 10, 12 + i * 2, i * 10, "如何描述：" + i.ToString(), 10 * i);
                temp.GetComponent<Inventory>().CreateInventory((InventoryStoreState)Random.Range(0, 3), infor);
              //  temp.GetComponent<Inventory>().CreateInventory(InventoryStoreState.Shop, infor);
                LoadAllInvertory(temp);

            }
            once = false;
        }

    }


}
