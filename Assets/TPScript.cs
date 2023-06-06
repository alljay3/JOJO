using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPScript : MonoBehaviour
{
    [SerializeField] public GameObject MeinPlayer;
    [SerializeField] public GameObject MeinCam;
    [SerializeField] public float XJump;
    [SerializeField] public float YJump;
    [SerializeField] public GameObject[] PossibleNextRoom;
    // Start is called before the first frame update
    void Start()
    {
        MeinPlayer = GameObject.Find("player");
        MeinCam = GameObject.Find("Main Camera");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            int rand = Random.Range(0, PossibleNextRoom.Length);
            GameObject bullet = GameObject.Instantiate(PossibleNextRoom[rand], MeinCam.transform.position + new Vector3(0, 20, 10), Quaternion.identity);

            MeinPlayer.transform.position = MeinPlayer.transform.position + new Vector3(XJump, YJump, 0);       
            MeinCam.transform.position = MeinCam.transform.position + new Vector3(0, 20, 0); 
            
              
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
