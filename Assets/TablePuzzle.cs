using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePuzzle : FurnitureCompletionPuzzle
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FurnitureObject>())
        {
            FurnitureObject thisObj = other.GetComponent<FurnitureObject>();

            if (thisObj.typeOfFurniture == targetObject)
            {
                SFXManager.Instance.PlaySound("DropItem");

                GameObject childToActivate = transform.Find(thisObj.name).gameObject;
                if (childToActivate != null)
                {
                    childToActivate.SetActive(true);
                }

                GameManager.Instance.RemovePlayerObject();
                Destroy(other.gameObject);

                CheckCompletion();
            }

        }
    }
}
