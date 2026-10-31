using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    PlayerController player;
    public int EnemyNum;
    [SerializeField]
    private int EnemyMaxNum = 10;

    [SerializeField]
    private float EnemySpawnTime = 2;
    private float EnemySpawnCount;

    public bool EnemyGenerating;

    [SerializeField]
    private List<GameObject> EnemyObjects;
    [SerializeField]
    private List<float> SpawProbability;

    public List<GameObject> Enemys = new List<GameObject>();

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
    }


    void EnemySpawn()
    {
        if(Vector3.Distance(transform.position,player.transform.position) < transform.localScale.magnitude * 0.75f)
        {
            EnemyGenerating = true; 
            if (EnemyMaxNum > EnemyNum)
                EnemySpawnCount += Time.deltaTime;
            else if (EnemySpawnCount != 0)
            {
                EnemySpawnCount = 0;
            }
            if (EnemySpawnCount > EnemySpawnTime)
            {

                Vector3 SpawPos = new Vector3(Random.Range((float)-transform.localScale.x, (float)transform.localScale.x) / 2, 1, Random.Range((float)-transform.localScale.z, (float)transform.localScale.z) / 2) + transform.position;
                if (Vector3.Distance(player.transform.position, SpawPos) <= 5)
                {
                    return;
                }
                EnemyNum++;
                EnemySpawnCount -= EnemySpawnTime;
                int RandomNum = Random.Range(0, 100);
                int EnemySpawNum = 0;
                for (int i = 0; i < SpawProbability.Count; i++)
                {
                    if (RandomNum < SpawProbability[i])
                    {
                        EnemySpawNum = i;
                        break;
                    }
                }
                GameObject cloneEnemy = Instantiate(EnemyObjects[EnemySpawNum], SpawPos, Quaternion.identity);
                cloneEnemy.GetComponentInChildren<EnemyStatus>().SpawParent = gameObject;
                cloneEnemy.transform.parent = transform;
                Enemys.Add(cloneEnemy);

            }
        }
        else
        {
            EnemyGenerating = false;
        }
      
    }



}
