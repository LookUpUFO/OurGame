using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
///  此Pannel的控制 ：
///  
/// </summary>
public class PanelController : MonoBehaviour {

    public  delegate  void RefeshTheInforToPanelHandle();
    public  RefeshTheInforToPanelHandle RefeshTheInforToPanelEvent;

    //原始的位置
    Vector3      origalPos;
    public float speed = 1f;
    Button       closeBtn;
    Tweener tween;
    private void Start()
    {
        Transform Close = transform.Find("CloseBtn");

        if (Close)
        {
            closeBtn = Close.GetComponent<Button>();
            //打开关闭监听
            closeBtn.onClick.AddListener(() => { UIManager.Instance.ClosePanel(); });
            origalPos = transform.position;
        }

    }

    //隐藏面板 
    public void HidePanel()
    {
        transform.DOMove(origalPos, 1f);

    }
    public void HidePanelBySCALE()
    {

    }
    //显示面板
    public void ShowPanel()
    {
        //刷新面板信息
        transform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), 1f);
        RefeshTheInforToPanelEvent();

    }

    //移动面板到指定位置
    public void ShowPanel(Vector3 pos)
    {
        transform.DOMove(pos, 1f);
    }
 
}
