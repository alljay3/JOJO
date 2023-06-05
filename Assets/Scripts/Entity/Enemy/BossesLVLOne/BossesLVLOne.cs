using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesLVLOne : Enemy
{
    [SerializeField] private DamageColliderEnemy DamageCollider;
    [SerializeField] private int RangeDamage;
    [SerializeField] private int BulletSpeed = 1;
    [SerializeField] private GameObject BulletPrefab;

    public void Start()
    {
        MeinPlayer = GameObject.Find("player");
        DamageCollider.Damage = Damage;
        CurHp = Hp;
    }


    public override void Move()
    {
        return;
    }

    public void SpawnBullet()
    {

        if (!IsDamageReceived)
        {
            GameObject bullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().Damage = RangeDamage;
            bullet.GetComponent<Bullet>().Speed = BulletSpeed;
            bullet.GetComponent<Bullet>().position = MeinPlayer.transform.position;
            bullet.transform.LookAt2D(bullet.transform.right, MeinPlayer.transform.position);
        }
    }

    public void FixedUpdate()
    {
        Vector2 cord = MeinPlayer.transform.position - transform.position;
        Vector2 _moveVector = cord.normalized;
        if (_moveVector.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public override void TakeDamage(int damage, Vector2 posintionDamage)
    {
        base.TakeDamage(damage, posintionDamage);
    }


}
