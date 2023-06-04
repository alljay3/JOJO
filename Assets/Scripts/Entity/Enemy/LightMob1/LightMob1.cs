using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMob1 : Enemy
{

    private Vector2 _moveVector = new Vector2();
    [SerializeField] private DamageColliderEnemy DamageCollider;
    [SerializeField] private GameObject MeinPlayer;

    public void Start()
    {
        DamageCollider.Damage = Damage;
        CurHp = Hp;
    }

    private void FixedUpdate()
    {
    if (!IsDamageReceived && !IsOnThehook)
        Move();
    }

    public override void Move()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * SpeedMove;
        Vector2 cord = MeinPlayer.transform.position - transform.position;
        cord = cord.normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = cord * SpeedMove;
        if (cord.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Foot")
        {
            collision.GetComponent<Foot>().MainPlayer.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 2;
        }
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Enemy>().TypeOfMove != Enemy.TypeMovement.Fly)
                collision.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foot")
        {
            collision.GetComponent<Foot>().MainPlayer.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }
}
