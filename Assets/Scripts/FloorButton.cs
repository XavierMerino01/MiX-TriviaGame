using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    TrivialManager trivialManager;

    public enum ButtonOption
    {
        A,
        B,
        C,
        D
    }

    public ButtonOption buttonOption;

    [HideInInspector] public HighlightButton highlightButton;

    private void Start()
    {
        trivialManager = FindObjectOfType<TrivialManager>();
        highlightButton = GetComponent<HighlightButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SendAnswer();
    }

    private void SendAnswer()
    {
        trivialManager.AnswerQuestion(buttonOption, this.highlightButton);
    }
}
