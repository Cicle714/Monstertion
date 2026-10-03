using System.Collections;
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
    public static float RollZReverseTime = 1;


    public static bool HornGrow = false;
    public static float HornGrowNum;

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
        transform.localRotation = Quaternion.Euler(PlayerStatus.PlayerRote) * Quaternion.Euler(0, 0, PlayerStatus.SkillRoteZ) * Quaternion.Euler(PlayerStatus.SkillRoteX, 0, 0);
        HornGrowing();

        RollNormal();
    }



    public void OnHornGrowing(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerStatus.HornAttack = true;
            HornGrow = true;
        }
        if (context.canceled)
        {
            HornGrow = false;
        }
    }

    void HornGrowing()
    {
        if (HornGrow)
        {
            HornGrowNum += Time.deltaTime * 2;
        }
        else
        {
            HornGrowNum -= Time.deltaTime * 2;
        }
        if (HornGrowNum < 1)
        {
            PlayerStatus.HornAttack = false;
            HornGrowNum = 1;
        }
        else if (HornGrowNum > 5)
        {
            HornGrowNum = 5;
        }
        MyHorn.transform.localScale = new Vector3(MyHorn.transform.localScale.x, 1 * HornGrowNum, MyHorn.transform.localScale.z);
        MyHorn2.transform.localPosition = Vector3.up * (HornGrowNum - 1) / 17.5f;
    }

    public void Beam(InputAction.CallbackContext context)
    {

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
                    FastRollX = true;
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
                // RollXPush = true;
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
            roteCount += Time.deltaTime * 360 * 5;
            FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(0, 0, 90 + roteCount);
            PlayerContoroller.PropellerUpPow += Time.deltaTime * PlayerStatus.PropellerPow;
            PlayerStatus.SP -= Time.deltaTime * 10;
            Debug.Log(PlayerStatus.SP);
            yield return null;
        }

        FindObjectOfType<PlayerSkill>().MyHorn.transform.localScale = new Vector3(1, 1, 1);
        FindObjectOfType<PlayerSkill>().MyHorn.transform.localRotation = Quaternion.Euler(90, 0, 0);
        PropellerCancel = false;
        PlayerContoroller.Prb.useGravity = true;
    }
    IEnumerator RollX()
    {
        PlayerStatus.MoveRollXStop = true;
        while (RollXCount < RollXTime)
        {
            float RollCountSpeed = 0;
            if (FastRollX)
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
        FastRollX = false;
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
