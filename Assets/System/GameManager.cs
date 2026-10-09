using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerContoroller player;
    bool GameOver = false;
    void Start()
    {
        player = FindObjectOfType<PlayerContoroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStatus.HP <= 0 && !GameOver)
        {
            GameOver = true;
            StartCoroutine(OnGameOver());
        }
    }

    IEnumerator OnGameOver()
    {
        PlayerRestart();
        yield return null;
    }

    private void PlayerRestart()
    {
        PlayerStatus.HP = PlayerStatus.MaxHP;
        PlayerStatus.SP = PlayerStatus.MaxSP;
        player.transform.position = PlayerStatus.RespawnPos;
        GameOver = false;
    }

}
