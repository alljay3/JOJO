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
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
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
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        if (collision.tag == "Enemy")
        {
               collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
