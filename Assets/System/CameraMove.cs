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



    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CenterObject.transform.position = player.transform.position;
        transform.localPosition = CameraPos;
        transform.localRotation = Quaternion.Euler(CameraRot);
        CenterObject.transform.rotation = Quaternion.Euler(CameraInput * InputPow);
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
