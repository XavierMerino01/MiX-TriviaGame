using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainMenuManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject charMenu;
    [SerializeField] private GameObject videoScreen;
    [SerializeField] private UIManager uiManager;

    [SerializeField] public GameObject[] characterImage;

    void Start()
    {
        //videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.loopPointReached += OnVideoEnd;
        //Invoke("DelayedFade", 2);
        DelayedFade();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoScreen.SetActive(false);
        mainMenu.SetActive(true);
        videoPlayer.enabled = false;
    }

    public void OnPlayButton()
    {
        audioManager.FadeOutCurrentTrack(2);
        uiManager.StartFade(1, 2);
        Invoke("LoadGame", 2);
        SFXManager.Instance.PlaySound("Butt_UI");
    }

    public void OnCharButton()
    {
        SFXManager.Instance.PlaySound("Butt_UI");
        mainMenu.GetComponent<Animator>().Play("MainMenu_Out");
        StartCoroutine(WaitAndDeactivate(mainMenu));
        charMenu.SetActive(true);

    }

    public void OnCharBackButton()
    {
        SFXManager.Instance.PlaySound("Butt_UI");

        charMenu.GetComponent<Animator>().Play("MainMenu_Out");
        StartCoroutine(WaitAndDeactivate(charMenu));

        mainMenu.SetActive(true);
    }

    public void OnRightArrowClick()
    {
        SFXManager.Instance.PlaySound("Butt_UI");

        switch (GameManager.Instance.GetPickedCharacter())
        {
            case PlayerCharacters.Characters.Duck:
                ChangePickedCharacter(0, 1, PlayerCharacters.Characters.Bear);
                break;

            case PlayerCharacters.Characters.Bear:
                ChangePickedCharacter(1, 2, PlayerCharacters.Characters.Dog);
                break;

            case PlayerCharacters.Characters.Dog:
                ChangePickedCharacter(2, 0, PlayerCharacters.Characters.Duck);
                break;
        }
    }

    public void OnLeftArrowClick()
    {
        SFXManager.Instance.PlaySound("Butt_UI");

        switch (GameManager.Instance.GetPickedCharacter())
        {
            case PlayerCharacters.Characters.Duck:
                ChangePickedCharacter(0, 2, PlayerCharacters.Characters.Dog);
                break;

            case PlayerCharacters.Characters.Bear:
                ChangePickedCharacter(1, 0, PlayerCharacters.Characters.Duck);
                break;

            case PlayerCharacters.Characters.Dog:
                ChangePickedCharacter(2, 1, PlayerCharacters.Characters.Bear);
                break;
        }
    }

    void ChangePickedCharacter(int currentChar, int newChar, PlayerCharacters.Characters pickedChar)
    {
        characterImage[currentChar].GetComponent<Animator>().Play("CharAnim_Out");
        StartCoroutine(WaitAndDeactivate(characterImage[currentChar]));
        characterImage[newChar].SetActive(true);
        GameManager.Instance.SetPickedCharacter(pickedChar);
    }

    void LoadGame()
    {
        GameManager.Instance.LoadScene("Controls");
    }

    IEnumerator WaitAndDeactivate(GameObject obj)
    {
        yield return new WaitForSeconds(1);

        obj.SetActive(false);
    }

    public void CloseGameButton()
    {
        SFXManager.Instance.PlaySound("Butt_UI");

        Application.Quit();
    }

    private void DelayedFade()
    {
        uiManager.StartFade(0, 2);
    }
}
