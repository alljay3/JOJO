using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    [HideInInspector] public int Damage;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(Damage, transform.position);
        }
    }
}
