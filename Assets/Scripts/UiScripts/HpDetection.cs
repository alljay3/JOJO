using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDetection : MonoBehaviour
{
    [SerializeField] public Player MeinPlayer;

    public void Start()
    {
        MeinPlayer = GameObject.Find("player").GetComponent<Player>();
    }

    public void FixedUpdate()
    {
        gameObject.GetComponent<Slider>().value = (float)MeinPlayer.CurHp / (float)MeinPlayer.Hp;
    }

}
