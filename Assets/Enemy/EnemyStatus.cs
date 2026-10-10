using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class EnemyStatus : MonoBehaviour
{
    public Canvas HPCanvas;
    public Image HPbar;

    public int AttackPow;
    public float AttackSpeed;
    public float AttackSpeedCount;
    public float Defense;
    public float MoveSpeed;

    public float SensingDistance;

    public float MoveStartTime;
    public float MoveStartTimeNoise;
    public float MoveStartFinalTime;
    public float MoveStartCount;
    public bool RandomMoveStart;
    public float MoveTime;
    public float MoveCount;

    public int Level;
    public int MaxHP;
    public int HP;
    public int EXP;

    public bool Big;
    
    public void BigEnemy(GameObject Self)
    {
        Level *= 10;
        MaxHP *= 10;
        HP = MaxHP;
        AttackPow *= 10;
        EXP *= 20;
        Big = true;
        Self.transform.localScale = Vector3.one * 2;
    }

}
