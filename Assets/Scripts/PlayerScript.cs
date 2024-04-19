using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PlayerCharacters
{
    public enum Characters
    {
        Duck,
        Bear,
        Dog
    }
    public Characters thisCharacter;

    public GameObject[] characterParts;
}


public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public Rigidbody playerRb;
    [HideInInspector] public InputManager inputManager;
    [SerializeField] public bool inputEnabled = true;
    public Animator playerAnim;

    public float moveSpeed = 10;

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Dancing,
    }

    public PlayerState currentPlayerState;

    public PlayerCharacters[] playerCharacters;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        GameManager.Instance.GameStart(this.gameObject, FindObjectOfType<UIManager>());
        SetSelectedCharacterActive();
        currentPlayerState = PlayerState.Idle;
    }

    public void ChangePlayerState(PlayerState state)
    {
        if (currentPlayerState == state) return;

        PlayAnimation(state);
        currentPlayerState = state;
    }

    private void PlayAnimation(PlayerState newPlayerState)
    {
        switch (newPlayerState)
        {
            case PlayerState.Idle:
                playerAnim.SetBool("isMoving", false);
                break;
            case PlayerState.Walking:
                playerAnim.SetBool("isMoving", true);
                break;
            case PlayerState.Dancing:
                playerAnim.Play("Dance_Anim");
                break;
        }
    }

    private void SetSelectedCharacterActive()
    {
        foreach (var character in playerCharacters)
        {
            if (character.thisCharacter != GameManager.Instance.GetPickedCharacter())
            {
                foreach (var character2 in character.characterParts)
                {
                    character2.SetActive(false);
                }

            }
        }
    }
}
