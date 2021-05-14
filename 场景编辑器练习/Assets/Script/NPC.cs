using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : ObjectBase
{
    public NpcDate info;
    public NPC(NpcDate date)
    {
        info = date;
        ModelID = date.ID;
    }
    public NPC(int plot, ObjectDate date)
    {
        info = new NpcDate(plot, date);
        ModelID = date.ID;
    }
    /// <summary>
    /// 从写创建的方法
    /// </summary>
    /// <param name="type"></param>
    public override void CreateObj(RoleType type)
    {
        //设置位置
        SetPos(info.pos);
        base.CreateObj(type);
    }
}
