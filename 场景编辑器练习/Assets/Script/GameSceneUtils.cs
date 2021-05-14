using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUtils
{
    static public void LoadSceneAsync(string name,Action action)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.completed += (_async) =>
        {
            action?.Invoke();
        };
    }
}
