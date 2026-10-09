using System.Collections;
using UnityEngine;

public class PlayerBeam : MonoBehaviour
{
    private float BeamTime = 0.5f;
    private float BeamCount;

    private float BeamDestroyTime = 0.5f;
    private float BeamDestroyCount;



    private ParticleSystem[] Childs;

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
        Childs = GetComponentsInChildren<ParticleSystem>();
        
        yield return new WaitForSeconds(0.5f);

        float[] SizeX = new float[Childs.Length];
        for(int i = 0;i < Childs.Length; i++)
        {
            var tmpX = Childs[i].main.startSizeX;
            SizeX[i] = (float)tmpX.constant;

        }

        while (BeamDestroyCount < BeamDestroyTime)
        {
            
            BeamDestroyCount += Time.deltaTime;

            for (int i = 0; i < Childs.Length; i++)
            {
                var tmpX = Childs[i].main;
                Childs[i].transform.localScale = Vector3.Lerp(Vector3.one, Vector3.right, BeamDestroyCount / BeamDestroyTime);
                tmpX.startSizeX = SizeX[i] - ((BeamDestroyCount / BeamDestroyTime) * SizeX[i])  ; 
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
