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
    public static bool MoveRollXStop = true;
    public static bool MoveRollZRStop = true;
    public static bool MoveRollZLStop = true;

    public static int PlayerLevel = 1;
    public static int CheckLevel = 1;
    public static int GetEXP = 0;
    public static int NeedEXP = 5;
    public static int PlayerAttack = 5;
    public static int MaxHP = 50;
    public static float HP = 50;
    public static int MaxSP = 100;
    public static float SP = 100;
    public static int HornAttackPow = 0;
    public static int BeamAttackPow = 0;
    public static int Defense = 5;
    public static float MoveSpeed;
    public static float JumpPow = 1250;
    public static float FullnessMax = 100;
    public static float Fullness = 100;

    public static float DashSpeed = 1.5f;

    public static float FullnessDecreaseNum = 30; //xïbÇ≈FullnessÇ1å∏è≠Ç∑ÇÈ

    public static bool UseSP = false;
    public static float BeforeUsingSP;

    public static float fastHornMagPow = 2.0f;
    public static float fastRoteMagPow = 2.0f;

    public static bool fastHorn = false;
    public static bool fastRote = false;

    public static float PropellerPow = 0.25f;

    public static float SPRecoveryDelayTime = 0.5f;
    public static float SPRecoveryDelayCount = 0;

    public static Vector3 RespawnPos;


    public static float RecoilCount;

    public static float HitInvincibleCount;


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

    
    

}
