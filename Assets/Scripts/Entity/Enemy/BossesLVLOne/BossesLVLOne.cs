using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesLVLOne : Enemy
{
    [SerializeField] private DamageColliderEnemy DamageCollider;
    [SerializeField] private int RangeDamage;
    [SerializeField] private int BulletSpeed = 1;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private int KolArrow = 5;
    [SerializeField] private float TimeTornado = 5;
    [SerializeField] private float SpeedTornadoArrow = 0.1f;
    [SerializeField] AudioClip[] Sounds;

    private int countArrow = 0;
    private bool isTornado = false;

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
        GameObject bullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().Damage = RangeDamage;
        bullet.GetComponent<Bullet>().Speed = BulletSpeed;
        bullet.GetComponent<Bullet>().position = MeinPlayer.transform.position;
        bullet.transform.LookAt2D(bullet.transform.right, MeinPlayer.transform.position);
        GetComponent<AudioSource>().clip = Sounds[0];
        GetComponent<AudioSource>().Play();
        countArrow += 1;
    }

    public void SpawnBulletTornado(int ang)
    {
        Vector2 coord = new Vector2();
        int r = 5;
        float x = r * Mathf.Cos(Mathf.Deg2Rad * ang);
        float y = r * Mathf.Sin(Mathf.Deg2Rad * ang);
        coord.y = y + transform.position.y;
        coord.x = x + transform.position.x;
        GameObject bullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().Damage = RangeDamage;
        bullet.GetComponent<Bullet>().Speed = BulletSpeed;
        bullet.GetComponent<Bullet>().position = coord;
        bullet.transform.LookAt2D(bullet.transform.right, coord);

    }

    public void FixedUpdate()
    {
        Vector2 cord = MeinPlayer.transform.position - transform.position;
        Vector2 _moveVector = cord.normalized;
        if (_moveVector.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        GoTornado(); 
    }

    void GoTornado()
    {
        if (countArrow >= KolArrow && !isTornado)
        {
            StartCoroutine("TornadoTimeEnum");
        }
    }

    IEnumerator TornadoTimeEnum()
    {
        isTornado = true;
        var tornado = StartCoroutine("TornadoBullet");
        GetComponent<Animator>().Play("Tornado");
        yield return new WaitForSeconds(TimeTornado);
        StopCoroutine(tornado);
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Stop();
        isTornado = false;
        countArrow = 0;
        GetComponent<Animator>().Play("Attack");

    }

    IEnumerator TornadoBullet()
    {
        GetComponent<AudioSource>().clip = Sounds[1];
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
        int x = 0;
        while (true)
        {
            SpawnBulletTornado(x);
            yield return new WaitForSeconds(SpeedTornadoArrow);
            x += 10;
            if (x > 360)
            {
                x = 0;
            }
        }
    }


    public override void StopMob()
    {
        return;
    }


    public override void TakeDamage(int damage, Vector2 posintionDamage)
    {
        base.TakeDamage(damage, posintionDamage);
    }


}
