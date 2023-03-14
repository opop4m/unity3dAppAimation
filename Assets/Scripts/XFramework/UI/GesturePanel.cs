using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XFramework
{
    public class GesturePanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        // public GesturePanel(UIType ui) : base(ui) { }
        // Start is called before the first frame update


        private bool Draging = false;

        const int PlayToStart = 1;
        const int PlayToEnd = 2;

        int playStatus = 0;
        float playSpeed = 2f;

        float playWidth = 0;
        private GameRoot gameRoot;
        // Start is called before the first frame update
        void Start()
        {
            gameRoot = GameRoot.Instance;
            curPanel = GameRoot.Instance.PanelManager.panelStack.Peek();
            prevPanel = getPrevPanel();
            // Debug.Log($"curPanel.CurrentTransform.anchoredPosition:{curPanel.CurrentTransform.anchoredPosition},urPanel.CurrentTransform.rect:{curPanel.CurrentTransform.rect}");
            curPanel.CurrentTransform.anchoredPosition = new Vector2(curPanel.CurrentTransform.rect.width, 0);
            playWidth = curPanel.CurrentTransform.rect.width;
            playSpeed = playWidth / 30;
            playStatus = PlayToStart;
        }

        // Update is called once per frame
        void Update()
        {
            if (playStatus != 0 && curPanel != null)
            {
                float speed = playSpeed;
                if (playStatus == PlayToStart)
                {
                    speed *= -1;
                }
                movePanel(speed);
            }
        }

        private void movePanel(float offset)
        {

            Vector2 oldPos = curPanel.CurrentTransform.anchoredPosition;
            Vector2 _new = new Vector2(oldPos.x + offset, 0);
            Vector2 prevNewPos = Vector2.one;
            if (prevPanel != null){
                Vector2 prevPos = prevPanel.CurrentTransform.anchoredPosition;
                prevNewPos = new Vector2(prevPos.x + offset / 2, 0);
            }
            if (playStatus == PlayToStart)
            {
                if (_new.x < 0)
                {
                    _new.x = 0;
                    playStatus = 0;
                    if (prevPanel != null)
                        prevNewPos.x = -1*playWidth/2;
                }
            }
            else
            {
                if (_new.x > playWidth)
                {
                    _new.x = playWidth;
                    playStatus = 0;
                    if (prevPanel != null)
                        prevNewPos.x = 0;
                    GameRoot.Instance.PanelManager.Pop();
                }
            }
            curPanel.CurrentTransform.anchoredPosition = _new;
            if (prevPanel != null)
            {
                prevPanel.CurrentTransform.anchoredPosition = prevNewPos;
                // Debug.Log("play switch curX:" + _new.x + " ,prevPanel.Transform.anchoredPosition:" + prevPanel.CurrentTransform.anchoredPosition);
            }
            
        }

        BasePanel curPanel;
        BasePanel prevPanel;
        Vector2 lastPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            
            Vector2 canvasSize = (curPanel.CurrentTransform.parent as RectTransform).sizeDelta;
            // Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, -Camera.main.transform.position.z));
            // Debug.Log("canvasSize:" + canvasSize + ",UnityEngine.Screen:" + $"{UnityEngine.Screen.width}x{UnityEngine.Screen.height},dpi:{UnityEngine.Screen.dpi}" +
            // ",CurrentTransform.rect:" + curPanel.CurrentTransform.rect + "\nCurrentTransform.position:" + curPanel.CurrentTransform.position);
            if (SlideSwitch.isAllowSwitch(curPanel.CurrentTransform, eventData))
            {
                Draging = true;
                lastPos = eventData.position;
                // curPanel.CurrentTransform.SetRtAnchorSafe(Vector2.zero, Vector2.one);
                // Debug.Log("OnBeginDrag:" + eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Draging)
            {
                Vector2 offset = eventData.position - lastPos;
                Vector2 old = curPanel.CurrentTransform.anchoredPosition;
                Vector2 _new = old + new Vector2(offset.x * gameRoot.UIrate, 0);
                if (_new.x < 0)
                    return;
                curPanel.CurrentTransform.anchoredPosition = _new;
                // Debug.Log("OnDrag:" + eventData);
                lastPos = eventData.position;

                if (prevPanel != null)
                {
                    Vector2 prevPos = prevPanel.CurrentTransform.anchoredPosition;
                    prevPanel.CurrentTransform.anchoredPosition = new Vector2(prevPos.x + offset.x * gameRoot.UIrate / 2, 0);
                }
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Draging = false;

            Vector2 cur = curPanel.CurrentTransform.anchoredPosition;
            if (cur.x < playWidth / 2)
            {
                playStatus = PlayToStart;
            }
            else
            {
                playStatus = PlayToEnd;
            }
            // Debug.Log($"playStatus: {playStatus}, curEnd:{cur.x}, playWidth:{playWidth}");
        }

        private BasePanel getPrevPanel()
        {
            BasePanel[] panelArr = GameRoot.Instance.PanelManager.panelStack.ToArray();
            if (panelArr.Length > 1)
            {
                return panelArr[1];
            }
            return null;
        }

        public void playPop(){
            playStatus = PlayToEnd;
        }

    }

}

