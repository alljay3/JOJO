using System.Collections;//1
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private GameObject Arm;
    [SerializeField] public float TimeCooldownHook = 2;
    [SerializeField] GameObject DamageBlockPrefab;
    [SerializeField] float CooldownAttak = 1;
    private bool _isCoooldownAttak = false;
    private GameObject _damageBlock;
    public bool _isHookCooldown = false;
    private Vector2 _moveVector = new Vector2();
    public int _stateAnim;
    [SerializeField] AudioClip [] Sounds;
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
                    GetComponent<AudioSource>().clip = Sounds[0];
                    GetComponent<AudioSource>().Play();
                    isHookPush = true;
                    pushHook();
                }
                else
                {
                    GetComponent<AudioSource>().clip = Sounds[1];
                    GetComponent<AudioSource>().Play();
                    isHookPush = false;
                    _isHookCooldown = true;
                    StartCoroutine("HookCooldownReduce");
                    HookShot.GetComponent<Hook>().HookDetele();
                }
           }
           if (!IsOnThehook)
            {
                _moveVector.x = Input.GetAxisRaw("Horizontal");
                _moveVector.y = Input.GetAxisRaw("Vertical");
                gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * (SpeedMove);
                _stateAnim = 0;
                bool s = false;
                if (_moveVector.y < 0)
                {
                    _stateAnim = 2;
                    s = true;
                }
                if (_moveVector.y > 0)
                {
                    _stateAnim = 1;
                    s = true;

                }
                if (_moveVector.x != 0)
                {
                    _stateAnim = 3;
                    s = true;

                }
                if (s == true)
                {
                    if (GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().clip = Sounds[3];
                        GetComponent<AudioSource>().Play();
                    }
                }

                if (!_damageBlock)
                    if (_moveVector.x < 0)
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    else
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
           if (Input.GetButton("Fire1"))
            {
                DamageBlockSpawn();
            }

        }

    }

    private void FixedUpdate()
    {
        AnimControl();
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
            GetComponent<AudioSource>().clip = Sounds[2];
            GetComponent<AudioSource>().Play();
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _naprVector = _mousePos - (Vector2)transform.position;
            Vector2 coord = _mousePos - (Vector2)transform.position;
            _naprVector = coord.normalized;
            coord = coord.normalized;
            coord.x = transform.position.x + coord.x * 0.5f;
            coord.y = transform.position.y - 0.2f + coord.y * 0.8f;
            _damageBlock = Instantiate(DamageBlockPrefab, coord, Quaternion.identity) as GameObject;

            if (_naprVector.y < 0)
            {
                _damageBlock.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
            else
            {
                _damageBlock.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }


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
            if (_naprVector.x < 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
    }

    public override void soundDmg()
    {
        base.soundDmg();
        GetComponent<AudioSource>().clip = Sounds[4];
        GetComponent<AudioSource>().Play();
    }

    public void AnimControl()
    {
        if (_damageBlock && _stateAnim != 5)
        {
            _stateAnim = 4;
        }
        if (gameObject.GetComponent<Animator>().GetInteger("State") != _stateAnim)
        {
            gameObject.GetComponent<Animator>().SetBool("GoBack", true);
            gameObject.GetComponent<Animator>().SetInteger("State", _stateAnim);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("GoBack", false);
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
