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
            PlayerPrefs.SetInt("FirstStart", 0);
            Application.Quit();
            Debug.Log("aaAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
