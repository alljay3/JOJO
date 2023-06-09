using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRangeMob : Enemy
{
    private Vector2 _moveVector = new Vector2();
    [SerializeField] private DamageColliderEnemy DamageCollider;
  
    [SerializeField] private int RangeDamage;
    [SerializeField] private int BulletSpeed = 1;
    [SerializeField] private float PlayerDist = 2f;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] AudioClip[] Sounds;
    public void Start()
    {
        MeinPlayer = GameObject.Find("player");
        DamageCollider.Damage = Damage;
        CurHp = Hp;
    }

    private void FixedUpdate()
    {
        if (!IsDamageReceived)
        {
            Move();
        }
    }

    public override void Move()
    {
        ControllerMove();
        gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * SpeedMove;
    }

    public void ControllerMove()
    {
        var distans = MeinPlayer.GetComponent<Collider2D>().Distance(gameObject.GetComponent<Collider2D>());
        if (distans.distance < PlayerDist)
        {
            _moveVector = MeinPlayer.transform.position - gameObject.transform.position;
            _moveVector = -1 * _moveVector.normalized;
        }
        else
        {
            _moveVector.x = Random.Range(-1f, 1f);
            _moveVector.y = Random.Range(-1f, 1f);

        }

    }
    
    public void SpawnBullet()
    {

        if (!IsDamageReceived)
        {
            GetComponent<AudioSource>().clip = Sounds[0];
            GetComponent<AudioSource>().Play();
            GameObject bullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().Damage = RangeDamage;
            bullet.GetComponent<Bullet>().Speed = BulletSpeed;
            bullet.GetComponent<Bullet>().position = MeinPlayer.transform.position;
        }
    }


    public override void TakeDamage(int damage, Vector2 posintionDamage)
    {
        base.TakeDamage(damage, posintionDamage);
        StopMob();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Foot")
        {
            collision.GetComponent<Foot>().MainPlayer.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 2;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foot")
        {
            collision.GetComponent<Foot>().MainPlayer.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }
}
