using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景物体的公共属性类
/// </summary>
public class ObjectDate
{
    /// <summary>
    /// 物体ID
    /// </summary>
    public int ID;
    /// <summary>
    /// 名字
    /// </summary>
    public string name;
    /// <summary>
    /// 物体的位置
    /// </summary>
    public Vector3 pos;
    /// <summary>
    /// 物体的类型
    /// </summary>
    public RoleType type;
}
/// <summary>
/// 玩家的数据类
/// 继承于物体公共数据类
/// </summary>
public class PlayerDate : ObjectDate
{
    /// <summary>
    /// 人物等级
    /// </summary>
    public int level;
    /// <summary>
    /// 人物当前血量
    /// </summary>
    public int hp;
    /// <summary>
    /// 人物最大血量
    /// </summary>
    public int MaxHp;
    /// <summary>
    /// 人物当前魔法值
    /// </summary>
    public int mp;
    /// <summary>
    /// 人物最大魔法值
    /// </summary>
    public int MaxMp;
    /// <summary>
    /// 人物攻击类
    /// </summary>
    public int ATK;
    /// <summary>
    /// 人物技能列表
    /// </summary>
    public List<SkillDate> skills;
}
/// <summary>
/// 怪物的数据类
/// 继承于物体公共数据类
/// </summary>
public class MonsterDate : ObjectDate
{
    /// <summary>
    /// 怪物等级
    /// </summary>
    public int level;
    /// <summary>
    /// 怪物攻击力
    /// </summary>
    public int ATK;
    public MonsterDate(RoleType roleType,ObjectDate date)
    {
        //ID
        ID = date.ID;
        //名字
        name = date.name;
        //位置
        pos = date.pos;
        //类型
        type = roleType;
    }
}
/// <summary>
/// NPC数据类
/// 继承于物体公共数据类
/// </summary>
public class NpcDate : ObjectDate

{    public NpcDate(int roleType,ObjectDate date)
    {
        //ID
        ID = date.ID;
        //名字
        name = date.name;
        //位置
         pos = date.pos;
        //类型
        type = RoleType.NPC;
    }
}
