using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public static Rigidbody rb;



    static PlayerStatus instance;

    public static Vector3 PlayerRote;
    public static float SkillRoteX;
    public static float SkillRoteY;
    public static float SkillRoteZ;

    public static bool HornAttack = false;
    public static bool BeamAttack = false;
    public static int BeamLevel = 0;


    public static bool IsGround;
    public static bool MoveRoll;
    public static bool MoveRollXStop;
    public static bool MoveRollZRStop;
    public static bool MoveRollZLStop;

    public static int PlayerLevel = 1;
    public static int CheckLevel = 1;
    public static int GetEXP = 0;
    public static int NeedEXP = 5;
    public static int PlayerAttack = 10;
    public static int MaxHP = 50;
    public static int HP = 50;
    public static int MaxSP = 100;
    public static float SP = 100;
    public static int HornAttackPow = 0;
    public static int BeamAttackPow = 0;
    public static int Defense = 5;
    public static float MoveSpeed;
    public static float JumpPow = 1250;

    public static bool UseSP = false;

    public static float fastHornMagPow = 2.0f;
    public static float fastRoteMagPow = 2.0f;

    public static bool faltHorn = false;
    public static bool faltRote = false;

    public static float PropellerPow = 0.25f;

    public static float SPRecoveryDelayTime = 0.5f;
    public static float SPRecoveryDelayCount = 0;


    public static float RecoilCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = new PlayerStatus();
        }
        else
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int FinalAttackPow()
    {
        float CalcPow = PlayerAttack;

        if (HornAttack)
        {
            CalcPow += HornAttackPow;
            if (faltHorn)
            {
                CalcPow *= 2;
            }
            if (faltRote)
            {
                CalcPow *= 2;
            }
        }
        if (BeamAttack)
        {
            CalcPow += BeamAttackPow;
            CalcPow *= BeamLevelMag();
        }


        return (int)CalcPow;
    }

    public static float BeamLevelMag()
    {
        float beamPow = 0;


        switch (BeamLevel)
        {
            case 0:
                beamPow = 0.25f;
                break;
            case 1:
                beamPow = 1.0f;
                break;
            case 2:
                beamPow = 2.5f;
                break;
            case 3:
                beamPow = 5.0f;
                break;
            case 4:
                beamPow = 10.0f;
                break;
        }
        return beamPow;
    }

    public static int NeedEXPCulc()
    {
        return (int)(PlayerStatus.NeedEXP * (PlayerStatus.PlayerLevel));
    }

    public static void CheckEXP()
    {
        if(PlayerStatus.GetEXP >= PlayerStatus.NeedEXPCulc())
        {
            GetEXP -= PlayerStatus.NeedEXPCulc();
            PlayerStatus.NeedEXP++;
            PlayerStatus.LevelUP();
        }
    }

    public static void LevelUP()
    {
        PlayerStatus.PlayerLevel++;
        PlayerStatus.MaxHP += 10;
        PlayerStatus.HP = PlayerStatus.MaxHP;
        PlayerStatus.MaxSP += 5;
        PlayerStatus.SP = PlayerStatus.MaxSP;
        PlayerStatus.BeamAttackPow += 2;
        PlayerStatus.HornAttackPow += 2;

    }


}
