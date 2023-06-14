using System.Collections;//1
using System.Collections.Generic;
using UnityEngine;

public class DepthAdjusterScript : MonoBehaviour
{
    [SerializeField] public int Depth;
    [SerializeField] public int curDepth;
    [SerializeField] public GameObject BossRoom;
    // Start is called before the first frame update
    void Start()
    {
        curDepth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
