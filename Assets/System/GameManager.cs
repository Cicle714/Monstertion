using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    bool GameOver = false;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStatus.HP <= 0 && !GameOver)
        {
            GameOver = true;
            StartCoroutine(OnGameOver());
        }

        PlayerSystem.TimeFullnessDecrease();
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
        PlayerStatus.Fullness = PlayerStatus.FullnessMax;
        player.transform.position = PlayerStatus.RespawnPos;
        GameOver = false;
    }

}
