using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景角色的类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 空类型
    /// </summary>
    Null = 0,
    /// <summary>
    /// 怪物类型
    /// </summary>
    Normal,
    /// <summary>
    /// 采集物
    /// </summary>
    Gather,
    /// <summary>
    /// 跟随物
    /// </summary>
    Follow,
    NPC
}
/// <summary>
/// 怪物的
/// </summary>
public class Monster : ObjectBase
{
    /// <summary>
    /// 怪物的数据类对象
    /// </summary>
    public MonsterDate monsterDate;
    /// <summary>
    /// 构造
    /// </summary>
    public Monster(RoleType type,MonsterDate date)
    {
        ModelID = date.ID;
        monsterDate = date;
        date.type = type;
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }
}

public class Normal : Monster
{
    public Normal(MonsterDate date) : base(RoleType.Gather, date)
    {

    }
    public Normal(ObjectDate date) : base(RoleType.Normal,new MonsterDate(RoleType.Normal,date))
    {

    }
    public override void CreateObj(RoleType type) 
    {
        SetPos(monsterDate.pos);
        base.CreateObj(type);
    }
    public override void OnCreate()
    {
        base.OnCreate();
        //怪物添加UIPate脚本
        m_pate = game.AddComponent<UIPate>();
        //初始化
        m_pate.InitPate(-1);
        m_pate.m_name.gameObject.SetActive(false);
        m_pate.m_hp.gameObject.SetActive(false);
        m_pate.m_mp.gameObject.SetActive(false);
        m_pate.m_gather.gameObject.SetActive(true);
    }
}
/// <summary>
/// 采集物
/// </summary>
public class Gatner : Monster
{
    public Gatner(MonsterDate date) : base(RoleType.Gather,date)
    {

    }
    public Gatner(ObjectDate date) : base(RoleType.Gather, new MonsterDate(RoleType.Gather,date))
    {

    }
    public override void CreateObj(RoleType type)
    {
        SetPos(monsterDate.pos);
        base.CreateObj(type);
    }
    public override void OnCreate()
    {
        base.OnCreate();
        //添加检测脚本
        StaticCircleCheck check = game.AddComponent<StaticCircleCheck>();
        check.m_taget = World.Instance.player.game;
        check.m_call = (isenter) =>
        {
            Notification notification = new Notification();
            notification.Refresh("gather_trigger", monsterDate.ID);
            MsgCenter.Instance.SendMsg("ClientMsg", notification);
        };

        m_pate = game.AddComponent<UIPate>();
        m_pate.InitPate(-1);

        m_pate.m_name.gameObject.SetActive(false);
        m_pate.m_hp.gameObject.SetActive(false);
        m_pate.m_mp.gameObject.SetActive(false);
        m_pate.m_gather.gameObject.SetActive(true);
    }
}

public class Follow : Monster
{
    public Follow(MonsterDate date) : base(RoleType.Follow, date)
    {

    }
    public Follow(ObjectDate date) : base(RoleType.Follow,new MonsterDate(RoleType.Follow,date))
    {

    }
    public override void CreateObj(RoleType type)
    {
        SetPos(monsterDate.pos);
        base.CreateObj(type);
    }
    public override void OnCreate()
    {
        StaticCircleCheck check = game.AddComponent<StaticCircleCheck>();
        check.m_call = (isenter) =>
        {
            Debug.Log("检测到有物体进入检测范围!!!!!!!!!!!");
        };
        base.OnCreate();
    }
}
