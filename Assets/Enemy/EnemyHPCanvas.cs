using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusCanvas : MonoBehaviour
{
    [SerializeField]
    EnemyStatus enemy;
    [SerializeField]
    Image greenBer;
    [SerializeField]
    Text LevelText;
    [SerializeField]
    Text HPText;
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.Big)
        {
            transform.position = enemy.transform.position + Vector3.up * 2;
        }
        else
            transform.position = enemy.transform.position + Vector3.up;
        greenBer.fillAmount = (float)enemy.HP / enemy.MaxHP;
        LevelText.text = "Lv"+enemy.Level;
        HPText.text = "HP:"+  enemy.HP +"/"+ enemy.MaxHP;
    }
}
