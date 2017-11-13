using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 人物属性面板
///     只具有对人物属性的显示功能
/// </summary>


public class CharaterAttributePanelScripts : MonoBehaviour {
    Image       headImg;
    Transform    neckLacesInventoryShell;
    Transform    RingInventoryShell;
    Transform    ClothesInventoryShell;
    Transform WeapenInventoryShell;
    Transform HpText;
    Transform MpText;
    Transform AdText;
    Transform HurtText;
    Transform DefenceText;
    Transform NameAndLeve;
    PlayerAttri  PlayerAttributeInfo;
    PlayerInfor  playerInfor;
    private void Start()
    {
        Transform charaterAttriPanel = transform.Find("CharaterAttriute");
        headImg                      = charaterAttriPanel.Find("headImage").GetComponent<Image>();
        neckLacesInventoryShell      = charaterAttriPanel.Find("Necklaces");

        RingInventoryShell           = charaterAttriPanel.Find("Ring");
        ClothesInventoryShell        = charaterAttriPanel.Find("Cloth");
        WeapenInventoryShell         = charaterAttriPanel.Find("Weapons");

        DefenceText                  = charaterAttriPanel.Find("zhandouText");
        AdText                       = charaterAttriPanel.Find("HurtText");

        HpText                    = charaterAttriPanel.Find("hpText");
        MpText                    = charaterAttriPanel.Find("mpText");
        NameAndLeve               = charaterAttriPanel.Find("PlayerNameAndLv");
        HurtText                  = charaterAttriPanel.Find("HurtText");
        playerInfor               = GameObject.FindWithTag("Player").GetComponent<PlayerInfor>();
        PlayerAttributeInfo       = playerInfor.playerAttribute;
        //每次调用面板就刷新一次
        GetComponent<PanelController>().RefeshTheInforToPanelEvent += ShowInfor;
    }


   public  void   ShowInfor()
    {
        try
        {
            // headImg = PlayerAttributeInfo.
            DefenceText.GetChild(0).GetComponent<Text>().text = PlayerAttributeInfo.Defence.ToString();
            
            AdText.GetChild(1).GetComponent<Text>().text = PlayerAttributeInfo.AD.ToString();
            HpText.GetChild(1).GetComponent<Text>().text = PlayerAttributeInfo.Hp.ToString();
            MpText.GetChild(1).GetComponent<Text>().text = PlayerAttributeInfo.Mp.ToString();
            HurtText.GetChild(1).GetComponent<Text>().text = PlayerAttributeInfo.AD.ToString();
            NameAndLeve.GetChild(0).GetComponent<Text>().text = PlayerAttributeInfo.Name;

            
            //加载装备
            EquipToPanel(EquipType.Cloth, ClothesInventoryShell);
            EquipToPanel(EquipType.Necklace, neckLacesInventoryShell);
            EquipToPanel(EquipType.Ring, RingInventoryShell);
            EquipToPanel(EquipType.Weapon,WeapenInventoryShell);


        }
        catch(System.Exception exp)
        {
            print(exp);
        }

    }

    //装备上物品
    void EquipToPanel(EquipType type , Transform parent)
    {
        GameObject equiped = playerInfor.GetEquipedInfor(type);
        if (equiped)
        {
            equiped.transform.SetParent(parent);
            equiped.transform.localPosition = Vector3.zero;
            equiped.transform.localScale = parent.transform.localScale;
        }
    }

}
