using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public Rigidbody rb;
    public Canvas HPCanvas;
    public Image HPbar;
    public GameObject MyParent;
    public GameObject SpawParent;
    public Material mat;

    public int AttackPow;
    public float AttackSpeed;
    public float AttackSpeedCount;
    public int DefenseH;
    public int DefenseB;
    public float MoveSpeed;

    public float KnockBackResistance;

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
    public int BigProbability = 5;


    public GameObject DropObject;
    public float DropProbability;

    public bool Despaw;
    public float DespawTime = 1;
    public float DespawCount;

    [SerializeField]
    private GameObject ChildObject;

    public void BigEnemy()
    {
        if (Random.Range(0, 100) < BigProbability)
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
    public void EnemyMove(PlayerController player)
    {

        if (Vector3.Distance(player.transform.position, transform.position) <= SensingDistance)
        {
            if (MoveStartCount != 0)
            {
                MoveStartCount = 0;
            }
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            transform.position += ((transform.forward * MoveSpeed) + rb.linearVelocity) * Time.deltaTime;
        }
        else
        {
            if (MoveStartCount >= MoveStartFinalTime && !RandomMoveStart)
            {
                MoveStartCount = 0;
                transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                RandomMoveStart = true;
            }
            else if (!RandomMoveStart)
                MoveStartCount += Time.deltaTime;

            if (RandomMoveStart)
            {
                if (Vector3.Distance(transform.position + transform.forward, SpawParent.transform.position) > SpawParent.transform.localScale.x / 2)
                {
                    MoveOut = true;
                    transform.LookAt(new Vector3(SpawParent.transform.position.x, transform.position.y, SpawParent.transform.position.z));
                }
                else
                {
                    MoveOut = false;
                    MoveCount += Time.deltaTime;
                }
                transform.position += (transform.forward + rb.linearVelocity) * Time.deltaTime;


                if (MoveCount >= MoveTime)
                {
                    MoveCount = 0;
                    RandomMoveStart = false;
                    MoveStartFinalTime = Random.Range(MoveStartTime - MoveStartTimeNoise, MoveStartTime + MoveStartTimeNoise);
                }
            }

        }

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
            ItemDrop();
            Destroy(MyParent);
        }
    }

    public void ItemDrop()
    {
        int RandomNum = Random.Range(0, 100);
        if (RandomNum < DropProbability)
        {
            Instantiate(DropObject, transform.position, Quaternion.identity);
        }
    }

    public void DespawCheck()
    {

        EnemySpawner parent = GetComponentInParent<EnemySpawner>();
        if (!parent.EnemyGenerating && !Despaw)
        {
            Despaw = true;
            StartCoroutine(DespawEnemy());
        }
    }
    public IEnumerator DespawEnemy()
    {
        Color Matcolor = mat.color;

        EnemySpawner parent = GetComponentInParent<EnemySpawner>();
        while (DespawCount <= DespawTime)
            {
                DespawCount += Time.deltaTime;
                Matcolor.a = 1 - (DespawCount / DespawTime);
                mat.color = Matcolor;
                ChildObject.GetComponent<Renderer>().material.color = Matcolor;
                if (DespawCount > DespawTime)
                {
                    parent.EnemyNum--;
                    parent.Enemys.Remove(gameObject);
                    Destroy(MyParent);
                }
                yield return null;
            }
        
        
    }
    public void FallEnemy()
    {
        if (transform.position.y < -30)
        {
            EnemySpawner parent = GetComponentInParent<EnemySpawner>();

            parent.EnemyNum--;
            parent.Enemys.Remove(gameObject);
            Destroy(MyParent);
        }

    }

    public void KnockBack(GameObject obj, Rigidbody rb, float KnockBackPow)
    {
        if (Big)
        {
            KnockBackPow /= 4;
        }
        rb.linearVelocity = ((transform.position - (Vector3.up * transform.position.y)) - (obj.transform.position - (Vector3.up * obj.transform.position.y))).normalized * (KnockBackPow - KnockBackResistance);

        Debug.Log(rb.linearVelocity + "ŒÄ‚Î‚ê‚Æ‚é");
    }

}
