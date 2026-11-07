using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Material mat;

    public enum ItemElement
    {
        Recovery,
        FullnessRecovery,
        AttackPow,
        Defense,
        Exp,
        Boom
    }

    [SerializeField]
    ItemElement element;

    [SerializeField]
    private int ItemPow;
    [SerializeField]
    private float DestroyTime = 15;
    private float DestroyCount;
    private float BlinkingNum = 10;
    private bool BlinkingStart;


    void Start()
    {
        mat = GetComponent<Renderer>().material;
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyCount += Time.deltaTime;
        if (DestroyCount >= BlinkingNum && !BlinkingStart)
        {
            BlinkingStart = true;
            StartCoroutine(Blinking());
        }
    }

    IEnumerator Blinking()
    {
        Color Matcolor = mat.color;
        float BlinkingInterval = 0.2f;
        int BlinkingNum = 0;
        while (true)
        {
            if (mat.color.a == 0)
            {
                Matcolor.a = 1;
                mat.color = Matcolor;
            }
            else
            {
                Matcolor.a = 0;
                mat.color = Matcolor;
            }
            BlinkingNum++;
            if (BlinkingNum >= 15)
            {
                BlinkingInterval /= 1.1f;
            }
            yield return new WaitForSeconds(BlinkingInterval);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            switch (element)
            {
                case ItemElement.Recovery:
                    PlayerStatus.HP += ItemPow;
                    if (PlayerStatus.HP > PlayerStatus.MaxHP)
                    {
                        PlayerStatus.HP = PlayerStatus.MaxHP;
                    }
                    break;
                case ItemElement.FullnessRecovery:
                    PlayerStatus.Fullness += ItemPow;
                    if (PlayerStatus.Fullness > PlayerStatus.FullnessMax)
                    {
                        PlayerStatus.Fullness = PlayerStatus.FullnessMax;
                    }
                    break;
                case ItemElement.AttackPow:
                    PlayerStatus.PlayerAttack += ItemPow;
                    break;
                case ItemElement.Defense:
                    PlayerStatus.Defense += ItemPow;
                    break;
                case ItemElement.Exp:
                    PlayerStatus.GetEXP += ItemPow;
                    break;
            }
            Destroy(gameObject);
        }
    }

}
