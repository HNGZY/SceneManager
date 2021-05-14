using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摇杆管理类
/// </summary>
public class JoyStickMgr : SingleTon<JoyStickMgr>
{
    public GameObject game;
    /// <summary>
    /// 摇杆
    /// </summary>
    public ETCJoystick joystick;
    /// <summary>
    /// 按钮
    /// </summary>
    public List<ETCButton> tCButtons;
    /// <summary>
    /// 玩家自己
    /// </summary>
    HostPlayer player;

    public bool JoyActive
    {
        set
        {
            if (game.activeSelf != value)
            {
                game.SetActive(value);
            }
        }
    }

    public void SetJoyArg(Camera camera, HostPlayer host)
    {
        //玩家自己
        player = host;
        //朝向
        joystick.cameraLookAt = host.game.transform;
        //移动
        joystick.cameraTransform = camera.transform;
        SetJoytick();
    }
    /// <summary>
    /// 控制摇杆的监听事件
    /// </summary>
    private void SetJoytick()
    {
        //判断摇杆和人物是否存在
        if (joystick && player.game)
        {
            Debug.Log("进来了吗，摇杆的");
            joystick.OnPressLeft.AddListener(() => player.Moving(joystick.axisX.axisValue, joystick.axisY.axisValue));
            joystick.OnPressRight.AddListener(() => player.Moving(joystick.axisX.axisValue, joystick.axisY.axisValue));
            joystick.OnPressUp.AddListener(() => player.Moving(joystick.axisX.axisValue, joystick.axisY.axisValue));
            joystick.OnPressDown.AddListener(() => player.Moving(joystick.axisX.axisValue, joystick.axisY.axisValue));
        }
        //判断攻击按钮和人物是否存在
        if (tCButtons.Count != 0 && player.game)
        {
            Debug.Log("进来了吗，按钮");
            //遍历所有按钮，添加监听事件
            foreach (var item in tCButtons)
            {
                //点击攻击按钮，通过继承基类的方法，向服务器发消息
                item.onPressed.AddListener(() => player.JoyButtonHandler(item.name));
            }
        }
    }
}
