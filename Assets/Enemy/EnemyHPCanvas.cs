using UnityEngine;
using UnityEngine.UI;

public class EnemyHPCanvas : MonoBehaviour
{
    [SerializeField]
    EnemyStatus enemy;
    [SerializeField]
    Image greenBer;
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position + Vector3.up;
        greenBer.fillAmount = (float)enemy.HP / enemy.MaxHP;
    }
}
