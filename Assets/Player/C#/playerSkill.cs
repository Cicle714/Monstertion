using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public static bool RollXStock;

    public static bool RollZRHold;
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

    [SerializeField]
    private EnemyStatus LookOnEnemy;
    public static bool LookOn;

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
        PlayerSystem.SkillFullnessDecrease();
        if (PlayerStatus.RecoilCount > 0)
            return;
        OnLookOn();
        transform.localRotation = Quaternion.Euler(PlayerStatus.PlayerRote) * Quaternion.Euler(0, 0, PlayerStatus.SkillRoteZ) * Quaternion.Euler(PlayerStatus.SkillRoteX, 0, 0);
        HornGrowing();

        RollNormal();

        if (BeamCharge)
        {
            BeamChargeCount += Time.deltaTime;
        }
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
        if (PlayerStatus.SP <= 0)
        {
            HornGrow = false;
        }
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
        if (PlayerStatus.rb.useGravity)
            MyHorn.transform.localScale = new Vector3(MyHorn.transform.localScale.x, 1 * HornGrowNum, MyHorn.transform.localScale.z);
        //MyHorn2.transform.localPosition = Vector3.up * (HornGrowNum - 1) / 10.0f;
    }

    public void Beam(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerStatus.UseSP = true;
            BeamCharge = true;
            BeamChargeEffect.SetActive(true);
        }
        if (context.canceled)
        {
            BeamChargeEffect.SetActive(false);
            if (BeamChargeCount >= 2 && PlayerStatus.SP >= 100)
            {
                PlayerStatus.SP -= 100;
                PlayerStatus.BeamLevel = 4;
                PlayerStatus.RecoilCount = 2f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[4], MyHorn2.transform.position, transform.rotation * Quaternion.identity);
            }
            else if (BeamChargeCount >= 1.5f && PlayerStatus.SP >= 75)
            {

                PlayerStatus.SP -= 75;
                PlayerStatus.BeamLevel = 3;
                PlayerStatus.RecoilCount = 1.5f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[3], MyHorn2.transform.position, transform.rotation * Quaternion.identity);
            }
            else if (BeamChargeCount >= 1f && PlayerStatus.SP >= 50)
            {
                PlayerStatus.SP -= 50;
                PlayerStatus.BeamLevel = 2;
                PlayerStatus.RecoilCount = 1f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[2], MyHorn2.transform.position, transform.rotation * Quaternion.identity);

            }
            else if (BeamChargeCount >= 0.5f && PlayerStatus.SP >= 25)
            {

                PlayerStatus.SP -= 25;
                PlayerStatus.BeamLevel = 1;
                PlayerStatus.RecoilCount = 0.5f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[1], MyHorn2.transform.position, transform.rotation * Quaternion.identity);
            }
            else if (BeamChargeCount >= 0 && PlayerStatus.SP >= 10)
            {
                PlayerStatus.SP -= 10;
                PlayerStatus.BeamLevel = 0;
                PlayerStatus.RecoilCount = 0.5f;
                PlayerStatus.BeamAttack = true;
                Instantiate(BeamObject[0], MyHorn2.transform.position, transform.rotation * Quaternion.identity);
            }
            PlayerStatus.UseSP = false;
            BeamCharge = false;
            BeamChargeCount = 0;
        }

    }

    public void RollXStart(InputAction.CallbackContext context)
    {
        Debug.Log("Yooo");
        if (context.started)
        {
            RollXStock = true;
            if (RollXPush)
            {

                if (PlayerStatus.SP >= 2 && !FastRollX)
                {
                    PlayerStatus.SP -= 2;
                    FastRollX = true;
                    PlayerStatus.fastRote = true;
                }
            }
            else
            {
                if (PlayerStatus.SP >= 2)
                {
                    PlayerStatus.MoveRollXStop = false;
                    PlayerStatus.SP -= 2;
                    PlayerRot = transform.rotation;
                    RollXPush = true;
                    StartCoroutine(RollX());
                }
            }
        }
        if (context.canceled)
        {
            RollXStock = false;
        }

    }
    public void RollZRStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RollZRHold = true;
            RollZReverse = false;
            PlayerStatus.MoveRollZRStop = false;
            PlayerRot = transform.rotation;
            StartCoroutine(RollZR());


        }
        if (context.canceled)
        {
            RollZRHold = false;
        }
    }
    public static IEnumerator HornPropeller()
    {

        PlayerStatus.rb.useGravity = false;
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
            PlayerController.PropellerUpPow += Time.deltaTime * PlayerStatus.PropellerPow;
            PlayerStatus.SP -= Time.deltaTime * 10;
            Debug.Log(PlayerStatus.SP);
            yield return null;
        }

        FindObjectOfType<PlayerSkill>().MyHorn.transform.localScale = new Vector3(1, 1, 1);
        FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(90, 0, 0);
        PlayerStatus.UseSP = false;
        PropellerCancel = false;
        PlayerStatus.rb.useGravity = true;
    }
    IEnumerator RollX()
    {
        while (RollXCount < RollXTime)
        {
            float RollCountSpeed = 0;
            if (PlayerStatus.fastRote)
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
        if (PlayerSkill.RollXStock && PlayerStatus.SP >= 2)
        {
            if (FastRollX && PlayerStatus.SP >= 4)
            {
                PlayerStatus.SP -= 2;
            }
            else if (FastRollX)
            {
                PlayerStatus.MoveRollXStop = true;
                RollXPush = false;
                FastRollX = false;
                PlayerStatus.fastRote = false;
                RollXCount = 0;
            }
            PlayerStatus.SP -= 2;

            RollXCount = 0;
            yield return StartCoroutine(RollX());

        }
        else
        {

            PlayerStatus.MoveRollXStop = true;
            RollXPush = false;
            FastRollX = false;
            PlayerStatus.fastRote = false;
            RollXCount = 0;
        }

    }
    IEnumerator RollZR()
    {
        while (PlayerStatus.SkillRoteZ < 90)
        {

            PlayerStatus.SkillRoteZ += 90 * Time.deltaTime * 2;

            if (PlayerStatus.SkillRoteZ > 90)
            {
                PlayerStatus.SkillRoteZ = 90;
            }

            yield return null;
        }

        RollZReverse = true;
        RollZRCount = 0;

    }

    public void RollNormal()
    {
        if (RollZReverse)
        {
            Debug.Log("RRRR"+ RollZRHold);
            Debug.Log(RollZReverseCount >= RollZReverseTime);
            if (RollZReverseCount >= RollZReverseTime && !RollZRHold)
            {

                PlayerStatus.MoveRollZRStop = true;
                PlayerStatus.MoveRollZLStop = true;
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
    public void LookOnStart(InputAction.CallbackContext context)
    {
        if (context.started) {
            if (!LookOn)
            {
                float MinDistance = 10;
                EnemyStatus[] enemys = FindObjectsOfType<EnemyStatus>();
                for (int i = 0; i < enemys.Length; i++)
                {
                    float DistanceNum = Vector3.Distance(transform.position + transform.forward, enemys[i].transform.position);
                    if (DistanceNum <= MinDistance)
                    {
                        MinDistance = DistanceNum;
                        LookOnEnemy = enemys[i];
                        LookOn = true;
                    }
                }
                if(LookOnEnemy == null)
                {
                    LookOn = false;
                }
            }
            else
            {
                LookOn = false;
            }
        }
    }

    private void OnLookOn()
    {
        if (LookOn)
        {
            if(LookOnEnemy == null)
            {
                LookOn = false;
                return;
            }
            PlayerStatus.PlayerRote = Quaternion.LookRotation(new Vector3(LookOnEnemy.transform.position.x, transform.position.y, LookOnEnemy.transform.position.z) - transform.position).eulerAngles;
        }
        else
        {
            LookOnEnemy = null;
        }
    }

}
