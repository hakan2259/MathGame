using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour
{

    public void StartAgain()
    {
        SceneManager.LoadScene("gameLevel");
    }
    public void BackToTheMainMenu()
    {
        SceneManager.LoadScene("menuLevel");

    }
}
