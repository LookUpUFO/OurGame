using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeadInfo : MonoBehaviour {

    Transform player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(ChangeBaseInfor());
    }
    //每一分钟检查一次面板
    IEnumerator ChangeBaseInfor()
    {
        while (true)
        {
            transform.Find("Name").GetComponent<Text>().text = player.GetComponent<PlayerInfor>().playerAttribute.Name;
            transform.Find("Level").GetComponent<Text>().text = player.GetComponent<PlayerInfor>().playerAttribute.Level.ToString();
            yield return new WaitForSeconds(30f);
        }
    }

}
