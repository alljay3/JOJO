using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1;
    public int Damage = 1;
    [HideInInspector] public Vector2 position;
    private Vector2 _moveVector;

    private void Start()
    {
        _moveVector = (Vector2)position - (Vector2)gameObject.transform.position;
        _moveVector = _moveVector.normalized;
        gameObject.GetComponent<Rigidbody2D>().velocity = _moveVector * Speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.gameObject.GetComponent<Player>().TakeDamage(Damage,transform.position);
        if (collision.tag == "Walls")
            Destroy(gameObject);
    }
}
