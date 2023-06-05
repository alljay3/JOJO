using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{


    [SerializeField] public int RoomDifficultyWeight;
    [SerializeField] public int NumberOfWaves;

    [Header("Doors")]
    public GameObject[] Doors;
    public GameObject doorEffect;


    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("Trees")]
    public GameObject[] treeTypes;
    public Transform[] treeSpawners;

    [Header("Powerups")]
    public GameObject[] buffs;

    public List<GameObject> enemies;

    //private RoomVariants variants;
    private bool spawned;
    
    private bool treespawned;
    private bool doorDestroyed;




    
    void Start()
    {
        spawned = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            treespawned = true;
            foreach (Transform spawner in treeSpawners)
            {
                int rand = Random.Range(0, treeTypes.Length);

                GameObject treeType = treeTypes[rand];

                float Xoffset = Random.Range(0.0f, 1.0f);
                float Yoffset = Random.Range(0.0f, 0.5f);
                GameObject tree = Instantiate(treeType, spawner.position + new Vector3(Xoffset, Yoffset,0), Quaternion.identity) as GameObject;
                //Renderer myRenderer = tree.getComponent<Renderer>();
                //myRenderer.sortingLayerName = "TreesSortingLayer";
                //SmyRenderer.sortingOrder = 10;
                if (spawner.position.y>0 && spawner.position.x>-8.5f && spawner.position.x <8)
                {
                    tree.GetComponent<TreeScript>().isTopTree = true;
                }
                
                tree.transform.parent = transform;
                
            }


            SpawnEnemies(RoomDifficultyWeight);


            CheckEnemies();
        }
    }

    void SpawnEnemies(int diff_weight)
    {
        foreach (Transform spawner in enemySpawners)
        {
            int rand = Random.Range(0, enemyTypes.Length);
            GameObject enemyType = enemyTypes[rand];
            GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = transform;
            enemies.Add(enemy);
        }
        spawned = true;
    }

    public void CheckEnemies()
    {
        
        Debug.Log("start");
        Debug.Log(enemies.Count);
        if (spawned)
        {
            if (enemies.Count == 0)
            {
                if (NumberOfWaves > 1)
                {

                    //yield return new WaitUntil(() => enemies.Count == 0);

                    SpawnEnemies(RoomDifficultyWeight);
                    NumberOfWaves--;

                }
                else
                {
                    Debug.Log("aaaaaaaaaaaaaaaaaaa");
                    DestroyDoors();
                }
            }
        }
       
       
      
       
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
    void FixedUpdate()
    {
        enemies.RemoveAll(obj => obj == null);
        if (spawned && !doorDestroyed)
        {
            CheckEnemies();
        }
        //Debug.Log("FixedUpdate time :" + Time.deltaTime);
        //Debug.Log(enemies.Count);
        //if (spawned && enemies.Count == 0)
        //{
        //    Debug.Log("No ENEMIES");
        //    if (NumberOfWaves != 0)
        //    {

        //    }
        //    else
        //    {
        //        foreach (GameObject door in Doors)
        //        {
        //            if (door != null)   /*&& door.transform.childCount != 0*/
        //            {
        //                Destroy(door);
        //            }
        //        }
        //        NumberOfWaves--;
        //    }

           
        //}
    }

    void Update()
    {
        
    }
}
