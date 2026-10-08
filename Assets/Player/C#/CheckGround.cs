using UnityEngine;

public class CheckGround : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerStatus.IsGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerStatus.IsGround = false;
    }
}
