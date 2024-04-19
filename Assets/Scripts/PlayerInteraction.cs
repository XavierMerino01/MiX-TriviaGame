using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerScript myPlayerScript;
    
    [SerializeField] private GameObject pickUp_Ref;
    [SerializeField] private InteractableDetection interactDetect;

    [HideInInspector] public bool hasObject;

    private void Awake()
    {
        myPlayerScript = GetComponent<PlayerScript>();  
    }

    private void Update()
    {
        CheckPlayerInputs();
    }

    private void CheckPlayerInputs()
    {
        if (myPlayerScript.inputManager.InteractButtonDown() && myPlayerScript.inputEnabled)
        {
            CheckPossibleInteraction();
        }
    }

    private void CheckPossibleInteraction()
    {
        if (hasObject)
        {
            ReleaseObject();
            return;
        }

        if (interactDetect.objectInRange == null) return;

        Interactable currentObject = interactDetect.objectInRange.GetComponent<Interactable>();

        switch (currentObject.interactableType)
        {
            case (Interactable.Type.PickUp):
                PickUpObject(currentObject.gameObject);
                break;

            case (Interactable.Type.Button):
                ClickButton(currentObject.gameObject);
                break;
        }
    }

    private void PickUpObject(GameObject obj)
    {
        myPlayerScript.playerAnim.Play("PickUp_Anim");
        myPlayerScript.inputEnabled = false;
        SFXManager.Instance.PlaySound("PickUp");

        StartCoroutine(WaitForAnim(null, 0.3f));


        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        obj.transform.SetParent(pickUp_Ref.transform);
        obj.transform.position = pickUp_Ref.transform.position;
        obj.transform.rotation = pickUp_Ref.transform.rotation;

        hasObject = true;
    }

    public void ReleaseObject()
    {
        GameObject pickedUpObject = pickUp_Ref.transform.GetChild(0).gameObject;
        SFXManager.Instance.PlaySound("DropItem");

        Rigidbody rb = pickedUpObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        pickedUpObject.transform.SetParent(null);
        hasObject = false;
    }

    private void ClickButton(GameObject obj)
    {
        GameButton gameButton = obj.GetComponent<GameButton>();

        myPlayerScript.playerAnim.Play("Interact_Anim");
        myPlayerScript.inputEnabled = false;

        StartCoroutine(WaitForAnim(() => gameButton.ActivateButton(), 1));
    }


    private IEnumerator WaitForAnim(Action methodToCall, float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);

        myPlayerScript.inputEnabled = true;

        if (methodToCall != null)
        {
            methodToCall.Invoke();
        }
    }
}
