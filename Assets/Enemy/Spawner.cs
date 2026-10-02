using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int EnemyNum;
    [SerializeField]
    private int EnemyMaxNum = 10;

    [SerializeField]
    private float EnemySpawnTime = 2;
    private float EnemySpawnCount;

    [SerializeField]
    private GameObject EnemyObject;

    public List<GameObject> Enemys = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
    }


    void EnemySpawn()
    {
        if(EnemyMaxNum > EnemyNum)
        EnemySpawnCount += Time.deltaTime;
        else if(EnemySpawnCount != 0)
        {
            EnemySpawnCount = 0;
        }
        if (EnemySpawnCount > EnemySpawnTime)
        {

            EnemyNum++;
            EnemySpawnCount -= EnemySpawnTime;

            Vector3 SpawPos = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x) / 2, 1, Random.Range(-transform.localScale.z, transform.localScale.z) / 2) + transform.position;
            GameObject cloneEnemy = Instantiate(EnemyObject, SpawPos, Quaternion.identity);
            cloneEnemy.transform.parent = transform;
            Enemys.Add(cloneEnemy);

        }
    }



}
