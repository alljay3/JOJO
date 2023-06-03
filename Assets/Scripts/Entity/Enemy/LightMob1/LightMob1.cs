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
        StartCoroutine("ChangeMove");
        DamageCollider.Damage = Damage;
        CurHp = Hp;
    }

    private void FixedUpdate()
    {
    if (!IsDamageReceived)
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

    public void ControllerMove()
    {

    }


    IEnumerator ChangeMove()
    {
        while (true)
        {
            _moveVector.x = Random.Range(-5, 5);
            _moveVector.y = Random.Range(-5, 5);
            _moveVector = _moveVector.normalized;
            Move();
            yield return new WaitForSeconds(5);
        }
        
    }
}
