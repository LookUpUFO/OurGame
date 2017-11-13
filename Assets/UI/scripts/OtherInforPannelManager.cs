using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherInforPannelManager : MonoBehaviour {

    public static OtherInforPannelManager instance;
    public static OtherInforPannelManager Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }

}
