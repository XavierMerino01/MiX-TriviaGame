using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieButton : MonoBehaviour
{
    MoviePostersManager moviePostersManager;

    public enum MovieOption
    {
        Oppenheimer,
        Barbie,
        Society,
        PoorThings,
        PearlHarbour,
        Wonka,
        HowToTrain
    }

    public MovieOption movieOption;

    [HideInInspector] public HighlightButton highlightButton;
    [SerializeField] public HighlightButton frameHighlight;

    private void Start()
    {
        moviePostersManager = FindObjectOfType<MoviePostersManager>();
        highlightButton = GetComponent<HighlightButton>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SendAnswer();
    }

    private void SendAnswer()
    {
        moviePostersManager.GuessSong(movieOption, this);
    }
}
