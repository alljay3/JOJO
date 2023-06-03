using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private GameObject Arm;
    [SerializeField] float TimeCooldownHook = 2;
    [SerializeField] GameObject DamageBlockPrefab;
    [SerializeField] float CooldownAttak = 1;
    private bool _isCoooldownAttak = false;
    private GameObject _damageBlock;
    private bool _isHookCooldown = false;
    private Vector2 _moveVector = new Vector2();
    void Start()
    {
        CurHp = Hp;
    }

    // Update is called once per frame
    void Update()
    {


      



        if(!IsDamageReceived)
        {
            if (Input.GetButtonDown("Jump") && !_isHookCooldown)
            {
                if (!isHookPush)
                {
                    isHookPush = true;
                    pushHook();
                }
                else
                {
                    isHookPush = false;
                    _isHookCooldown = true;
                    StartCoroutine("HookCooldownReduce");
                    HookShot.GetComponent<Hook>().HookDetele();
                }
           }
           if (!isHookPush)
            {
                _moveVector.x = Input.GetAxisRaw("Horizontal");
                _moveVector.y = Input.GetAxisRaw("Vertical");
                gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * (SpeedMove); 
            }
           if (Input.GetButton("Fire1"))
            {
                DamageBlockSpawn();
            }

        }
    }



    public override void pushHook()
    {
        HookShot = GameObject.Instantiate(PrefabSpellHook, PrefabSpellHook.transform.position, Quaternion.identity) as GameObject;
        HookShot.transform.position = Arm.transform.position;
        HookShot.GetComponent<Hook>().SelfEntity = gameObject;
    }

    public override bool EntityMoveGrap(Vector2 place, float speed)
    {
        return base.EntityMoveGrap(place, speed);
    }


    override public void stopHook()
    {
        if (isHookPush == true)
        {
            isHookPush = false;
            _isHookCooldown = true;
            StartCoroutine("HookCooldownReduce");
            HookShot.GetComponent<Hook>().HookDetele();
        }
    }

    private void DamageBlockSpawn()
    {
        if (!_damageBlock && !_isCoooldownAttak)
        {
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _naprVector = _mousePos - (Vector2)transform.position;
            Vector2 coord = _mousePos - (Vector2)transform.position;
            _naprVector = coord.normalized;
            coord = coord.normalized;
            coord.x = transform.position.x + coord.x * 0.7f;
            coord.y = transform.position.y + coord.y * 1.1f;
            _damageBlock = Instantiate(DamageBlockPrefab, coord, Quaternion.identity) as GameObject;


            float angle = Vector2.Angle(_naprVector, new Vector2(0,1));
            if (_naprVector.x > 0)
            {
                angle = -angle;
            }
            _damageBlock.transform.Rotate(new Vector3(0, 0, angle));




            //_damageBlock.transform.LookAt2D(_damageBlock.transform.up, _mousePos);
            _damageBlock.transform.parent = transform;
            _damageBlock.transform.GetComponent<PlayerDamageCollider>().Damage = Damage;
            _isCoooldownAttak = true;
            StartCoroutine("DamageBlockController");
            StartCoroutine("ColldowAttakEnum");
        }
    }







    IEnumerator HookCooldownReduce()
    {
        yield return new WaitForSeconds(TimeCooldownHook);
        Debug.Log(TimeCooldownHook);
        _isHookCooldown = false;
    }

    IEnumerator DamageBlockController()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(_damageBlock);
    }

    IEnumerator ColldowAttakEnum()
    {
        yield return new WaitForSeconds(CooldownAttak);
        _isCoooldownAttak = false;
    }


}
