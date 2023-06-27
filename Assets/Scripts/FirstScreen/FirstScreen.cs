using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreen : MonoBehaviour
{
    [SerializeField] GameObject LoadScreen;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("FirstStart", 0);
        PlayerPrefs.SetInt("GoReclama", 1);
        PlayerPrefs.SetInt("Dead", 0);
        LoadScreen.SetActive(true);
    }
}
