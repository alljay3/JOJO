using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{



    [SerializeField] public int Hp;
    [SerializeField] public float SpeedMove;
    [SerializeField] public float SpeedAttack;
    [SerializeField] public GameObject PrefabSpellHook;
    [SerializeField] public int Damage = 0;
    [SerializeField] public int Armor = 0;
    [SerializeField] public float TimeDamageRecive = 0.2f;
    [SerializeField] public float TimeInvulnerability = 0.2f;
    [SerializeField] public float SlowPercent = 0.4f;
    [SerializeField] public float Weight = 1;
    protected bool IsSlowed = false;
    protected bool IsDamageReceived = false;
    protected bool IsInvulnerability = false;
    protected bool isHookPush;
    protected GameObject HookShot;
    protected SpriteRenderer spriteRenderer;
    public int CurHp;
    public bool IsOnThehook = false;

    public int levelToLoad;

    void Update()
    {
            
    }

    public void setIsHookPush(bool hookPush)
    {
        isHookPush = hookPush;
    }

    virtual public void pushHook()
    {
        HookShot = GameObject.Instantiate(PrefabSpellHook, PrefabSpellHook.transform.position, Quaternion.identity) as GameObject;
        HookShot.transform.position = transform.position;
        HookShot.GetComponent<Hook>().SelfEntity = gameObject;

    }

    virtual public bool EntityMoveGrap(Vector2 place, float speed)
    {
        if (Vector2.SqrMagnitude(place - (Vector2)transform.position) > 0.3)
        {
            transform.GetComponent<Rigidbody2D>().velocity = (place - (Vector2)transform.position).normalized * speed;
            return false;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return true;
        }
    }

    virtual public void setSlowed()
    {
        if (!IsSlowed)
        {
            SpeedMove = SpeedMove * SlowPercent;
            IsSlowed = true;
        }
    }

    virtual public void removeSlowed()
    {
        if (IsSlowed)
        {
            SpeedMove = SpeedMove / SlowPercent;
            IsSlowed = false;
        }
    }

    virtual public bool SuperEntityMoveGrap(GameObject place, float speed)
    {
        var colliderDist = gameObject.GetComponent<Collider2D>().Distance(place.GetComponent<Collider2D>());
        if (colliderDist.distance > 0.4)
        {
            transform.GetComponent<Rigidbody2D>().velocity = ((Vector2)place.transform.position - (Vector2)transform.position).normalized * speed;
            return false;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return true;
        }
    }


    virtual public void TakeDamage(int damage, Vector2 posintionDamage)
    {
        if (!IsDamageReceived && !IsInvulnerability)
        {
            CurHp -= damage;
            IsDamageReceived = true;
            IsInvulnerability = true;
            StartCoroutine("DamageReceived");
            StartCoroutine("Invulnerability");
            Debug.Log(CurHp);
            Vector2 coord = (Vector2)transform.position - posintionDamage;
            transform.GetComponent<Rigidbody2D>().velocity = coord.normalized * Weight * 3;
            stopHook();
        }
        if (CurHp <= 0)
        {
          
            if (gameObject.tag == "Player")
            {
                SceneManager.LoadScene(levelToLoad);
            }
            Destroy(gameObject);
        }
    }


    virtual public void stopHook()
    {
    }


    IEnumerator DamageReceived()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(TimeDamageRecive);
        IsDamageReceived = false;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator Opacity()
    {
        while (true)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.8f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Invulnerability()
    {
        var myCoroutine = StartCoroutine("Opacity");
        yield return new WaitForSeconds(TimeInvulnerability);
        IsInvulnerability = false;
        StopCoroutine(myCoroutine);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
}
