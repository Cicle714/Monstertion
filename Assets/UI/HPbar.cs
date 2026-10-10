using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour
{
    [SerializeField]
    List<Image> Bers;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Bers[0].fillAmount = (float)PlayerStatus.HP / PlayerStatus.MaxHP;
        Bers[1].fillAmount = (float)PlayerStatus.SP / PlayerStatus.MaxSP;
        Bers[2].fillAmount = (float)PlayerStatus.GetEXP / PlayerStatus.NeedEXPCulc();
        Bers[3].fillAmount = (float)PlayerStatus.Fullness / PlayerStatus.FullnessMax;
    }
}
