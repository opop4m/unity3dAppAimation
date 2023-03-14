using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;

/// <summary>
/// 开始面板
/// </summary>
public class StartPanel : BasePanel
{
    /// <summary>
    /// 路径
    /// </summary>
    static readonly string path = "Prefabs/UI/Panel/StartPanel";

    /// <summary>
    /// 开始面板
    /// </summary>
    public StartPanel() : base(new UIType(path))
    {

    }
    public Page curPage { get; private set; }
    protected override void InitEvent()
    {

        CurrentTransform.GetOrAddComponentInChildren<Button>("NextPage").onClick.AddListener(() =>
        {
            Push(new Panel1());
        });
        CurrentTransform.GetOrAddComponentInChildren<Button>(EnumPage.TabItem1.key).onClick.AddListener(() =>
        {

            selectedPage(EnumPage.TabItem1);
            // Push(new Panel1());
            // Game.LoadScene(new MainScene());
        });
        CurrentTransform.GetOrAddComponentInChildren<Button>(EnumPage.TabItem2.key).onClick.AddListener(() =>
        {
            selectedPage(EnumPage.TabItem2);
        });
    }
    public Page[] pageArr = new Page[] { EnumPage.TabItem1, EnumPage.TabItem2 };
    void selectedPage(Page showPage)
    {
        curPage = showPage;
        // Debug.Log($"selected page:{showPage}");
        for (int i = 0; i < pageArr.Length; i++)
        {
            Page page = pageArr[i];
            Transform panel = CurrentTransform.Find(page.panelKey);
            if (panel == null)
            {
                Debug.LogError($"page is not found:{page.ToString()}");
                continue;
            }
            if (page.key == showPage.key)
            {
                panel.PanelAppearance(true);
                // Debug.Log($"show panel:{panel.name}");
            }
            else
            {
                panel.PanelAppearance(false);
            }
        }
    }
}

public class EnumPage
{
    public static Page TabItem1 = new Page("TabPanel1", "Content/HomePanel");
    public static Page TabItem2 = new Page("TabPanel2", "Content/MinePanel");


}
public class Page
{
    public Page(string key, string panelKey)
    {
        this.key = key;
        this.panelKey = panelKey;
    }
    public string key;
    public string panelKey;

    public override string ToString()
    {
        return $"page key:{key}, pannel key:{panelKey}";
    }
}
