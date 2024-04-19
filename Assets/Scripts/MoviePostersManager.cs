using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MovieGuess
{
    public MovieButton.MovieOption movieOption;
    public AudioClip movieAudio;
}


public class MoviePostersManager : MonoBehaviour
{

    [SerializeField] private List<MovieGuess> movieList = new List<MovieGuess>();

    public enum GameState
    {
        Inactive,
        Explaining,
        WaitingAnswer,
        Complete
    }
    public GameState gameState;

    AudioSource audioSource;

    [SerializeField] private GameTrigger exitTp;
    [SerializeField] private ParticleSystem exitPortal;
    [SerializeField] private Transform exitPortalTransform;

    public AudioManager myAudioManager;
    public TextMeshPro buttonText;

    private int movieIndex;
    private int correctGuessCount;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        buttonText.text = "Restart song";
        myAudioManager.FadeOutCurrentTrack(1);
        PickAndPlayMusic();
        gameState = GameState.WaitingAnswer;
    }

    public void CompleteGame()
    {
        myAudioManager.FadeInCurrentTrack(1);
        SFXManager.Instance.PlaySound("Puzzle_Done");

        BroadcastMessage("DeactivateHighlight");
        buttonText.text = "Has acabat!";
        gameState = GameState.Complete;
        GameManager.Instance.SpawnPrizeHearts(correctGuessCount - 1);
        exitTp.triggerType = GameTrigger.EventType.Teleport;
        Instantiate(exitPortal, exitPortalTransform);

    }

    public void GuessSong(MovieButton.MovieOption givenMovieOption, MovieButton thisButton)
    {
        if (gameState == GameState.WaitingAnswer)
        {
            SFXManager.Instance.PlaySound("Butt_Ingame");

            thisButton.highlightButton.ActivateHighlight();
            gameState = GameState.Explaining;
            if (givenMovieOption == movieList[movieIndex].movieOption)
            {
                StartCoroutine(CorrectGuess(thisButton));
                movieList.RemoveAt(movieIndex);
            }
            else
            {
                StartCoroutine(WrongGuess(thisButton));
                movieList.RemoveAt(movieIndex);
            }
        }
    }

    IEnumerator CorrectGuess(MovieButton lastButton)
    {
        yield return new WaitForSeconds(2);

        correctGuessCount ++;
        SFXManager.Instance.PlaySound("OKAnswer");

        lastButton.highlightButton.DeactivateHighlight();
        lastButton.frameHighlight.DeactivateHighlight();
        lastButton.GetComponent<Collider>().enabled = false;
        audioSource.Stop();


        yield return new WaitForSeconds(2);

        if (movieList.Count > 0)
        {
            PickAndPlayMusic();
            gameState = GameState.WaitingAnswer;
        }
        else
        {
            CompleteGame();
        }
    }

    IEnumerator WrongGuess(MovieButton lastButton)
    {
        yield return new WaitForSeconds(2);
        SFXManager.Instance.PlaySound("WrongAnswer");

        lastButton.highlightButton.DeactivateHighlight();
        audioSource.Stop();
        //TO DO: Sound

        yield return new WaitForSeconds(2);

        if (movieList.Count > 0)
        {
            PickAndPlayMusic();
            gameState = GameState.WaitingAnswer;
        }
        else
        {
            CompleteGame();
        }
    }

    public void ControlButton()
    {
        if (gameState == GameState.Inactive)
        {
            StartGame();
            return;
        }

        if (gameState == GameState.Explaining || gameState == GameState.Complete) return;
   

        if (gameState == GameState.WaitingAnswer && !audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }

    public void PickAndPlayMusic()
    {
        movieIndex = Random.Range(0, movieList.Count);
        audioSource.clip = movieList[movieIndex].movieAudio;
        audioSource.Play();
    }
}
