using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SlideSwitch
{


    public static bool isAllowSwitch(RectTransform curPanel, PointerEventData eventData)
    {
        float allowMoveX = curPanel.rect.width / 10;
        if (eventData.position.x < allowMoveX)
        {
            return true;
        }
        return false;
    }
}