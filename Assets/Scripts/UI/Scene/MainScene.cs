using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

public class MainScene : SceneState
{
    /// <summary>
    /// 主场景
    /// </summary>
    public MainScene()
    {
        sceneName = "Main";
    }

    public override void OnEnter()
    {
        // panelManager.Push(new MainPanel());
    }
}
