using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour
{
    [SerializeField]
    Image HPBer;
    
    [SerializeField]
    Image SPBer;
    
    [SerializeField]
    Image EXPBer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HPBer.fillAmount = (float)PlayerStatus.HP / PlayerStatus.MaxHP;
        SPBer.fillAmount = (float)PlayerStatus.SP / PlayerStatus.MaxSP;
        EXPBer.fillAmount = (float)PlayerStatus.GetEXP / PlayerStatus.NeedEXPCulc();
    }
}
