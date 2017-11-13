using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引入人物管理
using CharaterManager;
using System;
/// <summary>
/// 在此处填写数值
/// </summary>


public enum PlayerType
{
    Warrior,Shooter,Master
}

public class PlayerInfor : MonoBehaviour
{ 

    //玩家类型
    private PlayerType playerType;
    //基本属性
    public PlayerAttri playerAttribute = new PlayerAttri("啥呢提案", 0, CharaterType.Player);

    public void CreatePlayer(PlayerAttri playerAttribute)
    {
        this.playerAttribute = playerAttribute;
    }

    //人物现在使用的穿戴装备 :已经穿戴上的装备
    Dictionary<EquipType, GameObject> EquipedInventory = new Dictionary<EquipType, GameObject>();

    public GameObject GetEquipedInfor(EquipType type) {
        if (EquipedInventory.ContainsKey(type))
        return     EquipedInventory[type];
        return null; 
    }


    //穿戴
    public void EquipTheInvenmtory(GameObject equip)
    {
        
        EquipType type = equip.GetComponent<Inventory>().inventoryInforOfThisItem.Equiptype;

        if (type != EquipType.Prop)
        {
            //若身上具有此物品
            if (EquipedInventory.ContainsKey(type))
            {
                //1.脱下当前的装备（默认放入背包中）
                UnEquipTheInvenmtory(type);
                //2.装上新的物品
                EquipedInventory.Add(type, equip);
                //3.设置当前装备所在位置
                equip.GetComponent<Inventory>().CheckIsInEquiped();
            }
            //没有此物品
            else
            {
                EquipedInventory.Add(type, equip);
            }
        }
    }
    //脱下此装备
    public void UnEquipTheInvenmtory(EquipType  EquipType)
    {
        GameObject Item = EquipedInventory[EquipType];
        EquipedInventory.Remove(EquipType);
        //3.设置当前装备所在位置
        Item.GetComponent<Inventory>().CheckIsInBackage();
        //放入到背包中
        GetComponent<Backage>().PickUpInventory(Item);
        
    }


}
