﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class LocalProps
{
    public static Dictionary<long, SPlayer> players = new Dictionary<long, SPlayer>();


}

public class ServerInit : MonoBehaviour
{
    public Vector3 m_playerPos;
    public Dictionary<int, Vector3> m_otherPosDic;

    private void Awake()
    {
        MsgCenter.Instance.AddListener("MovePos", (notify) =>
        {
            if (notify.msg.Equals("Player"))
            {
                m_playerPos = (Vector3)notify.data[0];
            }
            else if (notify.msg.Equals("Other"))
            {
                if (m_otherPosDic == null)
                {
                    m_otherPosDic = new Dictionary<int, Vector3>();
                }
                int insid = (int)notify.data[0];
                Vector3 pos = (Vector3)notify.data[1];
                if (!m_otherPosDic.ContainsKey(insid))
                {
                    m_otherPosDic.Add(insid, pos);
                }
                else
                {
                    m_otherPosDic[insid] = pos;
                }
            }

        });

        MsgCenter.Instance.AddListener("ServerMsg", (notify) =>
        {
            if (notify.msg.Equals("gather"))
            {
                Debug.Log($"点击采集按钮  Insid：{(int)notify.data[0]}");
                int insid = (int)notify.data[0];

                notify.Refresh("gather_callback", insid);
                MsgCenter.Instance.SendMsg("ServerMsg", notify);
            }
        });
        // 初始化 完所有角色，给所有角色相关组件注册回调

        //解析配置表 初始化角色信息
        SPlayer splayer = new SPlayer();
        splayer.InitPlayer();
        splayer.m_insid = 1;
        splayer.Hp = 100;

        splayer.components.Add(ComponentType.battle,new BattleComponent());
        //splayer.components.Add(ComponentType.task, new TaskComponent());

        LocalProps.players.Add(splayer.m_insid, splayer);

        if (LocalProps.players == null) return;
        foreach (var item in LocalProps.players)
        {
            foreach (var item1 in item.Value.components)
            {
                item1.Value.GetPlayerById = GetPlayer;
                item1.Value.Init();
            }
        }
    }

    private SPlayer GetPlayer(long id)
    {
        using (var tmp = LocalProps.players.GetEnumerator())
        {
            while (tmp.MoveNext())
            {
                if (tmp.Current.Key == id)
                {
                    return tmp.Current.Value;
                }
            }
        }
        return null;
    }

}

public enum ComponentType:byte
{
    nil =0,
    task =1,
    battle = 1,

}

public class SkillProp
{
   public float range;
    //范围、攻击力、时长、触发节点
}

public class SPlayer
{
    public long m_insid;

    public Vector3 m_pos;
    public float Hp;
    public float Mp;
    public float Atk;
    //
    public List<int> buffs;
    public List<SkillProp> skills;

    public Dictionary<ComponentType, SComponent> components;


    public void InitPlayer()
    {
        buffs = new List<int>();
        skills = new List<SkillProp>();
        components = new Dictionary<ComponentType, SComponent>();
    }

    public void PropOperation(int type, float value)
    {
        switch (type)
        {
            case 1:
                Hp += value;
                break;
            case 2:
                Mp += value;
                break;
        }
        Notification m_notify = new Notification();
        m_notify.Refresh("ByServer", type,value);
        MsgCenter.Instance.SendMsg("propchange", m_notify);
    }


}

public class SComponent
{
   public  Func<long, SPlayer> GetPlayerById;

    Notification m_notify;
    public virtual void S2CMsg(string cmd,object value)
    {
        Debug.Log("是什么消息 =====" + cmd);
        if (m_notify == null)
        {
            m_notify = new Notification();
        }
        m_notify.Refresh("ByServer",value);
        MsgCenter.Instance.SendMsg(cmd, m_notify);
    }

    public virtual void Init()
    { }
}

public class TaskComponent: SComponent
{
    public List<int> Tasks;
    //

    public override void Init()
    {
        MsgCenter.Instance.AddListener("ByClent_Task", (notify) => {
            if (notify.msg.Equals("over"))
            {
                int id = (int)notify.data[0];

                S2CMsg("over", id);
                //完成一条
            }
            if (notify.msg.Equals("get"))
            {
                int id = (int)notify.data[0];
            }
        });
    }
    
    
}

public class BattleComponent: SComponent
{
    public override void Init()
    {
        MsgCenter.Instance.AddListener("ByClent_Battle", (notify) => {
            if (notify.msg.Equals("atkOther"))
            {
                int atkId = (int)notify.data[0];
                int targetID = (int)notify.data[1];
                int skillID = (int)notify.data[2];

                AtkPlayer(atkId,targetID,skillID);
            }
           
        });
    }

    private void AtkPlayer(long atk, long target,int skillid)
    {
        SPlayer p1 = GetPlayerById(atk);
        SPlayer p2 = GetPlayerById(target);
        //==实际逻辑为下方逻辑
        //float tmp = p1.skills[skillid].range;
        //判断攻击距离  修改伤害数据
        //if (tmp >= Vector3.Distance(p1.m_pos,p2.m_pos))
        //{
        //    p2.PropOperation(1, -10);
        //    S2CMsg("atkover", true);
        //    S2CMsg("shouji", p2.m_insid);
        //}
        //===临时消息
        S2CMsg("atkActionPlay", skillid);
        //S2CMsg("shouji", p2.m_insid);
    }
}

