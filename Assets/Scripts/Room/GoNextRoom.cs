using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextRoom : MonoBehaviour
{

    [SerializeField] public GameObject MeinPlayer;
    // Start is called before the first frame update
    void Start()
    {
        MeinPlayer = GameObject.Find("player");

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("a");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
