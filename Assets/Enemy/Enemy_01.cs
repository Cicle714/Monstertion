using UnityEngine;

public class Enemy_01 : EnemyStatus
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
        EnemyMove(player);
        DespawEnemy();
        EnemyDeath();
        FallEnemy();
    }

        

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && PlayerStatus.HitInvincibleCount <= 0)
        {
            if (AttackPow - PlayerStatus.Defense >= 1)
            {
                PlayerStatus.HP -= AttackPow - PlayerStatus.Defense;
            }
            else
            {
                PlayerStatus.HP -= 1;
            }
            PlayerStatus.HitInvincibleCount = 3;
        }
    }



}
