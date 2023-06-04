using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultHook : Hook
{
    private Vector2 _mousePos, _naprVector;
    public bool _isGrap = false;
    private bool _hoolEnd = false;
    [SerializeField] private float SpeedGrap;
    void Start()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ѕозици€ мыши переводитс€ в координаты игры.
        _naprVector = _mousePos - (Vector2)transform.position;
        transform.LookAt2D(transform.right, _mousePos);
        transform.GetComponent<Rigidbody2D>().velocity = _naprVector.normalized * Speed;
        gameObject.GetComponent<LineRenderer>().SetPosition(0, SelfEntity.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, gameObject.transform.position);
    }
    void FixedUpdate()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, SelfEntity.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, gameObject.transform.position);
        if (_isGrap)
        {
            HookAction();
            if (_hoolEnd || !Aim)
            {
                SelfEntity.GetComponent<Entity>().setIsHookPush(false);
                HookDetele();
            }
        }
    }


    void StopHook()
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }


    void HookAction()
    {
        if (SelfEntity.GetComponent<Player>()._stateAnim != 0)
        {
            SelfEntity.GetComponent<Animator>().SetBool("GoBack", true);
            SelfEntity.GetComponent<Player>()._stateAnim = 0;
        }

        if (Aim && Aim.tag == "Walls")
        {
            _hoolEnd = SelfEntity.GetComponent<Entity>().SuperEntityMoveGrap(gameObject, SpeedGrap);
        }
        if (Aim && Aim.tag == "Enemy")
        {
            Aim.gameObject.GetComponent<Entity>().IsOnThehook = true;
            Aim.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Aim.gameObject.GetComponent<Enemy>().StopMob();
            if (Aim.gameObject.GetComponent<Enemy>().TypeOfSeverity == Enemy.SeverityMob.Light)
               _hoolEnd = Aim.gameObject.GetComponent<Entity>().SuperEntityMoveGrap(SelfEntity, SpeedGrap);
            else if (Aim.gameObject.GetComponent<Enemy>().TypeOfSeverity == Enemy.SeverityMob.Heavy)
            {
                _hoolEnd = SelfEntity.gameObject.GetComponent<Entity>().SuperEntityMoveGrap(Aim, SpeedGrap);
            }
            transform.position = Aim.transform.position;
        }


    }





    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isGrap && (collider.tag == "Walls" || collider.tag == "Enemy" || collider.tag == "Background"))
        {
            _isGrap = true;
            Aim = collider.gameObject;
            StopHook();
            SelfEntity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            SelfEntity.gameObject.GetComponent<Entity>().IsOnThehook = true;
        }
    }

    public override void HookDetele()
    {
        if (Aim && Aim.tag == "Enemy")
            Aim.gameObject.GetComponent<Entity>().IsOnThehook = false;
        SelfEntity.gameObject.GetComponent<Entity>().IsOnThehook = false;
        Destroy(gameObject);
    }
}
