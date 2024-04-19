using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public enum ButtonType
    {
        None,
        Teleport,
        Option
    }

    public ButtonType buttonType;

    [SerializeField] public GameObject targetDoor;
    [SerializeField] public int optionValue;
    [SerializeField] public GameObject lightIndicator;

    public void ActivateButton()
    {
        SFXManager.Instance.PlaySound("Butt_Ingame");
        if (buttonType == ButtonType.Teleport && !GameManager.Instance.HasCompletedLevel(optionValue))
        {
            targetDoor.GetComponent<Animator>().Play("WallMove_In");
            SFXManager.Instance.PlaySound("Door");
            lightIndicator.SetActive(false);
            GameManager.Instance.SetLevelComplete(optionValue);
        }
        else if (buttonType == ButtonType.Option)
        {
            SendMessageUpwards("ControlButton", SendMessageOptions.RequireReceiver);
        }
    }
}