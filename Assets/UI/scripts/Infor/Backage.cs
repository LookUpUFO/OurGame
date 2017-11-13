
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包的操作：
///   买装备
///   卖装备
///   捡装备
///   用装备   
/// </summary>


public class Backage : MonoBehaviour {
    //当背包中装备发生变化
    public delegate void OnBackItemChangeHandle();
    public event         OnBackItemChangeHandle   OnBackageItemChangeEvent;

    //所拥有的金币数量
    int       coin = 60;
    public    int  Coin { get { return coin; } }

    private   Dictionary<string,GameObject> invertoryDic = new Dictionary<string, GameObject>();

 ///////////////////// //测试

    //public GameObject Prefab;
    //bool once = true;
    //private void Update()
    //{
    //    if (once)
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            GameObject temp = Instantiate(Prefab);
    //            InventoryInfor infor = new InventoryInfor(i, "上天" + i.ToString(), "we", (EquipType)Random.Range(1, 5), i * 10, 12 + i * 2, i * 10, "如何描述：" + i.ToString(), 10 * i);
    //            temp.GetComponent<Inventory>().CreateInventory(InventoryStoreState.Backage, infor);
    //            LoadInventoryToBackage(temp);
             
    //        }
    //        once = false;
    //    }

    //}

    /////数据加载
    public void LoadInventoryToBackage(GameObject inventory )
    {

        if (inventory.GetComponent<Inventory>())
        {
            inventory.GetComponent<Inventory>().CheckIsInBackage();
            AddInventory(inventory);
        }
        OnBackageItemChangeEvent();
    }
    public void LoadInventoryToBackage(List<GameObject> inventorylist)
    {
        for (int i = 0; i < inventorylist.Count; i++)
        {
            inventorylist[i].GetComponent<Inventory>().CheckIsInBackage();
            AddInventory(inventorylist[i]);
        }
    }


    //删除物品：清除内存
       void       RemoveInventory(string name)
    {
        if (invertoryDic.ContainsKey(name))
        {
            
            GameObject temp = invertoryDic[name];
            invertoryDic.Remove(name);
            print("字典还有的装备的个数为：" + invertoryDic.Count);
            Destroy(temp);
        }
    }

       void       RemoveInventory(int id)
    {
        string name = string.Empty;
       foreach(var temp in invertoryDic)
        {
            if( temp.Value.GetComponent<Inventory>().inventoryInforOfThisItem.ID == id)
            {
                name = temp.Key;
                break;
            }
        }
       if(name != string.Empty)
          invertoryDic.Remove(name);
    }
   // 添加物品
      bool        AddInventory(GameObject inver)
    {
        if( null !=inver)
        {
            if (invertoryDic.ContainsKey(inver.GetComponent<Inventory>().inventoryInforOfThisItem.Name))
            {
                invertoryDic[inver.GetComponent<Inventory>().inventoryInforOfThisItem.Name].GetComponent<Inventory>().inventoryInforOfThisItem. AddInventaryCount(1);
                GameObject.Destroy(inver);
                return true;
            }
            else
            {
             
                //背包大小具有限制，检查背包的大小
                if (UIManager.Instance.GetComponentInChildren<BackagePanelInforShow>().BackageSize > invertoryDic.Count)
                {
                    invertoryDic.Add(inver.GetComponent<Inventory>().inventoryInforOfThisItem.Name, inver);
                    
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }

        }
        return false;
    }
   
   //获取单个道具
      GameObject  GetInvertory(string name)
    {
        if (invertoryDic.ContainsKey(name))
        {
            return invertoryDic[name];
        }
        return null;
    }

    //使用装备 ：使用(使用的方式：：：) & 用光/穿戴
    public void UseTheInventory(string name)
    {    
        GameObject uesInventory = invertoryDic[name];
            //为穿戴装备 ： 人物穿戴上
            PlayerInfor playerInfor = GetComponent<PlayerInfor>();

        GameObject equip  = Instantiate(uesInventory);
        equip.GetComponent<Inventory>().inventoryInforOfThisItem = uesInventory.GetComponent<Inventory>().inventoryInforOfThisItem.Clone() as InventoryInfor;

        equip.name       = uesInventory.name;


        equip.GetComponent<Inventory>().CheckIsInEquiped();
            playerInfor.EquipTheInvenmtory(equip);

           int count = UseInventory(uesInventory);
           if (count <= 0)
         {
            uesInventory.transform.parent.name = "Empty";
            RemoveInventory(name);          
          //  Destroy(uesInventory);
          }

    }

    //对装备的属性加成：若他为装备：那么就跟换面板上的装备
    int  UseInventory(GameObject uesInventory)
    {
        InventoryInfor inve = uesInventory.GetComponent<Inventory>().inventoryInforOfThisItem;
        int count = uesInventory.GetComponent<Inventory>().inventoryInforOfThisItem.UsedTheInventory();

        PlayerInfor playerInfor = GetComponent<PlayerInfor>();
        playerInfor.playerAttribute.AddMp((ushort)inve.Mp);
        playerInfor.playerAttribute.AddHp((ushort)inve.Hp);
        playerInfor.playerAttribute.AddAttack((ushort)inve.Damage);

        return count;
    }

    //买入装备
    public void BuyInventoryInStore(GameObject goods)
    {

        if (goods.GetComponent<Inventory>().inventoryInforOfThisItem.Price <= coin)
        {
            coin -= goods.GetComponent<Inventory>().inventoryInforOfThisItem.Price;
            goods.GetComponent<Inventory>().CheckIsInBackage();
            bool isSuccess = AddInventory(goods);
            
            OnBackageItemChangeEvent();
        }
        else
        {
            print("没钱，买不起,所以我销毁他了哟");
            GameObject.Destroy(goods);
        }
    }
    //捡到装备
    public void PickUpInventory(GameObject goods)
    {
      
        bool isSuccess = AddInventory(goods);
        goods.GetComponent<Inventory>().CheckIsInBackage();

        print("捡到成功:" + isSuccess);
        OnBackageItemChangeEvent();
    }
    //卖出装备
    public void  SaleTheInventory(string goodsName)
    {
        print(goodsName);
        GameObject goods =  GetInvertory(goodsName);
        print(goods.GetComponent<Inventory>());

        coin += goods.GetComponent<Inventory>().inventoryInforOfThisItem.Price;
        
        //减少装备的个数：当小于0便摧毁它
        goods.GetComponent<Inventory>().inventoryInforOfThisItem.AddInventaryCount(-1);
        if (goods.GetComponent<Inventory>().inventoryInforOfThisItem.Count <= 0)
        {
            goods.transform.parent.name = "Empty";
            RemoveInventory(goodsName);

        }
        OnBackageItemChangeEvent();
    }

    public void SaleTheInventory(GameObject goods)
    {
        RemoveInventory(goods.name);
        goods.GetComponent<Inventory>().CheckIsInShop();

    }
    // 获取所有的道具
    public List<GameObject> GetAllInvertory()
    {
        List<GameObject> list = new List<GameObject>();

        foreach (var temp in invertoryDic.Values)
            list.Add(temp);
    
        return list;
    }

}
