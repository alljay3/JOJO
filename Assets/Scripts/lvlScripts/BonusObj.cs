using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObj : MonoBehaviour
{
    [SerializeField] int CurHp = 0;
    [SerializeField] int MaxHp = 0;
    [SerializeField] int Damage = 0;
    [SerializeField] float MoveSpeed = 0;
    [SerializeField] float CooldownHookReduce = 0;
    [SerializeField] float TimeInvulnerability = 0;

    [SerializeField] bool DestroyOtherBonus;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.Hp += MaxHp;
            player.CurHp += CurHp;
            var hp = player.CurHp + CurHp;
            if (hp >= player.Hp)
                player.CurHp = player.Hp;
            else
                player.CurHp = hp;
            player.Damage += Damage;
            player.SpeedMove += MoveSpeed;
            if (player.TimeCooldownHook - CooldownHookReduce >= 0)
                player.TimeCooldownHook -= CooldownHookReduce;
            player.TimeInvulnerability += TimeInvulnerability;
            if (DestroyOtherBonus)
            {
                var bonuses =  GameObject.FindGameObjectsWithTag("Bonus");
                foreach (var f in bonuses)
                {
                    if (f != gameObject)
                    {
                        Destroy(f);
                    }
                }
            }
            Destroy(gameObject);
        }
    }

}

