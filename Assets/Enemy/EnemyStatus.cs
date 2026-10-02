using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class EnemyStatus : MonoBehaviour
{
    public Canvas HPCanvas;
    public Image HPbar;

    public float AttackPow;
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
    
}
