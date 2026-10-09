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
        Nums[1].text = "HP:" + PlayerStatus.HP + "/" + PlayerStatus.MaxHP;
        Nums[2].text = "SP:" + (int)PlayerStatus.SP + "/" + PlayerStatus.MaxSP;
        Nums[3].text = "EXP:" + PlayerStatus.GetEXP + "/" + PlayerStatus.NeedEXPCulc();
    }
}
