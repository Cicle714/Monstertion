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
                other.GetComponent<EnemyStatus>().HP -= PlayerStatus.FinalAttackPow();
            }
        }
    }
}
