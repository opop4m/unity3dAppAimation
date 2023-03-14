using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;


public class Panel2 : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/Panel2";

    /// <summary>
    /// 开始面板
    /// </summary>
    public Panel2() : base(new UIType(path))
    {

    }

    protected override void InitEvent()
    {
        CurrentTransform.GetOrAddComponentInChildren<Button>("BtnBack").onClick.AddListener(() =>
        {
            Pop();
        });
        CurrentTransform.GetOrAddComponentInChildren<Button>("BtnNext").onClick.AddListener(() =>
        {
            Push(new Panel3());
        });
    }
}