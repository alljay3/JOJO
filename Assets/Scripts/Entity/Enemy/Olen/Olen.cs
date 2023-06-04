using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olen : Enemy
{

    private Vector2 _moveVector = new Vector2();
    [SerializeField] private DamageColliderEnemy DamageCollider;
    [SerializeField] private float TimeRush;
    [SerializeField] private float TimeColldownSkill;
    [SerializeField] private float MaxDistant;
    [SerializeField] private float SpeedRush;
    private bool _isSkillColdow = false;
    private bool _isSkillProcces = false;
    private bool _isSkillPreparation = false;


    public void Start()
    {
        MeinPlayer = GameObject.Find("player");

        DamageCollider.Damage = Damage;
        CurHp = Hp;
    }

    private void FixedUpdate()
    {

        if (!IsDamageReceived && !IsOnThehook && !_isSkillProcces && !_isSkillPreparation)
        {
            var distant = MeinPlayer.GetComponent<Collider2D>().Distance(gameObject.GetComponent<Collider2D>());
            float dist = distant.distance;
            if (!_isSkillColdow && dist < MaxDistant)
            {
                PreparationStart();
            }
                
            else

                Move();

        }
        if (_isSkillPreparation)
        {
            Vector2 cord = MeinPlayer.transform.position - transform.position;
            _moveVector = cord.normalized;
            if (_moveVector.x > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }


    public void PreparationStart()
    {
        gameObject.layer = 13;
        _isSkillPreparation = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Animator>().Play("preparation");
    }


    public void RushStart()
    {
        _isSkillProcces = true;
        _isSkillPreparation = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * SpeedRush;
        gameObject.GetComponent<Animator>().Play("Rush");
        StartCoroutine("SkillCooldownReduce");
        StartCoroutine("RushEnum");

    }

    IEnumerator RushEnum()
    {
        yield return new WaitForSeconds(TimeRush);
        _isSkillProcces = false;
        gameObject.GetComponent<Animator>().Play("Run");
        gameObject.layer = 3;
        Debug.Log(_isSkillColdow);
        Debug.Log(_isSkillPreparation);
        Debug.Log(_isSkillProcces);
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

    IEnumerator SkillCooldownReduce()
    {
        _isSkillColdow = true;
        yield return new WaitForSeconds(TimeColldownSkill);
        _isSkillColdow = false;
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
