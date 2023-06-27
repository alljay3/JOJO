using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAdd : MonoBehaviour
{
    static public void Show()

    {
        Application.ExternalCall("ShowAd");
    }
}
