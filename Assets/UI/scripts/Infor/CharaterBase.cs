using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 角色基类：
///  1.姓名
/// 
///  2.战斗属性
///  
///  3.技能属性
///  
///  4. 级别 
///  。。。。。。
///  
/// </summary>


namespace CharaterManager
{

   //简单列举有玩家，龙，骷髅，宠物
  public enum CharaterType
    {
        Player = 1 ,Dragon,Skeleton,Pet
    }

  public abstract class CharacterBase 
    {

        //角色类型
        protected  CharaterType charaterType;
        new protected string    name;
        public    string    Name { get { return name; } }
        
        //物理伤害
        protected ushort ad;
        virtual public    ushort AD { get { return ad; } }
        
        //法术伤害
        protected ushort ap;
        virtual public ushort AP { get { return ap; } }
        
        //级别
        protected ushort level;
        public    ushort Level { get { return level; } }
        
        //升级的跨度：1表示在原来的级别上+1
        public    abstract void AddLevel(ushort number);

        //血量：基础为10
        protected ushort hp;
        virtual public ushort Hp { get { return       hp; } }
        
        //number表示加血量
        public abstract void AddHp(ushort number);
        
        //蓝量 ：基础为10
        protected ushort mp = 10;
        virtual public ushort Mp { get { return mp; } }
        
        //number表示加血量 
        public    abstract void AddMp(ushort number);

        //经验 
        protected ushort exp ;
        virtual public ushort Exp { get { return exp; } }
        public abstract void AddExp(ushort number);

        //移动速度
        protected ushort speedOfMove;
        virtual public ushort SpeedOFMove { get { return speedOfMove; } }
        //防御力：
        protected ushort defence;
        virtual public ushort Defence { get { return defence; } }

        //人物创建的时指定的人物信息
        public CharacterBase(string name, ushort level, CharaterType charaterType)
        {
            this.charaterType = charaterType;
            this.name  = name ;
            this.level = level;
        }

    }
}
