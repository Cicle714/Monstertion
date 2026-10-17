
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_01 :EnemyStatus
{

    PlayerController player;





    void Start()
    {

        BigEnemy();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        DespawEnemy();
        EnemyDeath();

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
                    MoveOut = false;
                    MoveCount += Time.deltaTime;
                }
                    transform.position += transform.forward * Time.deltaTime;
                    
                
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
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (AttackPow - PlayerStatus.Defense >= 1)
            {
                PlayerStatus.HP -= AttackPow - PlayerStatus.Defense;
            }
            else
            {
                PlayerStatus.HP -= 1;
            }
        }
    }



}
