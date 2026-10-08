using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoroller : MonoBehaviour
{
    public static Rigidbody Prb;

    private Vector2 moveInput = Vector2.zero; //Å@à⁄ìÆ
    public static float PropellerUpPow;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private bool KeyMove;

    float speed = 5;

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
        Prb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStatus.RecoilCount > 0)
        {
            PlayerStatus.RecoilCount-= Time.deltaTime;
            return;
        }

        var move = transform.position + new Vector3(moveInput.x, PropellerUpPow, moveInput.y) * speed * Time.deltaTime;
        transform.position = move;

        if (Prb.useGravity && PropellerUpPow > 0)
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
        if (Dash)
            moveInput *= 2f;

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        if (!PlayerStatus.MoveRoll && PlayerStatus.IsGround)
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

            if (Dash)
            moveInput *= 2f;

        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        if (!PlayerStatus.MoveRoll)
            PlayerStatus.PlayerRote = Quaternion.LookRotation(direction).eulerAngles;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            PropellerChargeCount = Time.time;

        if (context.canceled && PlayerStatus.IsGround)
        {
            if (Time.time - PropellerChargeCount > PropellerChargeTime)
            {
                StartCoroutine(PlayerSkill.HornPropeller());
            }
            else
            {
                PlayerSkill.PropellerCancel = true;
                if (Time.time - PropellerChargeCount > 0.1f)
                    Prb.AddForce(Vector3.up * PlayerStatus.JumpPow);
                else
                    Prb.AddForce(Vector3.up * PlayerStatus.JumpPow * 0.75f);
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
        if (!PlayerStatus.HornAttack && !PlayerStatus.BeamAttack && !Dash)
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
