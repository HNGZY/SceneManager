using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : SingleTon<World>
{
    /// <summary>
    /// 一个库
    /// 存放角色
    /// </summary>
    public Dictionary<int, ObjectBase> roleDic = new Dictionary<int, ObjectBase>();
    /// <summary>
    /// 玩家自己的类对象
    /// </summary>
    public HostPlayer player;
    /// <summary>
    /// NPC
    /// </summary>
    private GameObject npc;
    /// <summary>
    /// 相机
    /// </summary>
    public Camera camera;
    /// <summary>
    /// 长
    /// </summary>
    public float xLength;
    /// <summary>
    /// 宽
    /// </summary>
    public float yLength;
    /// <summary>
    /// 初始化的方法
    /// </summary>
    public void Init()
    {
        //找到地图
        GameObject plan = GameObject.Find("Plane");
        //获得地图的尺寸
        Vector3 length = plan.GetComponent<MeshFilter>().mesh.bounds.size;
        xLength = length.x * plan.transform.localScale.x;
        yLength = length.z * plan.transform.localScale.z;
        Debug.Log($"地图的尺寸为 x:{xLength}   y:{yLength}");

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        npc = GameObject.Find("NPC_Root");
        UIMgr.Instance.Init(GameObject.Find("UIRoot"), GameObject.Find("HUD"));
        //给玩家赋值属性
        PlayerDate info = new PlayerDate();
        info.ID = 1;//ID
        info.name = "Teddy";//名字
        info.level = 99;//等级
        info.pos = Vector3.zero;//位置
        info.hp = 2000;//当前血量
        info.mp = 1500;//当前法力值
        info.MaxHp = 2000;//最大血量
        info.MaxMp = 2000;//最大法力值
        //把玩家的属性传到玩家自己的脚本身上
        player = new HostPlayer(info);
        //创建对应的角色
        player.CreateObj(RoleType.Null);
        JoyStickMgr.Instance.SetJoyArg(camera, player);
        JoyStickMgr.Instance.JoyActive = true;
        CreaeIns();
        //监听事件---移动
        MsgCenter.Instance.AddListener("AutoMove", (notify) =>
        {
            this.AutoMoveByInsId((int)notify.data[0], (Vector3)notify.data[1]);
        });
        //监听事件---Buff
        MsgCenter.Instance.AddListener("AddBuff" , (notify) =>
        {

        });
    }

    private void CreaeIns()
    {
        JsonData data = MonsterCfg.Instance.GetJsonDate();
        ObjectDate info;
        //遍历josn解析出来的数据
        for (int i = 0; i < data.datas.Count; i++)
        {
            info = new ObjectDate();
            info.ID = roleDic.Count + 1;
            info.name = string.Format("{0} {1}", data.datas[i].name, info.ID);
            info.pos = new Vector3(data.datas[i].x, data.datas[i].y, data.datas[i].z);
            info.type = data.datas[i].type;
            CreateObj(info);
        }
    }
    ObjectBase monster = null;
    private void CreateObj(ObjectDate info)
    {
        monster = null;
        if(info != null)
        {
            if (info.type == RoleType.Normal)
            {
                monster = new Normal(info);
            }
            else if (info.type == RoleType.Gather)
            {
                monster = new Gatner(info);
            }
            else if (info.type == RoleType.NPC)
            {
                monster = new NPC(1, info);
            }
        }
        if (monster != null)
        {
            //创建对应模型
            monster.CreateObj(info.type);
            //设置父物体
            monster.game.transform.SetParent(npc.transform, false);
            roleDic.Add(info.ID, monster);
        }
        else
        {
            Debug.Log("生成失败！");
        }
    }

    public void AutoMoveByInsId(int index,Vector3 pos)
    {

    }
}
