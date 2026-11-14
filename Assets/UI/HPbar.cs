using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour
{
    [SerializeField]
    List<Image> Bers;

    private float WaitHPRedGege = 0.5f;
    private float DisHPRedGege = 0.5f;
    private float RedGage;
    private float RedEndNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RedEndNum = PlayerStatus.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if(RedEndNum > PlayerStatus.HP)
        {
            RedGage = RedEndNum;
            RedEndNum = PlayerStatus.HP;
            
            StartCoroutine(HPRedGegeEffect());
        }

        if (RedGage < PlayerStatus.HP)
        {
            RedGage = PlayerStatus.HP;
            RedEndNum = PlayerStatus.HP;
        }

        Bers[0].fillAmount = (float)PlayerStatus.HP / PlayerStatus.FinalMaxHP;
        Bers[1].fillAmount = (float)PlayerStatus.SP / PlayerStatus.FinalMaxSP;
        Bers[2].fillAmount = (float)PlayerStatus.GetEXP / PlayerSystem.NeedEXPCulc();
        Bers[3].fillAmount = (float)PlayerStatus.Fullness / PlayerStatus.FullnessMax;


        Bers[4].fillAmount = (float)RedGage / PlayerStatus.FinalMaxHP;
    }

    public IEnumerator HPRedGegeEffect()
    {
        yield return new WaitForSeconds(1f);
        while(RedGage > RedEndNum)
        {
            RedGage -= PlayerStatus.FinalMaxHP * Time.deltaTime / 5;
            yield return null;
        }
        if (RedGage < RedEndNum)
            RedGage = RedEndNum;
    }
}
