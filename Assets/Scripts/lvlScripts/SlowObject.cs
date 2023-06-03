using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Foot")
        {
            collision.GetComponent<Foot>().MainPlayer.GetComponent<Player>().setSlowed();
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
            collision.GetComponent<Foot>().MainPlayer.GetComponent<Player>().removeSlowed();
            collision.GetComponent<Foot>().MainPlayer.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 2;
        }
        if (collision.tag == "Enemy")
        {
               collision.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }
}
