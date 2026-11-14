using UnityEngine;

public class Enemy_01 : EnemyStatus
{

    PlayerController player;





    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<Renderer>().material;
        BigEnemy();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DespawCheck();
        EnemyDeath();
        FallEnemy();
    }

    private void FixedUpdate()
    {

        EnemyMove(player);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && PlayerStatus.HitInvincibleCount <= 0)
        {
            if (AttackPow - PlayerStatus.FinalDefense >= 1)
            {
                PlayerStatus.HP -= AttackPow - PlayerStatus.FinalDefense;
            }
            else
            {
                PlayerStatus.HP -= 1;
            }
            PlayerStatus.HitInvincibleCount = 3;
        }
    }



}
