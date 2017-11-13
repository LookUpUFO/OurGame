using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharaterManager;
using System;

public class PlayerAttri : CharacterBase
{
    public PlayerAttri(string name, ushort level, CharaterType charaterType) : base(name, level, charaterType)
    {

    }

    public override void AddExp(ushort number)
    {
        exp += number;
    }

    public override void AddHp(ushort number)
    {
        hp += number;
    }

    public override void AddLevel(ushort number)
    {
        level += number;
    }

    public override void AddMp(ushort number)
    {
        mp += number;
    }
    public   void  AddAttack(ushort number )
    {
        ad += number;
    }
}
