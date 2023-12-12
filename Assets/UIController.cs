using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject clearText;
    GameObject gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        clearText = GameObject.Find("clearText");
        gameOverText = GameObject.Find("gameOverText");
    }

    // Update is called once per frame
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
