using Unity.VisualScripting;
using UnityEngine;

public class Horn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyStatus>())
        {
            if (PlayerStatus.HornAttack)
            {
                Debug.Log("atatteru");
                PlayerStatus.BeamAttack = false;
                int KnockBackPow = 5;
                if (PlayerSkill.RollXPush)
                {
                    KnockBackPow += 2;
                }
                if (PlayerStatus.fastHorn)
                {
                    KnockBackPow += 3;
                }
                if (PlayerStatus.fastRote)
                {
                    KnockBackPow += 3;
                }
                other.gameObject.GetComponent<EnemyStatus>().KnockBack(gameObject, other.gameObject.GetComponent<EnemyStatus>().rb, KnockBackPow);
                if (other.GetComponent<EnemyStatus>().DefenseH < PlayerSystem.FinalAttackPow())
                {
                    other.GetComponent<EnemyStatus>().HP -= PlayerSystem.FinalAttackPow() - other.GetComponent<EnemyStatus>().DefenseH;
                }
                else
                {
                    other.GetComponent<EnemyStatus>().HP -= 1;
                }
            }
        }
    }
}
