using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// InterfaceUI下的子对象与ManagerPanel是相对应的
/// </summary>

public class InterfaceScripts : MonoBehaviour {
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = UIManager.Instance.transform.GetChild(i).gameObject;
            transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => { UIManager.Instance.OpenPanel(obj); });

        }
    }



}
