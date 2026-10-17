using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class EnemyStatus : MonoBehaviour
{
    public Canvas HPCanvas;
    public Image HPbar;
    public GameObject MyParent;
    public GameObject SpawParent;

    public int AttackPow;
    public float AttackSpeed;
    public float AttackSpeedCount;
    public int DefenseH;
    public int DefenseB;
    public float MoveSpeed;

    public float SensingDistance;

    public float MoveStartTime;
    public float MoveStartTimeNoise;
    public float MoveStartFinalTime;
    public float MoveStartCount;
    public bool RandomMoveStart;
    public float MoveTime;
    public float MoveCount;

    public bool MoveOut;

    public int Level;
    public int MaxHP;
    public int HP;
    public int EXP;

    public bool Big;

    public GameObject DropObject;
    public float DropProbability;

    public float DespawTime = 5;
    public float DespawCount;

    public void BigEnemy()
    {
        if (Random.Range(0, 100) < 5)
        {
            Level *= 10;
            MaxHP *= 10;
            HP = MaxHP;
            AttackPow *= 5;
            DefenseB *= 10;
            DefenseH *= 10;
            EXP *= 20;
            Big = true;
            transform.localScale = Vector3.one * 2;
        }
        transform.position += Vector3.up;
    }

    public void EnemyDeath()
    {
        if (HP <= 0)
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

    public void ItemDrop()
    {
        int RandomNum = Random.Range(0, 100);
        if(RandomNum < DropProbability)
        {
            Instantiate(DropObject, transform.position, Quaternion.identity);
        }
    }

    public void DespawEnemy()
    {
        EnemySpawner parent = GetComponentInParent<EnemySpawner>();
        if (!parent.EnemyGenerating)
        {
            DespawCount += Time.deltaTime;
            if(DespawCount > DespawTime)
            {
                parent.EnemyNum--;
                parent.Enemys.Remove(gameObject);
                Destroy(MyParent);
            }
        }
        else
        {
            DespawCount = 0;
        }
    }


}
