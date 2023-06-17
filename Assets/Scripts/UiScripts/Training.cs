using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    [SerializeField] GameObject[] MyUi;
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart") == 0)
        {
            foreach (var l in MyUi)
            {
                l.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("FirstStart") == 1)
        {
            foreach (var l in MyUi)
            {
                l.SetActive(false);
            }
        }
    }
}
