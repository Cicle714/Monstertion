using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{

    [SerializeField]
    public GameObject MyHorn;

    [SerializeField]
    public GameObject MyHorn2;

    private Vector3 FirstHornSize;
    private Vector3 FirstHornPos;

    public static bool PropellerCancel;
    public static bool RollXPush;
    public static float RollXCount;
    public static float RollXTime = 1;
    public static bool FastRollX;

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
        HornGrowing();
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
        if(HornGrowNum < 1)
        {
            PlayerStatus.HornAttack = false;
            HornGrowNum = 1;
        }else if(HornGrowNum > 5)
        {
            HornGrowNum = 5;
        }
        MyHorn.transform.localScale = new Vector3(MyHorn.transform.localScale.x, 1 * HornGrowNum, MyHorn.transform.localScale.z);
            MyHorn2.transform.localPosition = Vector3.up * (HornGrowNum - 1) / 17.5f;
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
                if (PlayerStatus.SP >= 5 && !FastRollX)
                {
                    PlayerStatus.SP -= 5;
                    FastRollX = true;
                }
            }
            else
            {
                if (PlayerStatus.SP >= 5)
                {
                    PlayerStatus.SP -= 5;
                    PlayerRot = transform.rotation;
                    RollXPush = true;
                    StartCoroutine(RollX());
                }
            }
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
        PlayerStatus.MoveRollStop = true;
        while (RollXCount < RollXTime)
        {
            float RollCountSpeed = 0;
            if (FastRollX)
                RollCountSpeed += Time.deltaTime * 4;
            else
                RollCountSpeed += Time.deltaTime;

            RollXCount += RollCountSpeed;

            transform.Rotate(new Vector3(360 * RollCountSpeed, 0, 0));

            if (RollXCount >= RollXTime)
            {
                transform.transform.rotation = PlayerRot;
            }
            yield return null;
        }

        PlayerStatus.MoveRollStop = false;
        RollXPush = false;
        FastRollX = false;
        RollXCount = 0;

    }

}
