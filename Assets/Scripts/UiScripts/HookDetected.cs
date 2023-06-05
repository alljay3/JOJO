using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookDetected : MonoBehaviour
{
    [SerializeField] public Player MeinPlayer;

    public void Start()
    {
        MeinPlayer = GameObject.Find("player").GetComponent<Player>();
    }

    public void FixedUpdate()
    {
        if (MeinPlayer._isHookCooldown == true)
            GetComponent<Image>().color = new Color32(255, 0, 0,224);
        else
        {
            GetComponent<Image>().color = new Color32(156, 156, 156, 224);
        }
    }

}
