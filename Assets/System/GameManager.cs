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
        if (PlayerStatus.HP <= 0 || player.transform.position.y < -30f)
        {
            if (!GameOver)
            {
                GameOver = true;
                StartCoroutine(OnGameOver());
            }
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
        PlayerStatus.HP = PlayerStatus.FinalMaxHP;
        PlayerStatus.SP = PlayerStatus.FinalMaxSP;
        PlayerStatus.Fullness = PlayerStatus.FullnessMax;
        player.transform.position = PlayerStatus.RespawnPos;
        GameOver = false;
    }

}
