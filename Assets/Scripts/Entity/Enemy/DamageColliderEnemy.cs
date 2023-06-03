using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliderEnemy : MonoBehaviour
{
    [HideInInspector]public int Damage;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(Damage,transform.position);
        }
    }

}
