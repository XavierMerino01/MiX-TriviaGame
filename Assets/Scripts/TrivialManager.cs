using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class TrivialQuestion
{
    public string questionFrase;
    public string answerA, answerB, answerC;
    public FloorButton.ButtonOption questionSolution;
    public AudioClip questionAudio;
}


public class TrivialManager : MonoBehaviour
{
    [SerializeField] public TrivialQuestion[] trivialQuestions;

    public enum GameState
    {
        Inactive,
        Explaining,
        WaitingAnswer,
        Complete
    }

    [Header("Puzzle Info")]
    public GameState triviaState;
    [SerializeField] public int currentQuestion = 0;
    private float questionDuration = 10;
    [HideInInspector] public int correctAnswers;

    public TextMeshPro Question, answerA, answerB, answerC;

    [Header("References")]
    [SerializeField] private GameTrigger exitTp;
    [SerializeField] private ParticleSystem exitPortal;
    [SerializeField] private Transform exitPortalTransform;
    AudioSource triviaAudioSource;
    public AudioManager myAudioManager;

    private void Start()
    {
        triviaAudioSource = GetComponent<AudioSource>();
        FillTextBoxes();
        currentQuestion += 1;
    }


    private void StartGame()
    {
        triviaState = GameState.Explaining;
        FillTextBoxes();
        currentQuestion += 1;
        StartCoroutine(WaitAndAsk());
    }

    IEnumerator WaitAndAsk()
    { 
       yield return new WaitForSeconds(3);
       
       BroadcastMessage("DeactivateHighlight");
       StartCoroutine(WaitForAnswer());
    }

    IEnumerator WaitAndEnd()
    {
        yield return new WaitForSeconds(3);

        BroadcastMessage("DeactivateHighlight");

        SFXManager.Instance.PlaySound("Puzzle_Done");

        triviaState = GameState.Complete;
        Question.text = "SUUUU! Has encertat "+ correctAnswers+"/10";
        answerA.text = "";
        answerB.text = "";
        answerC.text = "";

        if (correctAnswers > 3)
        {
            if (correctAnswers < 7)
            {
                GameManager.Instance.SpawnPrizeHearts(correctAnswers);
            }
            else
            {
                GameManager.Instance.SpawnPrizeHearts(7);
            }
        }
        exitTp.triggerType = GameTrigger.EventType.Teleport;
        Instantiate(exitPortal, exitPortalTransform);

    }

    private IEnumerator WaitForAnswer()
    {
        FillTextBoxes();
        if (trivialQuestions[currentQuestion].questionAudio != null)
        {
            PlayQuestionAudio();
        }
        triviaState = GameState.WaitingAnswer;
        float elapsedTime = 0f;

        while (elapsedTime < questionDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //TO DO: Time over error
    }

    private void FillTextBoxes()
    {
        Question.text = trivialQuestions[currentQuestion].questionFrase;
        answerA.text = trivialQuestions[currentQuestion].answerA;
        answerB.text = trivialQuestions[currentQuestion].answerB;
        answerC.text = trivialQuestions[currentQuestion].answerC;
    }

    private void CorrectAnswer()
    {
        if (trivialQuestions[currentQuestion].questionAudio != null) myAudioManager.FadeInCurrentTrack(1);

        SFXManager.Instance.PlaySound("OKAnswer");
        correctAnswers += 1;

        Question.text = "Correcte!";
        answerA.text = "" ;
        answerB.text = "";
        answerC.text = "";

        currentQuestion += 1;
        if (currentQuestion != trivialQuestions.Length) 
        {
            StartCoroutine(WaitAndAsk());
        }
        else
        {
            StartCoroutine(WaitAndEnd());
        }
    }

    private void IncorrectAnswer() 
    {
        if (trivialQuestions[currentQuestion].questionAudio != null) myAudioManager.FadeInCurrentTrack(1);

        SFXManager.Instance.PlaySound("WrongAnswer");

        Question.text = "CAGAAASTE";
        answerA.text = "";
        answerB.text = "";
        answerC.text = "";

        currentQuestion += 1;
        if (currentQuestion != trivialQuestions.Length)
        {
            StartCoroutine(WaitAndAsk());
        }
        else
        {
            StartCoroutine(WaitAndEnd());
        }
    }

    public void AnswerQuestion(FloorButton.ButtonOption givenAnswer, HighlightButton buttonRef)
    {
        if (triviaState == GameState.Inactive)
        {
            buttonRef.ActivateHighlight();
            SFXManager.Instance.PlaySound("Butt_Ingame");
            StartGame();
            return;
        }

        if (triviaState == GameState.Explaining || triviaState == GameState.Complete) return;

        if (currentQuestion == trivialQuestions.Length)
        {
            Debug.Log("Game is over");
            return;
        }

        buttonRef.ActivateHighlight();
        triviaAudioSource.Stop();
        triviaState = GameState.Explaining;
        SFXManager.Instance.PlaySound("Butt_Ingame");

        if (givenAnswer == trivialQuestions[currentQuestion].questionSolution)
        {
            StopCoroutine(WaitForAnswer());
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }
    }

    private void PlayQuestionAudio()
    {
        myAudioManager.FadeOutCurrentTrack(1);
        triviaAudioSource.clip = trivialQuestions[currentQuestion].questionAudio;
        triviaAudioSource.Play();
    }

}
