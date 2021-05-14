//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///*
//具体种类：怪物 
//*/
//public class Monster : ObjectBase
//{
//    public MonsterDate m_info;

//    public Monster(RoleType type, MonsterDate info)
//    {
//        info.type = type;
//        m_info = info;
//        ModelID = info.ID;
//    }

//    public override void OnCreate()
//    {
//        base.OnCreate();
//    }
//}

/////正常怪物
//public class Normal : Monster
//{
//    public Normal(MonsterDate info)
//        : base(RoleType.Normal, info)
//    {
//    }

//    public Normal(ObjectDate info) :
//        base(RoleType.Normal, new MonsterDate(RoleType.Normal, info))
//    {
//    }

//    public override void CreateObj(RoleType type)
//    {
//        SetPos(m_info.pos);
//        base.CreateObj(type);
//    }

//    public override void OnCreate()
//    {
//        base.OnCreate();

//        //FSM  

//        m_pate = game.AddComponent<UIPate>();
//        m_pate.InitPate();

//        m_pate.m_name.gameObject.SetActive(false);
//        m_pate.m_hp.gameObject.SetActive(false);
//        m_pate.m_mp.gameObject.SetActive(false);
//        m_pate.m_gather.gameObject.SetActive(true);
//    }
//}

////采集物
//public class Gather : Monster
//{
//    public Gather(MonsterDate info)
//        : base(RoleType.Gather, info)
//    {
//    }
//    //public Gather(MonsterDate info) :
//    //    base(RoleType.Gather, new MonsterDate(RoleType.Gather, info))
//    //{
//    //}

//    //public override void CreateObj(RoleType type)
//    //{
//    //    SetPos(m_info.pos);
//    //    base.CreateObj(type);
//    //}

//    //public override void OnCreate()
//    //{
//    //    base.OnCreate();
//    //    StaticCircleCheck check = game.AddComponent<StaticCircleCheck>();
//    //    check.m_taget = World.Instance.m_plyer.m_go;
//    //    check.m_call = (isenter) =>
//    //    {
//    //        Debug.Log(string.Format("主角触发了我,我是{0}", m_info.m_res));
//    //        Notification notify = new Notification();
//    //        notify.Refresh("gather_trigger", m_info.ID);
//    //        MsgCenter.Instance.SendMsg("ClientMsg", notify);
//    //    };

//    //    m_pate = m_go.AddComponent<UIPate>();
//    //    m_pate.InitPate();

//    //    m_pate.m_name.gameObject.SetActive(false);
//    //    m_pate.m_hp.gameObject.SetActive(false);
//    //    m_pate.m_mp.gameObject.SetActive(false);
//    //    m_pate.m_gather.gameObject.SetActive(true);

//    //}


//    //public void RefreshGatherCount(int cnt)
//    //{
//    //    if (m_pate && m_pate.m_gathers.Count>0)
//    //    {
//    //        for (int i = 0; i < m_pate.m_gathers.Count; i++)
//    //        {
//    //            m_pate.m_gathers[i].gameObject.SetActive(cnt <= i + 1);
//    //        }
//    //    } 
//    //}

//}


