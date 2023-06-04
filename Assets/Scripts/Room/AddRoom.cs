using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Doors")]
    public GameObject[] Doors;
    public GameObject doorEffect;


    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;


    [Header("Powerups")]
    public GameObject[] buffs;

    [HideInInspector] public List<GameObject> enemies;

    //private RoomVariants variants;
    private bool spawned;
    private bool doorDestroyed;




    
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;

            foreach (Transform spawner in enemySpawners)
            {
                int rand = Random.Range(0, enemyTypes.Length);
                
                GameObject enemyType = enemyTypes[rand];
                //LightMob1.MeinPlayer= GameObject.Find("player");

                //enemyType.GetComponent<MeinPlayer>() = GameObject.FindGameObjectsWithTag("Player");
                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                //enemy.setPlayer(GameObject.FindGameObjectsWithTag("Player"));
                //enemy.GetComponent<MeinPlayer>() = GameObject.FindGameObjectsWithTag("Player");
                //enemy.LightMob1.setPlayer();
                enemy.transform.parent = transform;
                enemies.Add(enemy);
            }
            StartCoroutine(CheckEnemies());
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyDoors();
    }

    public void DestroyDoors()
    {
        foreach(GameObject door in Doors)
        {
            if (door != null)   /*&& door.transform.childCount != 0*/
            {
                Destroy(door);
            }
        }
        doorDestroyed = true;
    }



    void Update()
    {
        
    }
}
