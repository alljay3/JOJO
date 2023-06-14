using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PobedaTextScript : MonoBehaviour
{

    [SerializeField] GameObject Music;
    // Start is called before the first frame update
    void Start()
    {
        Music.GetComponent<AudioSource>().enabled = false;
    }
    public void Pobeda()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
