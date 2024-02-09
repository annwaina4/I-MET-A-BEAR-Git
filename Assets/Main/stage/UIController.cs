using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject clearText;
    GameObject gameOverText;
    void Start()
    {
        clearText = GameObject.Find("clearText");
        gameOverText = GameObject.Find("gameOverText");
    }

    void Update()
    {
        
    }
    public void gameClear()
    {
        clearText.GetComponent<Text>().text = "GAME  CLEAR";
    }

    public void gameOver()
    {
        gameOverText.GetComponent<Text>().text = "GAME  OVER";
    }
}
