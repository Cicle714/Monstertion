using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    public static bool PropellerCancel;
    public static bool RollXPush;
    public static float RollXCount;
    public static float RollXTime = 1;
    public static bool FastRollX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void HornGrowing(InputAction.CallbackContext context)
    {

    }

    public void Beam(InputAction.CallbackContext context)
    {

    }

    public void RollStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (RollXPush)
            {
                FastRollX = true;
            }
            else
            {
                RollXPush = true;
                StartCoroutine(RollX());
            }
        }

    }
    public static IEnumerator HornPropeller()
    {

        PlayerContoroller.Prb.useGravity = false;
        while (!PlayerStatus.Attack && PlayerStatus.SP > 0 && !PropellerCancel)
        {
            PlayerContoroller.PropellerUpPow += Time.deltaTime * PlayerStatus.PropellerPow;
            PlayerStatus.SP -= Time.deltaTime * 10;
            Debug.Log(PlayerStatus.SP);
            yield return null;
        }
        PropellerCancel = false;
        PlayerContoroller.Prb.useGravity = true;
    }
    IEnumerator RollX()
    {
        PlayerStatus.MoveRollStop = true;
        while (RollXCount < RollXTime)
        {
            float RollCountSpeed = 0;
            if (FastRollX)
                RollCountSpeed += Time.deltaTime * 4;
            else
                RollCountSpeed += Time.deltaTime;

            RollXCount += RollCountSpeed;

            transform.Rotate(new Vector3(360 * RollCountSpeed, 0,0));
            yield return null;
        }
        PlayerStatus.MoveRollStop = false;
        RollXPush = false;
        FastRollX = false;
        RollXCount = 0;
        
    }

}
