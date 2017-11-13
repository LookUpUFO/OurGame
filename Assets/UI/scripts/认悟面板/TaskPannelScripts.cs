using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 任务面板：
///       任务的控制
/// </summary>


public class TaskPannelScripts : MonoBehaviour {


	void Start () {
    //面板刷新
        GetComponent<PanelController>().RefeshTheInforToPanelEvent += Refesh;
    }
    
    //刷新数据
    void Refesh()
    {

    }

}
