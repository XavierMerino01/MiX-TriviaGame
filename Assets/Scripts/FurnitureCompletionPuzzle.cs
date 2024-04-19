using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCompletionPuzzle : MonoBehaviour
{
    LivingRoomPuzzle puzzleManager;

    public enum TypeOfFurniture
    {
        None,
        Sofa,
        Picture,
        Table
    }

    public TypeOfFurniture typeOfFurniture;
    public FurnitureObject.TypeOfObject targetObject;

    public GameObject[] children;

    private void Start()
    {
        puzzleManager = GetComponentInParent<LivingRoomPuzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FurnitureObject>())
        {
            FurnitureObject thisObj = other.GetComponent<FurnitureObject>();

            if (thisObj.typeOfObject == targetObject) 
            {
                SFXManager.Instance.PlaySound("DropItem");

                children[other.GetComponent<FurnitureObject>().indexOfObject].SetActive(true);
                GameManager.Instance.RemovePlayerObject();
                Destroy(other.gameObject);

                CheckCompletion();
            }

        }
    }

    private void CheckCompletion()
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (!children[i].activeSelf)
            {
                return;
            }
        }
        GameManager.Instance.LobbySpawnHeart();
        puzzleManager.SetPuzzleState(typeOfFurniture.ToString(), true);
    }
}
