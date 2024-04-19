using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool[] LevelCompletion;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 10, -8);
    [SerializeField] private GameObject playerRef;
    [SerializeField] public UIManager myUIManager;

    [SerializeField] public PlayerCharacters.Characters pickedCharacter;
    [SerializeField] private GameObject heartPrefab;
    private int heartsToSpawn;


    public static GameManager Instance; // A static reference to the GameManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            LobbySpawnHeart();
        }
    }

    public void GameStart(GameObject player, UIManager uiManager)
    {
        playerRef = player;
        myUIManager = uiManager;
        myUIManager.StartFade(0, 2);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RemovePlayerObject()
    {
        playerRef.GetComponent<PlayerInteraction>().ReleaseObject();
    }

    public void TeleportPlayer(Vector3 targetLocation, Transform cameraTarget, float transitionDuration)
    {
        //TO DO: Fade in and out
        playerRef.GetComponent<PlayerScript>().inputEnabled = false;
        StartCoroutine(TeleportTransition(targetLocation, cameraTarget, transitionDuration));
        myUIManager.StartFade(1, transitionDuration);

    }

    private IEnumerator TeleportTransition(Vector3 targetLocation, Transform cameraTarget, float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);

        playerRef.transform.position = targetLocation;
        Camera.main.transform.position = cameraTarget.position;
        Camera.main.transform.rotation = cameraTarget.rotation;
        Camera.main.fieldOfView = 60;
        myUIManager.StartFade(0, delayDuration);

        if (heartsToSpawn > 0)
        {
            StartCoroutine(ProgressivePrizeSpawn(heartsToSpawn));
            heartsToSpawn = 0;
        }

    }

    public void SetLevelComplete(int level)
    {
        LevelCompletion[level] = true;
    }


    public bool HasCompletedLevel(int level)
    {
        return LevelCompletion[level];
    }

    public void SpawnPrizeHearts(int amount)
    {
        heartsToSpawn = amount;
    }

    public void LobbySpawnHeart()
    {
        StartCoroutine(ProgressivePrizeSpawn(1));
    }

    private IEnumerator ProgressivePrizeSpawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(2f, 8f), 12f, Random.Range(-2f, 4f)); // Adjust the range as per your requirement
            Instantiate(heartPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1f); // Wait for 1 second before spawning the next heart
        }
    }

    public void DisablePlayerInput()
    {
        if (playerRef == null) return;

        playerRef.GetComponent<PlayerScript>().inputEnabled = false;
    }

    public void EnablePlayerInput()
    {
        if (playerRef == null || playerRef.GetComponent<PlayerScript>().currentPlayerState == PlayerScript.PlayerState.Dancing) return;

        playerRef.GetComponent<PlayerScript>().inputEnabled = true;
    }

    public PlayerCharacters.Characters GetPickedCharacter()
    {
        return pickedCharacter;
    }

    public void SetPickedCharacter (PlayerCharacters.Characters newChar)
    {
        pickedCharacter = newChar;
    }

    public void EndGame()
    {
        DisablePlayerInput();
        playerRef.GetComponent<PlayerScript>().ChangePlayerState(PlayerScript.PlayerState.Dancing);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
