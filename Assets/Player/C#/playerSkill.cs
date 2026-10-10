using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{

    [SerializeField]
    private GameObject RoteObject;

    [SerializeField]
    public GameObject MyHorn;

    [SerializeField]
    public GameObject MyHorn2;

    private Vector3 FirstHornSize;
    private Vector3 FirstHornPos;

    public static bool PropellerCancel;
    public static bool RollXPush;
    public static bool RollZRPush;
    public static bool RollZLPush;
    public static float RollXCount;
    public static float RollXTime = 1;
    public static float RollZRCount;
    public static float RollZRTime = 1;
    public static float RollZLCount;
    public static float RollZLTime = 1;
    public static bool FastRollX;

    public static bool RollZReverse;
    public static float RollZReverseCount;
    public static float RollZReverseTime = 2;


    public static bool HornGrow = false;
    public static float HornGrowNum;

    public static bool BeamCharge;
    public static float BeamChargeCount;
    [SerializeField]
    private int[] BeamLevelTimes;
    public static float BeamChargeMaxTime;
    [SerializeField]
    private GameObject BeamChargeEffect;
    [SerializeField]
    private List<GameObject> BeamObject;

    

    private Quaternion PlayerRot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirstHornPos = MyHorn.transform.position;
        FirstHornSize = MyHorn.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerStatus.SkillFullnessDecrease();
        if (PlayerStatus.RecoilCount > 0)
            return;
        transform.localRotation = Quaternion.Euler(PlayerStatus.PlayerRote) * Quaternion.Euler(0, 0, PlayerStatus.SkillRoteZ) * Quaternion.Euler(PlayerStatus.SkillRoteX, 0, 0);
        HornGrowing();

        RollNormal();

    }



    public void OnHornGrowing(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HornGrow = true;
        }
        if (context.canceled)
        {
            HornGrow = false;
        }
    }

    void HornGrowing()
    {
        if (!PlayerStatus.BeamAttack)
        {
            PlayerStatus.HornAttack = true;
        }
        if (HornGrow)
        {
            HornGrowNum += Time.deltaTime * 2;
        }
        else
        {
            HornGrowNum -= Time.deltaTime * 10;
        }
        if (HornGrowNum < 1)
        {
            PlayerStatus.HornAttack = false;
            HornGrowNum = 1;
        }
        else if (HornGrowNum > 7.5f)
        {
            HornGrowNum = 7.5f;
        }
        if (PlayerContoroller.Prb.useGravity)
            MyHorn.transform.localScale = new Vector3(MyHorn.transform.localScale.x, 1 * HornGrowNum, MyHorn.transform.localScale.z);
        //MyHorn2.transform.localPosition = Vector3.up * (HornGrowNum - 1) / 10.0f;
    }

    public void Beam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            BeamCharge = true;
            BeamChargeEffect.SetActive(true);
        }
        if (BeamCharge)
        {
            BeamChargeCount += Time.deltaTime;
        }
        if (context.canceled)
        {
            BeamChargeEffect.SetActive(false);
            if (PlayerStatus.SP >= 50)
            {
                PlayerStatus.SP -= 50;
                PlayerStatus.BeamLevel = 2;
                PlayerStatus.RecoilCount = 1f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[0], MyHorn2.transform.position, transform.rotation * Quaternion.identity);
            }
        }

    }

    public void RollXStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (RollXPush)
            {
                if (PlayerStatus.SP >= 2 && !FastRollX)
                {
                    PlayerStatus.SP -= 2;
                    PlayerStatus.faltRote = true;
                }
            }
            else
            {
                if (PlayerStatus.SP >= 2)
                {
                    PlayerStatus.MoveRoll = true;
                    PlayerStatus.SP -= 2;
                    PlayerRot = transform.rotation;
                    RollXPush = true;
                    StartCoroutine(RollX());
                }
            }
        }
    }
    public void RollZRStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            RollZReverseCount = 0;
            RollZReverse = false;
                PlayerStatus.MoveRoll = true;
                PlayerRot = transform.rotation;
                StartCoroutine(RollZR());
            

        }
    }
    public static IEnumerator HornPropeller()
    {

        PlayerContoroller.Prb.useGravity = false;
        FindObjectOfType<PlayerSkill>().MyHorn.transform.localScale = new Vector3(1, 5, 1);
        FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(90, 0, 90);

        float roteCount = 0;

        while (!PlayerStatus.HornAttack && !PlayerStatus.BeamAttack && PlayerStatus.SP > 0 && !PropellerCancel)
        {
            if (!PlayerStatus.UseSP)
            {
                PlayerStatus.UseSP = true;
            }
            roteCount += Time.deltaTime * 360 * 5;
            FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(0, 0, 90 + roteCount);
            PlayerContoroller.PropellerUpPow += Time.deltaTime * PlayerStatus.PropellerPow;
            PlayerStatus.SP -= Time.deltaTime * 10;
            Debug.Log(PlayerStatus.SP);
            yield return null;
        }

        FindObjectOfType<PlayerSkill>().MyHorn.transform.localScale = new Vector3(1, 1, 1);
        FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(90, 0, 0);
        PlayerStatus.UseSP = false;
        PropellerCancel = false;
        PlayerContoroller.Prb.useGravity = true;
    }
    IEnumerator RollX()
    {
        PlayerStatus.MoveRollXStop = true;
        while (RollXCount < RollXTime)
        {
            float RollCountSpeed = 0;
            if (PlayerStatus.faltRote)
                RollCountSpeed += Time.deltaTime * 4;
            else
                RollCountSpeed += Time.deltaTime;

            RollXCount += RollCountSpeed;

            if (RollXCount > RollXTime)
            {
                RollXCount = RollXTime;
            }

            PlayerStatus.SkillRoteX = 360 * (RollXCount / RollXTime);


            yield return null;
        }

        PlayerStatus.MoveRollXStop = false;
        RollXPush = false;
        PlayerStatus.faltRote = false;
        PlayerStatus.MoveRoll = false;
        RollXCount = 0;

    }
    IEnumerator RollZR()
    {
        PlayerStatus.MoveRollZRStop = true;
        while (PlayerStatus.SkillRoteZ < 90)
        {

            PlayerStatus.SkillRoteZ += 90 * Time.deltaTime * 2;

            if (PlayerStatus.SkillRoteZ > 90)
            {
                PlayerStatus.SkillRoteZ = 90;
            }

            yield return null;
        }

        PlayerStatus.MoveRollZRStop = false;
        PlayerStatus.MoveRoll = false;
        RollZReverse = true;
        RollZRCount = 0;

    }

    public void RollNormal()
    {
        if (RollZReverse)
        {
            if (RollZReverseCount >= RollZReverseTime)
            {
                if (PlayerStatus.SkillRoteZ < 0)
                {

                    PlayerStatus.SkillRoteZ += 90 * Time.deltaTime;

                    if (PlayerStatus.SkillRoteZ > 0)
                    {
                        PlayerStatus.SkillRoteZ = 0;
                    }
                }
                if (PlayerStatus.SkillRoteZ > 0)
                {

                    PlayerStatus.SkillRoteZ -= 90 * Time.deltaTime;

                    if (PlayerStatus.SkillRoteZ < 0)
                    {
                        PlayerStatus.SkillRoteZ = 0;
                    }
                }
            }
            else
                RollZReverseCount += Time.deltaTime;

        }
        else
        {
            if (RollZReverseCount != 0)
            {
                RollZReverseCount = 0;
            }
        }


    }


}
