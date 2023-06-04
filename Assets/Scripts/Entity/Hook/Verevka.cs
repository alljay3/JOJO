using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verevka : MonoBehaviour
{
    public GameObject selfobj, hookobj;

    private void Start()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, selfobj.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(0, hookobj.transform.position);
    }

    private void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, selfobj.transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(0, hookobj.transform.position);
    }

}
