using UnityEngine;

public class CameraMove : MonoBehaviour
{

    PlayerController player;

    [SerializeField]
    private Vector3 CameraPos;
    [SerializeField]
    private Vector3 CameraRot;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + CameraPos;
        transform.rotation = Quaternion.Euler(CameraRot);
    }
}
