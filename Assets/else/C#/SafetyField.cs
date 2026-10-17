using UnityEngine;

public class SafetyField : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerSystem.PlayerRecovery();
        }
    }
}
