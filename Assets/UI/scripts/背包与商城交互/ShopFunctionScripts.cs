using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店的功能 ：
///       加载资源到商店
///       买入      
/// </summary>
public class ShopFunctionScripts : MonoBehaviour {
    //商品的清单 ：content
    Transform      m_GoodsContent;
    //物品架
    public GameObject m_EmptyGoodsShell;

    private void Start()
    {
        m_GoodsContent = transform.GetComponentInChildren<ScrollRect>().content;      
        //商城数据刷新
        GetComponent<PanelController>().RefeshTheInforToPanelEvent += RefeshPannel;
    }


    void RefeshPannel()
    {

    }

    //     加载资源 ：必须具有商品所携带的Inventory 功能
    public void LoadAnGoodsToShop(GameObject inventory)
    {
      if(inventory.GetComponent<Inventory>())
        {
            inventory.name = inventory.GetComponent<Inventory>().inventoryInforOfThisItem.Name;
            //   print("已经加载");
            GameObject shell = CreateGoodsShell();
            shell.name = inventory.name;
            inventory.transform.SetParent(shell.transform);
            inventory.transform.localPosition = Vector3.zero;
            inventory.transform.localScale = shell.transform.localScale;
            inventory.GetComponent<Inventory>().CheckIsInShop();
            inventory.GetComponent<Inventory>().Refesh();
        }

    }

    GameObject  CreateGoodsShell()
    {
        GameObject shell = Instantiate(m_EmptyGoodsShell, m_GoodsContent, false);
        shell.name = m_EmptyGoodsShell.name;
        return shell;
    }

    public void LoadAllGoodsToShop(List<GameObject> InventoryList)
    {
        if (InventoryList[0].GetComponent<Inventory>())
         for (int i =0;i < InventoryList.Count;i++)
        {
                InventoryList[i].GetComponent<Inventory>().CheckIsInShop();
            GameObject shell = CreateGoodsShell();
               
                InventoryList[i].transform.SetParent(shell.transform);
                InventoryList[i].transform.localPosition = Vector3.zero;
                shell.name = InventoryList[i].name;
                InventoryList[i].GetComponent<Inventory>().Refesh();
            }

    }
    }
