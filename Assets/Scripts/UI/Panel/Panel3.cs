using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;


public class Panel3 : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/Panel3";

    /// <summary>
    /// 开始面板
    /// </summary>
    public Panel3() : base(new UIType(path))
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
            Debug.Log("no next");
        });
    }
}