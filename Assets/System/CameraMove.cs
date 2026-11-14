using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{

    PlayerController player;

    [SerializeField]
    private GameObject CenterObject;

    [SerializeField]
    private Vector3 CameraPos;
    [SerializeField]
    private Vector3 CameraRot; 
    [SerializeField]
    private Vector3 InputPow;

    private Vector2 CameraInput;
    public static Quaternion CameraDirectionY;
    public static float CameraDirectionX;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        CameraDirectionY = CenterObject.transform.rotation;
        CameraDirectionX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CenterObject.transform.position = player.transform.position;
        transform.localPosition = CameraPos;
        CameraDirectionX += CameraInput.x;
        if(CameraDirectionX > 15)
        {
            CameraDirectionX = 15;
        }else if(CameraDirectionX < -25) {
            CameraDirectionX = -25;
        }
        transform.localRotation = Quaternion.Euler(CameraRot);
        CameraDirectionY *= Quaternion.Euler(new Vector2(0, CameraInput.y * InputPow.y));
        CenterObject.transform.rotation = CameraDirectionY * Quaternion.Euler(Vector3.right * CameraDirectionX);
    }

    public void CameraRote(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().magnitude <= 0.1f)
        {
            CameraInput = Vector2.zero;
            return;
        }

        CameraInput = context.ReadValue<Vector2>();
        CameraInput = new Vector2(-CameraInput.y, CameraInput.x);




    }
}
