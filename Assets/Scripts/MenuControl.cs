using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public Text infoText;
    private int pressCounter = 0;
    public Button startButton, infoButton, exitButton;
    private Vector3 initPosStart, initPosInfo, initPosExit;

    private void Start()
    {
        initPosStart = startButton.transform.position;
        initPosInfo = infoButton.transform.position;
        initPosExit = exitButton.transform.position;

        startButton.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        infoButton.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
        exitButton.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startButton.interactable = true;
            infoButton.interactable = true;
            exitButton.interactable = true;
            startButton.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            infoButton.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            exitButton.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            startButton.transform.position = initPosStart;
            infoButton.transform.position = initPosInfo;
            exitButton.transform.position = initPosExit;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Info()
    {
        pressCounter++;
        if(pressCounter % 2 == 1)
        {
            infoText.text = "This game has extremely simple mechanics. " +
                "All you have to do is tapping the screen. " +
                "Avoid to collide with different colors and other obstacles. " +
                "Have fun!!!";
        }
        else
        {
            infoText.text = "";
        }
    }

}
