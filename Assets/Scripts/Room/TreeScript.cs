using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Vector2 position;
    [HideInInspector] Renderer myRenderer;

    public bool isTopTree;
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        position = gameObject.transform.position;
        myRenderer.sortingLayerName = "Top";
        if (isTopTree)
        {
            
            myRenderer.sortingOrder = 4;
        }
        else
        {
            myRenderer.sortingOrder = 10000 - (int)(10 * position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
