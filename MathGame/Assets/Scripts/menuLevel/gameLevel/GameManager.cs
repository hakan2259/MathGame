using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject squarePrefab;

    [SerializeField]
    private Transform squaresPanel;

    private GameObject[] squaresArray = new GameObject[28];

    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform healthPanel;

    [SerializeField]
    private Transform resultPanel;

    [SerializeField]
    private Transform gameCompletePanel;


    [SerializeField]
    private Text questionText;

    List<int> textValuesList = new List<int>();

    int divisor, dividend;
    int whichQuestion;

    int number1, number2, total;
    int buttonValue;

    bool isButtonClick;

    int correctResult;

    int remaingHealth;

    string difficultyLevel;

    RemainingLifeManager remainingLifeManager;
    ScoreManager scoreManager;

    [SerializeField]
    private Sprite[] squareSprites;

    GameObject currentSquare;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip correctButtonSound;

    [SerializeField]
    private AudioClip wrongButtonSound;

    [SerializeField]


    private void Awake()
    {

        audioSource = GetComponent<AudioSource>();



        resultPanel.transform.GetComponent<RectTransform>().localScale = Vector2.zero;
        gameCompletePanel.transform.GetComponent<RectTransform>().localScale = Vector2.zero;

        remaingHealth = 3;
        remainingLifeManager = Object.FindObjectOfType<RemainingLifeManager>();
        scoreManager = Object.FindObjectOfType<ScoreManager>();
        remainingLifeManager.remainingLifeControl(remaingHealth);

    }

    void Start()
    {
        isButtonClick = false;
        squareCreate();


        questionPanel.GetComponent<RectTransform>().localScale = Vector2.zero;


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void squareCreate()
    {
        for (int i = 0; i < 28; i++)
        {
            GameObject square = Instantiate(squarePrefab, squaresPanel);
            square.transform.GetChild(1).GetComponent<Image>().sprite = squareSprites[Random.Range(0, squareSprites.Length)];

            square.transform.GetComponent<Button>().onClick.AddListener(() => btnClicked());

            squaresArray[i] = square;
        }

        generateRandomTextValue();
        StartCoroutine(DoFadeRoutine());
        Invoke("openQuestionPanel", 2.3f);






    }

    void btnClicked()
    {
        if (isButtonClick)
        {

            buttonValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            currentSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            ResultControl();

        }


    }
    void ResultControl()
    {
        if (buttonValue == correctResult)
        {
            audioSource.PlayOneShot(correctButtonSound);
            currentSquare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            currentSquare.transform.GetChild(0).GetComponent<Text>().text = "";
            currentSquare.transform.GetComponent<Button>().interactable = false;


            scoreManager.ScoreIncrease(difficultyLevel);
            textValuesList.RemoveAt(whichQuestion);
            Debug.Log(textValuesList.Count);
            openQuestionPanel();

        }
        else
        {
            audioSource.PlayOneShot(wrongButtonSound);
            remaingHealth--;
            remainingLifeManager.remainingLifeControl(remaingHealth);


        }
        if (remaingHealth == 0)
        {
            GameOver();
        }


    }

    void YouWon()
    {

        audioSource.Stop();

        gameCompletePanel.transform.GetComponent<RectTransform>().DOScale(1, 0.3f);


    }

    void GameOver()
    {
        audioSource.Stop();
        isButtonClick = false;
        resultPanel.transform.GetComponent<RectTransform>().DOScale(1, 0.3f);


    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var square in squaresArray)
        {
            square.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }


    }
    void generateRandomTextValue()
    {

        foreach (var text in squaresArray)
        {
            int randomValue = Random.Range(1, 33);

            textValuesList.Add(randomValue);

            text.transform.GetChild(0).GetComponent<Text>().text = randomValue.ToString();

        }

    }

    void openQuestionPanel()
    {
        askDivisionQuestion();
        isButtonClick = true;
        questionPanel.GetComponent<CanvasGroup>().DOFade(1, 1.0f).SetEase(Ease.OutBack);
        questionPanel.GetComponent<RectTransform>().localScale = Vector2.one;
        healthPanel.GetComponent<CanvasGroup>().DOFade(1, 2.0f);



    }
    void askDivisionQuestion()
    {
        divisor = Random.Range(2, 11);
        whichQuestion = Random.Range(0, textValuesList.Count);
        correctResult = textValuesList[whichQuestion];

        Debug.Log(whichQuestion);
        dividend = divisor * textValuesList[whichQuestion];

        if (dividend <= 40)
        {
            difficultyLevel = "kolay";
        }
        else if (dividend > 40 && dividend <= 80)
        {
            difficultyLevel = "orta";
        }
        else
        {
            difficultyLevel = "zor";
        }
        questionText.text = dividend.ToString() + " : " + divisor.ToString();


    }
    void askSumQuestion()
    {
        number1 = Random.Range(1, 11);
        number2 = Random.Range(1, 11);

        whichQuestion = Random.Range(0, textValuesList.Count);
        correctResult = textValuesList[whichQuestion];
        Debug.Log(whichQuestion);
        number1 = textValuesList[whichQuestion] - number2;
        questionText.text = number1.ToString() + " + " + number2.ToString();



    }
    void askMinusQuestion()
    {
        number1 = Random.Range(1, 11);
        if (number2 < number1)
        {
            number2 = Random.Range(1, 11);
        }
        whichQuestion = Random.Range(0, textValuesList.Count);
        correctResult = textValuesList[whichQuestion];
        Debug.Log(whichQuestion);
        number1 = textValuesList[whichQuestion] + number2;
        questionText.text = number1.ToString() + " - " + number2.ToString();


    }
    private void LateUpdate()
    {
        if (textValuesList.Count == 0)
        {
            YouWon();
        }
    }


}
