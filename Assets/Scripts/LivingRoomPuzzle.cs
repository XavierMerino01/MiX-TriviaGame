using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomPuzzle : MonoBehaviour
{
    public Dictionary<string, bool> puzzleStates = new Dictionary<string, bool>()
    {
        { "Sofa", false },
        { "Picture", false },
        { "Table", false }
    };

    [SerializeField] private GameObject lightIndicator;

    // Example method to set the state of a puzzle
    public void SetPuzzleState(string puzzleName, bool isComplete)
    {
        puzzleStates[puzzleName] = isComplete;

        if (CheckPuzzleCompletion())
        {
            lightIndicator.SetActive(false);
            GameManager.Instance.SetLevelComplete(2);
        }
    }

    public bool CheckPuzzleCompletion()
    {
        // Iterate through the dictionary values
        foreach (bool puzzleState in puzzleStates.Values)
        {
            // If any value is false, return false
            if (!puzzleState)
            {
                return false;
            }
        }
        // If all values are true, return true
        SFXManager.Instance.PlaySound("Puzzle_Done");
        return true;
    }
}
