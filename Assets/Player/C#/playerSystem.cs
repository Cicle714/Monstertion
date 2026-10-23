using UnityEngine;

public class PlayerSystem : MonoBehaviour
{


    public static int FinalAttackPow()
    {
        float CalcPow = PlayerStatus.PlayerAttack;

        if (PlayerStatus.HornAttack)
        {
            CalcPow += PlayerStatus.HornAttackPow;
            if (PlayerStatus.faltHorn)
            {
                CalcPow *= 2;
            }
            if (PlayerStatus.faltRote)
            {
                CalcPow *= 2;
            }
        }
        if (PlayerStatus.BeamAttack)
        {
            CalcPow += PlayerStatus.BeamAttackPow;
            CalcPow *= BeamLevelMag();
        }


        return (int)CalcPow;
    }

    public static float BeamLevelMag()
    {
        float beamPow = 0;


        switch (PlayerStatus.BeamLevel)
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
        if (PlayerStatus.GetEXP >= PlayerSystem.NeedEXPCulc())
        {
            PlayerStatus.GetEXP -= PlayerSystem.NeedEXPCulc();
            PlayerSystem.LevelUP();
        }
    }

    public static void LevelUP()
    {
        PlayerStatus.NeedEXP++;
        PlayerStatus.PlayerLevel++;
        PlayerStatus.MaxHP += 10;
        PlayerStatus.HP = PlayerStatus.MaxHP;
        PlayerStatus.MaxSP += 5;
        PlayerStatus.SP = PlayerStatus.MaxSP;
        PlayerStatus.BeamAttackPow += 2;
        PlayerStatus.HornAttackPow += 2;
        PlayerStatus.Defense += 1;
    }

    public static void TimeFullnessDecrease()
    {
        PlayerStatus.Fullness -= Time.deltaTime / PlayerStatus.FullnessDecreaseNum;
    }
    public static void SkillFullnessDecrease()
    {
        if (PlayerStatus.BeforeUsingSP > PlayerStatus.SP)
        {
            PlayerStatus.Fullness -= (PlayerStatus.BeforeUsingSP - PlayerStatus.SP) / 20;
            PlayerStatus.BeforeUsingSP = PlayerStatus.SP;
        }
        else
        {
            PlayerStatus.BeforeUsingSP = PlayerStatus.SP;
        }
    }

    public static void PlayerRecovery()
    {
        PlayerStatus.HP += Time.deltaTime * 10;
        if (PlayerStatus.HP > PlayerStatus.MaxHP)
        {
            PlayerStatus.HP = PlayerStatus.MaxHP;
        }
    }

    public static void HitInvincible()
    {
        if(PlayerStatus.HitInvincibleCount > 0)
        {
            PlayerStatus.HitInvincibleCount -= Time.deltaTime;
        }
    }

}
