using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 moveInput = Vector2.zero; //Å@à⁄ìÆ
    public static float PropellerUpPow;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private bool KeyMove;


    private bool IsPush;

    private bool MoveStart;

    private float DashCount;
    private float SetDashTime = 0.025f;

    private bool Dash = false;

    private bool PropellerCharge;
    private float PropellerChargeCount;
    private float PropellerChargeTime = 1f;

    void Start()
    {
        PlayerStatus.RespawnPos = transform.position;
        PlayerStatus.rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSystem.CheckEXP();
        PlayerSystem.HitInvincible();
        PlayerSystem.LowestFallSpeed();
        PlayerSystem.Fullness();
        if (PlayerStatus.RecoilCount > 0)
        {
            PlayerStatus.RecoilCount-= Time.deltaTime;
            return;
        }

        Vector3 move = new Vector3(moveInput.x, PropellerUpPow, moveInput.y) * PlayerSystem.FinelSpeed() * Time.deltaTime;
        if (Dash)
        {
            move *= PlayerStatus.DashSpeed;
        }
        var move2 = transform.position + move;
       
        transform.position = move2;

        if (PlayerStatus.rb.useGravity && PropellerUpPow > 0)
        {
            PropellerUpPow -= Time.deltaTime * 20;
        }

        SPRecovery();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().magnitude <= 0.1f && !KeyMove)
        {
            DashCount = 0;
            Dash = false;
            MoveStart = false;
            moveInput = Vector2.zero;
            return;
        }
        KeyMove = false;
        if (SetDashTime > DashCount)
        {
            if (context.ReadValue<Vector2>().magnitude >= 1f)
            {
                Dash = true;
            }
            DashCount += Time.deltaTime;
        }

        moveInput = context.ReadValue<Vector2>();

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        if (PlayerStatus.MoveRollXStop && PlayerStatus.MoveRollZLStop && PlayerStatus.MoveRollZRStop)
            PlayerStatus.PlayerRote = Quaternion.LookRotation(direction).eulerAngles;


    }
    public void OnDashKey(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            Dash = true;
        }
        if (context.canceled)
        {
            Dash = false;
        }
    }

    public void OnKeyMove(InputAction.CallbackContext context)
    {
        KeyMove = true;
        moveInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            if(context.ReadValue<Vector2>().magnitude <= 0.1f)
            {
                MoveStart = false;
                moveInput = Vector2.zero;
            }
            return;
        }

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        if (PlayerStatus.MoveRollXStop && PlayerStatus.MoveRollZLStop && PlayerStatus.MoveRollZRStop)
            PlayerStatus.PlayerRote = Quaternion.LookRotation(direction).eulerAngles;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            PropellerChargeCount = Time.time;

        if (context.canceled)
        {
            if (PlayerStatus.IsGround)
            {
                if (Time.time - PropellerChargeCount > PropellerChargeTime)
                {
                    StartCoroutine(PlayerSkill.HornPropeller());
                }
                else
                {
                    if (Time.time - PropellerChargeCount > 0.1f)
                        PlayerStatus.rb.AddForce(Vector3.up * PlayerStatus.JumpPow);
                    else
                        PlayerStatus.rb.AddForce(Vector3.up * PlayerStatus.JumpPow * 0.75f);
                }
            }
            else
            {
                PlayerSkill.PropellerCancel = true;
            }
                PropellerChargeCount = 0;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            IsPush = false;
            return;
        }
        if (!context.started) return;
        if (!IsPush)
        {
            IsPush = true;
        }
    }

    void SPRecovery()
    {
        if (!PlayerStatus.HornAttack && !PlayerStatus.BeamAttack && !PlayerStatus.UseSP)
        {
            PlayerStatus.SPRecoveryDelayCount += Time.deltaTime;
            if (PlayerStatus.SPRecoveryDelayCount >= PlayerStatus.SPRecoveryDelayTime)
            {
                PlayerStatus.SP += (PlayerStatus.MaxSP / 10) * Time.deltaTime;
                if (moveInput.magnitude == 0)
                {
                    PlayerStatus.SP += (PlayerStatus.MaxSP / 10) * Time.deltaTime * 2;
                }
            }
        }
        else if (PlayerStatus.SPRecoveryDelayCount != 0)
        {
            PlayerStatus.SPRecoveryDelayCount = 0;
        }
        if (PlayerStatus.SP > PlayerStatus.MaxSP)
        {
            PlayerStatus.SP = PlayerStatus.MaxSP;
        }
    }

    


}
