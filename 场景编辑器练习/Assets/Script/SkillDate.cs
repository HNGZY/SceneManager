using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能播放的基类
/// </summary>
public class SkillDate : MonoBehaviour
{
    public string name = string.Empty;
    /// <summary>
    /// 播放的方法
    /// </summary>
    public virtual void Play()
    {

    }
    /// <summary>
    /// 初始化的方法
    /// </summary>
    public virtual void Init()
    {

    }
    /// <summary>
    /// 停止播放的方法
    /// </summary>
    public virtual void Syop()
    {

    }
}
