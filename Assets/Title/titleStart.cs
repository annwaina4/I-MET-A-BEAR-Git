using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleStart : MonoBehaviour
{
    public void startBtn()
    {
        SceneManager.LoadScene("main");
    }
}
