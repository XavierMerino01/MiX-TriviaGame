using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCompletionPuzzle : MonoBehaviour
{
    LivingRoomPuzzle puzzleManager;

    public FurnitureObject.TypeOfObject targetObject;

    protected GameObject[] children;

    protected int childIndex;

    private void Start()
    {
        puzzleManager = GetComponentInParent<LivingRoomPuzzle>();

        children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FurnitureObject>())
        {
            FurnitureObject thisObj = other.GetComponent<FurnitureObject>();

            if (thisObj.typeOfFurniture == targetObject) 
            {
                SFXManager.Instance.PlaySound("DropItem");

                children[childIndex].SetActive(true);
                childIndex++;
                GameManager.Instance.RemovePlayerObject();
                Destroy(other.gameObject);

                CheckCompletion();
            }

        }
    }

    protected virtual void CheckCompletion()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (!children[i].activeSelf)
            {
                return;
            }
        }
        GameManager.Instance.LobbySpawnHeart();
        puzzleManager.SetPuzzleState(targetObject.ToString(), true);
    }
}
