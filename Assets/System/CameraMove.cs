using UnityEngine;

public class CameraMove : MonoBehaviour
{

    PlayerContoroller player;

    [SerializeField]
    private Vector3 CameraPos;
    [SerializeField]
    private Vector3 CameraRot;
    void Start()
    {
        player = FindObjectOfType<PlayerContoroller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + CameraPos;
        transform.rotation = Quaternion.Euler(CameraRot);
    }
}
