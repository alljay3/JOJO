using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{


    //[SerializeField] public int RoomDifficultyWeight;
    [SerializeField] public int[] NumberOfWavesWithDifficult;

    [Header("Doors")]
    [SerializeField] public GameObject[] Doors;
    [SerializeField] public GameObject doorEffect;
    [SerializeField] public GameObject[] PossibleNextRoom;


    [Header("Enemies")]
    [SerializeField] public GameObject[] enemyTypes;
    [SerializeField] public Transform[] enemySpawners;
    [SerializeField] public List<GameObject> statues;
    [SerializeField] public int WaveOfActivate;
 

    [Header("Trees")]
    [SerializeField] public GameObject[] treeTypes;
    [SerializeField] public Transform[] treeSpawners;
    [SerializeField] public float Xoffset1;
    [SerializeField] public float Xoffset2;
    [SerializeField] public float Yoffset1;
  

    [Header("Powerups")]
    [SerializeField] public GameObject[] buffs;

    [SerializeField] public List<GameObject> enemies;

    //private RoomVariants variants;
    private bool spawned;
    private bool treespawned;
    private bool doorDestroyed;
    private int NumberOfWaves;






    void Start()
    {

        NumberOfWaves = NumberOfWavesWithDifficult.Length;
        spawned = NumberOfWaves == 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");
        if (other.CompareTag("Player") && !spawned)
        {
            Debug.Log("ASSSSSSSSSSSSSSSSSSSSSSSSSSS");
            Debug.Log(NumberOfWaves);
            Debug.Log(NumberOfWavesWithDifficult.Length - NumberOfWaves);
            Debug.Log(NumberOfWavesWithDifficult[NumberOfWavesWithDifficult.Length - NumberOfWaves]);
            Debug.Log("aDDDDDDDDDDDDDDDDDDDDDDDD");
            SpawnEnemies(NumberOfWavesWithDifficult[NumberOfWavesWithDifficult.Length - NumberOfWaves]);
            NumberOfWaves--;
            CheckEnemies();
        }

        if (other.CompareTag("Player") && !treespawned)
        {
            treespawned = true;
            foreach (Transform spawner in treeSpawners)
            {
                int rand = Random.Range(0, treeTypes.Length);

                GameObject treeType = treeTypes[rand];

                float Xoffset = Random.Range(0.0f, 1.0f);
                float Yoffset = Random.Range(0.0f, 0.5f);
                GameObject tree = Instantiate(treeType, spawner.position + new Vector3(Xoffset, Yoffset, 0), Quaternion.identity) as GameObject;

                if (spawner.position.y > Yoffset && spawner.position.x > Xoffset1 && spawner.position.x < Xoffset2)
                {
                    tree.GetComponent<TreeScript>().isTopTree = true;
                }

                tree.transform.parent = transform;

            }

        }
    }

    void SpawnEnemies(int diff_weight)
    {
        Debug.Log("ASSSSSSSSSSSSSSSSSSSSSSSSSSS");
        Debug.Log(NumberOfWaves);
        Debug.Log(NumberOfWavesWithDifficult.Length - NumberOfWaves);
        Debug.Log(NumberOfWavesWithDifficult[NumberOfWavesWithDifficult.Length - NumberOfWaves]);
        Debug.Log("aDDDDDDDDDDDDDDDDDDDDDDDD");
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA");
        Debug.Log(diff_weight);
        int diff = diff_weight;
        while (diff > 0)
        {
            int rand = Random.Range(0, enemyTypes.Length);
            int randPosition = Random.Range(0, enemySpawners.Length);
            GameObject enemyType = enemyTypes[rand];
            GameObject enemy = Instantiate(enemyType, enemySpawners[randPosition].position, Quaternion.identity) as GameObject;
            enemy.transform.parent = transform;
            enemies.Add(enemy);
            diff -= enemy.GetComponent<Enemy>().DifficultyWeight;

        }
        //foreach (Transform spawner in enemySpawners)
        //{
        //    int rand = Random.Range(0, enemyTypes.Length);
        //    GameObject enemyType = enemyTypes[rand];
        //    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
        //    enemy.transform.parent = transform;
        //    enemies.Add(enemy);
        //}
        spawned = true;
    }

    public void CheckEnemies()
    {
      
        if (spawned)
        {
            if (enemies.Count == 0)
            {
                if (NumberOfWaves > 0)
                {

                    

                    SpawnEnemies(NumberOfWavesWithDifficult[NumberOfWavesWithDifficult.Length - NumberOfWaves]);
                    NumberOfWaves--;

                }
                else
                {
                    
                    DestroyDoors();
                }
            }
        }
       
       
      
       
    }
    public void CheckStatues()
    {
        if (NumberOfWaves == NumberOfWavesWithDifficult.Length - WaveOfActivate )
        {
            foreach (GameObject statue in statues)
            {
                statue.GetComponent<RangeStatuy>().Active();
                enemies.Add(statue);
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
        statues.RemoveAll(obj => obj == null);
        if (spawned && !doorDestroyed)
        {
            CheckEnemies();
            CheckStatues();
        }
        
    }

    void Update()
    {
        
    }
}
