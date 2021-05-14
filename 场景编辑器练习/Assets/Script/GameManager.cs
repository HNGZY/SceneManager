using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 存放跳转场景不销毁的物体
    /// </summary>
    public GameObject[] DontDestory;
    /// <summary>
    /// 存放技能的集合
    /// </summary>
    public List<ETCButton> Attack;
    /// <summary>
    /// 摇杆控制移动的
    /// </summary>
    public ETCJoystick joystick;

    public GameObject uiroot;
    void Start()
    {
        for (int i = 0; i < DontDestory.Length; i++)
        {
            GameObject.DontDestroyOnLoad(DontDestory[i]);
        }
        GameSceneUtils.LoadSceneAsync("Lobby", () =>
        {
            JoyStickMgr.Instance.game = DontDestory[0];
            JoyStickMgr.Instance.joystick = joystick;
            JoyStickMgr.Instance.tCButtons = Attack;
            GameData.Instance.InitByRoleName("Teddy");
            World.Instance.Init();
        });


    }
    void Update()
    {
        
    }
}
