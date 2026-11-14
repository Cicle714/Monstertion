using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    [SerializeField]
    private Image BlackOut;
    private bool SceneMove;

    [SerializeField]
    private Text StartText;
    private float BlinkTime = 0.5f;
    private float BlinkCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
    }


    private void Blink()
    {
        BlinkCount += Time.deltaTime;
        if(SceneMove)
        {
            BlinkCount += Time.deltaTime * 2;
        }
        if(BlinkCount >= BlinkTime)
        {
            Color TextColor = StartText.color;
            BlinkCount = 0;
            if(TextColor.a != 0)
            {
                TextColor.a = 0;
            }
            else
            {
                TextColor.a = 1;
            }

            StartText.color = TextColor;
        }
    }

    public void StartPush(InputAction.CallbackContext context)
    {
        if (context.started && !SceneMove)
        {
            SceneMove = true;
            StartCoroutine(StartSceneMove());
        }
    }

    private IEnumerator StartSceneMove()
    {
        float WaitTime = 1;
        float MoveTime = 1;
        float MoveCount = 0;
        Color colorB = BlackOut.color;

        yield return new WaitForSeconds(WaitTime);

        while (MoveCount < MoveTime) {
            MoveCount += Time.deltaTime;
            colorB.a = MoveCount;
            BlackOut.color = colorB;
            yield return null;
        }
        SceneManager.LoadScene("PlayScene");
    }

}
