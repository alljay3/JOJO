using System.Collections;//1
using System.Collections.Generic;
using UnityEngine;

public class TPScript : MonoBehaviour
{
    [SerializeField] public GameObject MeinPlayer;
    [SerializeField] public GameObject MeinCam;
    [SerializeField] public float XJump;
    [SerializeField] public float YJump;
    [SerializeField] public GameObject[] PossibleNextRoom;
    [SerializeField] public GameObject DepthAdjuster;
    // Start is called before the first frame update
    void Start()
    {
        MeinPlayer = GameObject.Find("player");
        MeinCam = GameObject.Find("Main Camera");
        DepthAdjuster = GameObject.Find("DepthAdjuster");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (DepthAdjuster!= null && DepthAdjuster.GetComponent<DepthAdjusterScript>().curDepth < DepthAdjuster.GetComponent<DepthAdjusterScript>().Depth)
            {
                DepthAdjuster.GetComponent<DepthAdjusterScript>().curDepth += 1;
                int rand = Random.Range(0, PossibleNextRoom.Length);
                GameObject bullet = GameObject.Instantiate(PossibleNextRoom[rand], MeinCam.transform.position + new Vector3(0, 20, 10), Quaternion.identity);

                MeinPlayer.transform.position = MeinPlayer.transform.position + new Vector3(XJump, YJump, 0);
                MeinCam.transform.position = MeinCam.transform.position + new Vector3(0, 20, 0);
            }
            else
            {
                GameObject bullet = GameObject.Instantiate(DepthAdjuster.GetComponent<DepthAdjusterScript>().BossRoom, MeinCam.transform.position + new Vector3(0, 20, 10), Quaternion.identity);

                MeinPlayer.transform.position = MeinPlayer.transform.position + new Vector3(XJump, YJump, 0);
                MeinCam.transform.position = MeinCam.transform.position + new Vector3(0, 20, 0);
            }
            
            
              
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
    void Update()
    {
        
    }
}
