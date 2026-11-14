using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumUI : MonoBehaviour
{
    [SerializeField]
    private List<Text> Nums;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Nums[0].text = "LV:" + PlayerStatus.PlayerLevel;
        Nums[1].text = "HP:" + (int)PlayerStatus.HP + "/" + PlayerStatus.FinalMaxHP;
        Nums[2].text = "SP:" + (int)PlayerStatus.SP + "/" + PlayerStatus.FinalMaxSP;
        Nums[3].text = "EXP:" + PlayerStatus.GetEXP + "/" + PlayerSystem.NeedEXPCulc();
        Nums[4].text = "–ž• “x:" + (int)PlayerStatus.Fullness + "/" + PlayerStatus.FullnessMax;
    }
}
