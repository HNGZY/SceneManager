using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象基类
/// </summary>
public abstract class ObjectBase
{
    /// <summary>
    /// 存储当前物体
    /// </summary>
    public GameObject game;
    /// <summary>
    /// 当前物体位置
    /// </summary>
    public Vector3 rolePos;
    /// <summary>
    /// 当前物体的状态机
    /// </summary>
    public Animator animator;

    public UIPate m_pate;
    public RoleType type1;
    /// <summary>
    /// 模型ID
    /// </summary>
    public int ModelID;
    /// <summary>
    /// 构造函数
    /// </summary>
    public ObjectBase()
    {

    }
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="type"></param>
    public virtual void CreateObj(RoleType type)
    {
        type1 = type;
        if(ModelID > 0 && type1 == RoleType.Null)
        {
            game = GameObject.Instantiate(Resources.Load<GameObject>("Teddy"));
            animator = game.GetComponent<Animator>();
            game.name = "player";
            game.transform.position = rolePos;
            if(game)
            {
                OnCreate();
            }
        }
        else
        {
            game = GameObject.Instantiate(Resources.Load<GameObject>("monster1"));
            game.name = ModelID.ToString();
            game.transform.position = rolePos;
            if (game)
            {
                OnCreate();
            }
        }
    }
    /// <summary>
    /// 生成角色的同时初始化对应的数据
    /// </summary>
    public virtual void OnCreate()
    {

    }
    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="vec"></param>
    public virtual void SetPos(Vector3 vec)
    {
        rolePos = vec;
    }
    /// <summary>
    /// 移动
    /// </summary>
    public virtual void PlayerMove(Vector3 look,Vector3 move)
    {
        //朝向
        game.transform.LookAt(look);
        //移动
        game.transform.Translate(move);
    }
    /// <summary>
    /// 希构造函数
    /// 销毁时调用的方法
    /// </summary>
    public virtual void Destory()
    {
        if(m_pate)
        {
            GameObject.Destroy(m_pate);
        }
        //删除角色
        GameObject.Destroy(game);
        rolePos = Vector3.zero;
        animator = null;
        ModelID = -1;
    }
}
