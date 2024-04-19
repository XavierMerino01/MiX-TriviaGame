using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Image fadePanelImage;
    [SerializeField] private Animator prizePanelAnim;
    [SerializeField] private GameObject[] prizePanels;
    [SerializeField] private GameObject endGamePanel;

    [SerializeField] private List<GameObject> prizes = new List<GameObject>();
    private bool pendingSpawnPrize;
  
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Controls") return;

        StartFade(0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckAndShowPrize(17);
        }
    }

    public void StartFade(float targetAlpha, float duration)
    {
        StartCoroutine(Fade(targetAlpha, duration));
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        // Get the current alpha value
        float startAlpha = fadePanelImage.color.a;

        // Calculate the increment value per frame
        float increment = (targetAlpha - startAlpha) / duration;

        // Adjust the alpha value gradually
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            Color newColor = fadePanelImage.color;
            newColor.a = startAlpha + increment * time;
            fadePanelImage.color = newColor;
            yield return null;
        }

        // Ensure the target alpha is set correctly
        Color finalColor = fadePanelImage.color;
        finalColor.a = targetAlpha;
        fadePanelImage.color = finalColor;

        if(targetAlpha == 0)
        {
            GameManager.Instance.EnablePlayerInput();
        }
    }

    public void CheckAndShowPrize(int currentHearts)
    {
        switch (currentHearts)
        {
            case 1:
                prizePanelAnim.gameObject.SetActive(true);
                SFXManager.Instance.PlaySound("PrizeSuu");
                GameManager.Instance.DisablePlayerInput();
                break;
            case 5:
                ActivateCurrentPrizePanel(0);
                break;

            case 8:
                ActivateCurrentPrizePanel(1);
                pendingSpawnPrize = true;
                break;

            case 10:
                ActivateCurrentPrizePanel(2);
                break;

            case 16:
                ActivateCurrentPrizePanel(3);
                pendingSpawnPrize = true;
                break;

            case 17:
                ActivateCurrentPrizePanel(4);
                GameManager.Instance.EndGame();
                Invoke("EndGameFade", 8);
                break;
        }
        
    }

    private void ActivateCurrentPrizePanel(int lastPanel)
    {
        SFXManager.Instance.PlaySound("PrizeSuu");
        GameManager.Instance.DisablePlayerInput();
        prizePanels[lastPanel].SetActive(false);
        prizePanels[lastPanel + 1].SetActive(true);
        prizePanelAnim.gameObject.SetActive(true);
    }

    public void ButtonOkPrizePanel()
    {
        prizePanelAnim.Play("PrizePanel_Out");
        SFXManager.Instance.PlaySound("Butt_UI");

        StartCoroutine(WaitForPanelAnim());
        GameManager.Instance.EnablePlayerInput();

        if (pendingSpawnPrize)
        {
            Vector3 randomPosition = new Vector3(Random.Range(2f, 8f), 12f, Random.Range(-2f, 4f));
            Instantiate(prizes[0], randomPosition, Quaternion.identity);
            prizes.RemoveAt(0);
            pendingSpawnPrize = false;
        }
    }

    public void ButtonOkInfo()
    {
        StartFade(1, 1);
        Invoke("LoadGame", 1);
    }

    void LoadGame()
    {
        GameManager.Instance.LoadScene("GameScene");
    }


    IEnumerator WaitForPanelAnim()
    {
        yield return new WaitForSeconds(1.5f);
        prizePanelAnim.gameObject.SetActive(false);
    }
    
    public void EndGameFade()
    {
        StartFade(1, 5);
        Invoke("EndGamePanel", 5);
    }

    private void EndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
