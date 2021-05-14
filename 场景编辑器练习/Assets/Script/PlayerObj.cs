using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家类
/// </summary>
public class PlayerObj : ObjectBase
{
    /// <summary>
    /// 玩家的数据类
    /// </summary>
    public PlayerDate info;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="date"></param>
    public PlayerObj(PlayerDate date)
    {
        info = date;
    }
    /// <summary>
    /// 创建
    /// </summary>
    public override void OnCreate()
    {
        //角色添加显示面板
        m_pate = game.AddComponent<UIPate>();
        //显示面板初始化
        m_pate.InitPate(1);
        //设置显隐
        m_pate.m_gather.SetActive(false);
        //设置血量和蓝量
        m_pate.SetData(info.name, info.hp / info.MaxHp, (float)info.mp / (float)info.MaxMp,info.type);

        base.OnCreate();
    }
    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="vec"></param>
    public override void SetPos(Vector3 vec)
    {
        base.SetPos(vec);
    }
}
/// <summary>
/// 自己的角色
/// </summary>
public class HostPlayer : PlayerObj
{
    Player player;
    public HostPlayer(PlayerDate date) : base(date)
    {
        ModelID = date.ID;
    }
    public override void CreateObj(RoleType type)
    {
        SetPos(info.pos);
        base.CreateObj(type);
    }
    public override void OnCreate()
    {

        player = game.AddComponent<Player>();
        player.Init();

        //atkActionPlay
        MsgCenter.Instance.AddListener("atkActionPlay", (notify) => {
            if (notify.msg.Equals("ByServer"))
            {
                int skillid = (int)notify.data[0];
                player.SetData(skillid.ToString());
                player.play();
            }

        });
        base.OnCreate();
    }
    Notification notification = new Notification();
    /// <summary>
    /// 玩家移动的重写
    /// </summary>
    /// <param name="h">X轴</param>
    /// <param name="v">Y轴</param>
    public void Moving(float h, float v)
    {
        PlayerMove(new Vector3(game.transform.position.x + h, game.transform.position.y, game.transform.position.z + v), Vector3.forward * Time.deltaTime * 1);
        notification.Refresh("Player", game.transform.position);
        MsgCenter.Instance.SendMsg("MovePos", notification);
    }
    /// <summary>
    /// 按钮点击事件
    /// </summary>
    /// <param name="name"></param>
    public void JoyButtonHandler(string name)
    {
        List<SkillDate> dates;
        switch(name)
        {
            case "Attack":
                Notification notification = new Notification();
                //向服务器发消息谁攻击了，攻击了谁，释放的技能是那个
                notification.Refresh("atkOther", 1, 2, 1);
                MsgCenter.Instance.SendMsg("ByClent_Battle", notification);
                break;
        }

    }
}
/// <summary>
/// 其他玩家
/// </summary>
public class OtherPlayer : PlayerObj
{
    public OtherPlayer(PlayerDate date) : base(date)
    {
        ModelID = date.ID;
    }

    public override void CreateObj(RoleType type)
    {
        SetPos(info.pos);
        base.CreateObj(type);
    }
}
