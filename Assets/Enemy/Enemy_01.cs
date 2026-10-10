
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01 :EnemyStatus
{

    PlayerContoroller player;





    void Start()
    {
        if(Random.Range(0,10) <= 0)
        {
            BigEnemy(gameObject);
            transform.position += Vector3.up;
        }
        player = FindObjectOfType<PlayerContoroller>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();

        if(HP <= 0)
        {
            EnemySpawner parent = GetComponentInParent<EnemySpawner>();
            if (parent != null)
            {
                parent.EnemyNum--;
                parent.Enemys.Remove(gameObject);
            }
            PlayerStatus.GetEXP += EXP;
            Destroy(MyParent);
        }

    }

    void EnemyMove()
    {
       
        if(Vector3.Distance(player.transform.position,transform.position) <= SensingDistance)
        {
            if(MoveStartCount != 0)
            {
                MoveStartCount = 0;
            }
            transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
            transform.position += (transform.forward * MoveSpeed) * Time.deltaTime;
        }
        else
        {
            if (MoveStartCount >= MoveStartFinalTime && !RandomMoveStart)
            {
                MoveStartCount = 0;
                transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                RandomMoveStart = true;
            }
            else if(!RandomMoveStart)
                MoveStartCount += Time.deltaTime;

            if (RandomMoveStart)
            {
                if (Vector3.Distance(transform.position + transform.forward,SpawParent.transform.position) > SpawParent.transform.localScale.x / 2)
                {
                    MoveOut = true; 
                    transform.LookAt(new Vector3(SpawParent.transform.position.x, transform.position.y, SpawParent.transform.position.z));
                }
                else
                {
                    transform.position += transform.forward * Time.deltaTime;
                    MoveCount += Time.deltaTime;
                }
                if(MoveCount >= MoveTime)
                {
                    MoveCount = 0;
                    RandomMoveStart = false;
                    MoveStartFinalTime = Random.Range(MoveStartTime - MoveStartTimeNoise, MoveStartTime + MoveStartTimeNoise);
                }
            }

        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerContoroller>())
        {
            PlayerStatus.HP -= AttackPow - PlayerStatus.Defense;
        }
    }



}
