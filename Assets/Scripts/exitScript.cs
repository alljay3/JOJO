using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Application.Quit();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
