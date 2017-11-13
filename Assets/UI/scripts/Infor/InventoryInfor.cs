using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// 对每个装备的信息的描述
/// </summary>
//public enum InventoryType
//{
//    Equip
    
//}

public enum EquipType
{
    NULL,
    Weapon,//武器
    Necklace,//项链
    Cloth,//衣服
    Ring, //戒指
    Prop  //其他道具：蓝，血
}

public class InventoryInfor : ICloneable{

    private int       id;//ID
    private string    name;//名字
    private string    icon;//图标

    private EquipType equiptype;//装备类型
    private int       quality = 1;//品质；
    private int       damage  = 0;//伤害
    private int       hp      = 0;//血量
    private int       power   = 0;//战斗力
    private int       mp      = 0;//战斗力
    private string    des;//描述
    private int       price;
    private int       count = 1;

    //Image 对象，不是组件
    //private GameObject UIInvertary;
   
    public InventoryInfor (int id, string name, string icon, EquipType equiptype, int damage, int hp, int power, string des,int price)
    {
        this.price = price;
        this.id          = id;
        this.icon        = icon;
        this.equiptype   = equiptype;
        this.damage      = damage;
        this.hp          = hp;
        this.power       = power;
        this.des         = des;
   //     this.UIInvertary = UIInvertary;
        this.name = name;
    }
    public int Mp { get { return mp; } }
    public int Price
    { get { return price; } }
    public int Count { get { return count; } }
    public int UsedTheInventory()
    {

        if(count>0)
          count--;
        return count;
    }
    public void AddInventaryCount(int addNumber)
    {
        count += addNumber;
    }

    public int ID
    {
        get { return id; }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string Icon
    {
        get
        {
            return icon;
        }

    }

    public EquipType Equiptype
    {
        get
        {
            return equiptype;
        }
    }
    
    public int Quality
    {
        get
        {
            return quality;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }
    
    public int Power
    {
        get
        {
            return power;
        }

    }

    public string Des
    {
        get
        {
            return des;
        }
    }
    
    public int Hp
    {
        get
        {
            return hp;
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
