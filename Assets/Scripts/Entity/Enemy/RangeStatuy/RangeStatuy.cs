using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeStatuy : Enemy
{
    [SerializeField] private DamageColliderEnemy DamageCollider;
    [SerializeField] private int RangeDamage;
    [SerializeField] private int BulletSpeed = 1;
    [SerializeField] private GameObject BulletPrefab;
    private bool isStart = false;

    public void Start()
    {
        MeinPlayer = GameObject.Find("player");
        DamageCollider.Damage = Damage;
        CurHp = Hp;
        //StartCoroutine("gogo");
    }


    public void Active()
    {
        GetComponent<Animator>().enabled = true;
    }

    public void Probujdenie()
    {
        GetComponent<Animator>().Play("attacked");
        isStart = true;
        DamageCollider.enabled = true;
        tag = "Enemy";
    }

    private void FixedUpdate()
    {
       if (GetComponent<Animator>().enabled == true)
        {
           GetComponent<Animator>().SetBool("isStart", isStart);
        }
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
            bullet.transform.LookAt2D(bullet.transform.up, MeinPlayer.transform.position);
        }
    }


    public override void TakeDamage(int damage, Vector2 posintionDamage)
    {
        base.TakeDamage(damage, posintionDamage);
        StopMob();
    }

    //public override void StopMob()
    //{
    //    GetComponent<Animator>().Play("attacked");
    //}

    IEnumerator gogo()
    {
        yield return new WaitForSeconds(1.5f);
        Active();
    }
}
