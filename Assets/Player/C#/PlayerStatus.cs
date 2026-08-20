using UnityEngine;

public class PlayerStatus : MonoBehaviour
{


    static PlayerStatus instance;

    public static bool Attack;

    public static bool IsGround;
    public static bool MoveRollStop;

    public static int PlayerLevel;
    public static int PlayerAttack;
    public static int MaxHP = 50;
    public static int HP = 50;
    public static int MaxSP = 100;
    public static float SP = 100;
    public static int Attack01;
    public static int Attack02;
    public static int Defense;
    public static float MoveSpeed;
    public static float JumpPow = 1250;

    public static float PropellerPow = 0.25f;

    public static float GiveDamege;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null)
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
