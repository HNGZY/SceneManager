﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MonsterType
{
    Null= 0,
    Normal, //怪物
    Gather, //采集物
    Biaoche, //跟随物
    NPC,
}

/*
具体种类：怪物 
*/
public class Monster : ObjectBase
{
    public monster_info m_info;

    public Monster(MonsterType type,monster_info info)
    {
        info.m_type = type;
        m_info = info;
        m_insID = info.ID;
        m_modelPath = info.m_res;
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }
}

//正常怪物
public class Normal : Monster
{
    public Normal( monster_info info) 
        : base(MonsterType.Normal,info)
    {
    }

    public Normal(object_info info):
        base(MonsterType.Normal,new monster_info(MonsterType.Normal, info))
    {
    }

    public override void CreateObj(MonsterType type)
    {
        SetPos(m_info.m_pos);
        base.CreateObj(type);
    }
    
    public override void OnCreate()
    {
        base.OnCreate();

        //FSM  

        m_pate = m_go.AddComponent<UIPate>();
        m_pate.InitPate();

        m_pate.m_name.gameObject.SetActive(false);
        m_pate.m_hp.gameObject.SetActive(false);
        m_pate.m_mp.gameObject.SetActive(false);
        m_pate.m_gather.gameObject.SetActive(true);
    }
}

//采集物
public class Gather : Monster
{
    public Gather(monster_info info) 
        : base(MonsterType.Gather, info)
    {
    }
    public Gather(object_info info) :
        base(MonsterType.Gather, new monster_info(MonsterType.Gather, info))
    {
    }

    public override void CreateObj(MonsterType type)
    {
        SetPos(m_info.m_pos);
        base.CreateObj(type);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        StaticCircleCheck check = m_go.AddComponent<StaticCircleCheck>();
        check.m_taget = World.Instance.m_plyer.m_go;
        check.m_call = (isenter) =>
        {
            Debug.Log(string.Format("主角触发了我,我是{0}", m_info.m_res));
            Notification notify = new Notification();
            notify.Refresh("gather_trigger", m_info.ID);
            MsgCenter.Instance.SendMsg("ClientMsg", notify);
        };

        m_pate = m_go.AddComponent<UIPate>();
        m_pate.InitPate();

        m_pate.m_name.gameObject.SetActive(false);
        m_pate.m_hp.gameObject.SetActive(false);
        m_pate.m_mp.gameObject.SetActive(false);
        m_pate.m_gather.gameObject.SetActive(true);

    }


    public void RefreshGatherCount(int cnt)
    {
        if (m_pate && m_pate.m_gathers.Count>0)
        {
            for (int i = 0; i < m_pate.m_gathers.Count; i++)
            {
                m_pate.m_gathers[i].gameObject.SetActive(cnt <= i + 1);
            }
        } 
    }

}

public class Biaoche : Monster
{
    public Biaoche(monster_info info) 
        : base(MonsterType.Biaoche, info)
    {
    }
    public Biaoche(object_info info) :
       base(MonsterType.Biaoche, new monster_info(MonsterType.Biaoche, info))
    {
    }
    public override void CreateObj(MonsterType type)
    {
        SetPos(m_info.m_pos);
        base.CreateObj(type);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        StaticCircleCheck check = m_go.AddComponent<StaticCircleCheck>();
        check.m_call = (isenter) =>
        {
            Debug.Log("进入检测范围");
            //follow跟随
        };
    }
}

