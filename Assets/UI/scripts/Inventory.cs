using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// 对每个装备对象的描述
///    具备创建功能：创建此装备对象
/// </summary>
//装备的存放状态:存放于哪里
public enum InventoryStoreState
{
    //  商店 , 背包   , 被穿戴  ，   怪物   ,  地面 
    Shop, Backage, BeEquiped    , Monster,  Ground,

}

public class Inventory : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler ,IPointerClickHandler
{
    //装备的信息显示面板
    Transform inforPanel;
    //功能面板 ：买/卖/使用
    Transform   btnPanel;
    //子物体具有功能排列
    // 0:买
    // 1:卖
    // 2:装备/使用
    // 3:卸下装备
    // 4:取消
    enum  OpenOrCloseWay
    {
        //滑动     ， 缩放  ，透明 ........
         TRANSFORM , SCALE  , TRANSPARENTS
    }


    //玩家
    GameObject   Player;
    InventoryStoreState inventoryStoreState = InventoryStoreState.Backage;
    public InventoryStoreState InventoryStoreState { get { return inventoryStoreState; } }
    [HideInInspector]
    public Image              inventoryImg ;
    [HideInInspector]

    public InventoryInfor inventoryInforOfThisItem; // = new InventoryInfor(0,"未知", "未知",EquipType.Ring,10,10,10,"不存在此物品物品",10);

    private void Start()
    {

        Player       = GameObject.FindWithTag("Player");
        inforPanel   = OtherInforPannelManager.instance.transform.Find("InvntoryInforPanel");
        btnPanel     = OtherInforPannelManager.instance.transform.Find("BtnOfBuy&Sale&Other");
       inventoryImg = GetComponent<Image>();
        //   this.gameObject.name = inventoryInforOfThisItem.Name;
        //取消该功能面板
        btnPanel.Find("Cancle").GetComponent<Button>().onClick.AddListener(() => { CloseInventoryInforPanel(1); });
    }

    public void CreateInventory( InventoryStoreState inventoryStoreState , InventoryInfor infor)
    {
        this.inventoryStoreState = inventoryStoreState;
   
        this.inventoryInforOfThisItem  = infor;
        
        this.name = infor.Name;
    }
    #region 道具的位置定义
    public void  CheckIsInBackage  ()
    {
    
        inventoryStoreState = InventoryStoreState.Backage;
    }
    public void  CheckIsInShop     ()
    {
     
        inventoryStoreState = InventoryStoreState.Shop;
    }
    public void  CheckIsInMonster  ()
    {
        inventoryStoreState = InventoryStoreState.Monster;
    }
    public void  CheckIsInEquiped  ()
    {
        inventoryStoreState = InventoryStoreState.BeEquiped;
    }
    public void CheckIsInGround    ()
    {
        inventoryStoreState = InventoryStoreState.Ground;
    }
    #endregion

    #region 显示：数据显示
    //填充方式信息
    void FillInforToPanel()
    {
        inforPanel.GetChild(0).GetComponent<Image>().sprite = inventoryImg.sprite;

        inforPanel.GetChild(1).GetComponent<Text>().text = inventoryInforOfThisItem.Des;

    }

    public   void   Refesh()
    {
        switch (inventoryStoreState)
        {
            case InventoryStoreState.Backage:
                transform.GetChild(0).GetComponent<Text>().text = "个数:" + inventoryInforOfThisItem.Count;
                break;
            case InventoryStoreState.Shop   :
                transform.GetChild(0).GetComponent<Text>().text = "价格为:" + inventoryInforOfThisItem.Price;
                break;
        }
        }

    //鼠标进入装备的UI ,那么久显示此装备的信息
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        // 数据填充
        FillInforToPanel();
        //显示道具的信息
        ShowInventoryInforPanel(0,pos);

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {

           CloseInventoryInforPanel(0);
                   
    }
    #endregion
    #region 出现装备信息面板的出现方式定义：（带填充）

    //显示此装备信息面板 ，出现方式默认为为滑动
    void ShowInventoryInforPanel(int index,Vector3 pos , OpenOrCloseWay Way = OpenOrCloseWay.TRANSFORM)
    {
        switch(Way)
        {
            case OpenOrCloseWay.TRANSFORM:
                // inforPanel.GetComponent<PanelController>().ShowPanel(pos);
                OtherInforPannelManager.instance.transform.GetChild(index).GetComponent<PanelController>().ShowPanel(pos);
                break;
            case OpenOrCloseWay.TRANSPARENTS:

                break;
            case OpenOrCloseWay.SCALE:

                break;
        }
       
    }

    //关闭此装备的信息面板 ： Index为几号面板 出现方式默认为为滑动
    void CloseInventoryInforPanel(int index, OpenOrCloseWay Way = OpenOrCloseWay.TRANSFORM)
    {
        switch (Way)
        {
            case OpenOrCloseWay.TRANSFORM:
                OtherInforPannelManager.instance.transform.GetChild(index).GetComponent<PanelController>().HidePanel();
                break;
            case OpenOrCloseWay.TRANSPARENTS:


                break;
            case OpenOrCloseWay.SCALE:

                break;
        }

    }



    #endregion

    #region 对装备装备：点击为使用/购买/脱下装备
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //关闭信息面板
        CloseInventoryInforPanel(0);
        //显示功能面板
        ShowInventoryInforPanel(1, Input.mousePosition);

        switch (inventoryStoreState)
        {
            case InventoryStoreState.Shop:
                btnPanel.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                btnPanel.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                    //在商店中点击为购买此物品:创建此物品的复制体，然后在放入背包
                    GameObject obj = Instantiate(this.gameObject);
                    obj.name       = this.gameObject.name;
                    obj.GetComponent<Inventory>().inventoryInforOfThisItem = this.inventoryInforOfThisItem.Clone() as InventoryInfor;
                    Player.GetComponent<Backage>().BuyInventoryInStore(obj);
                    print("买东西");
                    CloseInventoryInforPanel(1);
                });

                //显示买
                btnPanel.GetChild(0).gameObject.SetActive(true);
                btnPanel.GetChild(3).gameObject.SetActive(false);
                btnPanel.GetChild(1).gameObject.SetActive(false);
                btnPanel.GetChild(2).gameObject.SetActive(false);
                break;
            //case InventoryStoreState.Monster:

            //    break;

            //在地面，捡起
            //case InventoryStoreState.Ground:

            //    break;

            case InventoryStoreState.Backage:

                btnPanel.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                btnPanel.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                    print("卖东西");
                    Player.GetComponent<Backage>().SaleTheInventory(this.name);
                    CloseInventoryInforPanel(1);
                });
                btnPanel.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
                btnPanel.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                {
                    //在背包中为使用此物品
                    CloseInventoryInforPanel(0);
                    print("使用东西");
                    Player.GetComponent<Backage>().UseTheInventory(this.gameObject.name);
                    CloseInventoryInforPanel(1);
                   
                });

                //显示卖
                btnPanel.GetChild(1).gameObject.SetActive(true);
                //显示装备
                btnPanel.GetChild(2).gameObject.SetActive(true);

                btnPanel.GetChild(0).gameObject.SetActive(false);
                btnPanel.GetChild(3).gameObject.SetActive(false);
                break;


            case InventoryStoreState.BeEquiped:
                btnPanel.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                btnPanel.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                {
                    Player.GetComponent<PlayerInfor>().UnEquipTheInvenmtory(inventoryInforOfThisItem.Equiptype);
                    CloseInventoryInforPanel(1);
                });
                //卸下装备
                btnPanel.GetChild(3).gameObject.SetActive(true);
                btnPanel.GetChild(0).gameObject.SetActive(false);
                btnPanel.GetChild(1).gameObject.SetActive(false);
                btnPanel.GetChild(2).gameObject.SetActive(false);
                break;

        }
    }

    #endregion

}
