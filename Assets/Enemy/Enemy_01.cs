
using UnityEngine;

public class Enemy_01 :EnemyStatus
{

    PlayerContoroller player;



    void Start()
    {
        player = FindObjectOfType<PlayerContoroller>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
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
                transform.position += transform.forward * Time.deltaTime;
                MoveCount += Time.deltaTime;
                if(MoveCount >= MoveTime)
                {
                    MoveCount = 0;
                    RandomMoveStart = false;
                    MoveStartFinalTime = Random.Range(MoveStartTime - MoveStartTimeNoise, MoveStartTime + MoveStartTimeNoise);
                }
            }

        }
       
    }


}
