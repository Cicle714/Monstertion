using System.Collections;
using UnityEngine;

public class PlayerBeam : MonoBehaviour
{
    private float BeamTime = 0.5f;
    private float BeamCount;

    private float BeamDestroyTime = 0.5f;
    private float BeamDestroyCount;



    private Transform[] Childs;

    void Start()
    {
        Destroy(gameObject, 2f);
        StartCoroutine(BeamEffect());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BeamEffect()
    {
        Childs = GetComponentsInChildren<Transform>();
        yield return new WaitForSeconds(0.5f);
        while (BeamDestroyCount < BeamDestroyTime)
        {
            BeamDestroyCount += Time.deltaTime;

            for (int i = 0; i < Childs.Length; i++)
            {
                Childs[i].transform.localScale = Vector3.Lerp(Vector3.one, Vector3.up, BeamDestroyCount / BeamDestroyTime);
            }
            yield return null;
        }

        PlayerStatus.BeamAttack = false;
        Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyStatus>())
        {
            Debug.Log("Beammm");
            PlayerStatus.BeamAttack = true;
            PlayerStatus.HornAttack = false;
            other.GetComponent<EnemyStatus>().HP -= PlayerStatus.FinalAttackPow();
            
        }
    }


}
