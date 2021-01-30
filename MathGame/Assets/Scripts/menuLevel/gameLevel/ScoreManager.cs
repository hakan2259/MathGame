using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    int totalScore;
    int scoreIncrease;

    [SerializeField]
    private Text scoreText;

    

    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text=totalScore.ToString();
    }

    public void ScoreIncrease(string difficultyLevel){

        switch(difficultyLevel){
            case "kolay":
            scoreIncrease = 5;
            break;
            case "orta":
            scoreIncrease = 10;
            break;
            case "zor":
            scoreIncrease = 15;
            break;

        }
        totalScore += scoreIncrease;
        scoreText.text = totalScore.ToString();

    }

  
}
