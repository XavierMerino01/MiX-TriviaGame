using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrigger : MonoBehaviour
{
    public enum EventType
    {
        None,
        Teleport,
        Option,
        CameraEvent
    }

    public EventType triggerType;


    [SerializeField] public Transform teleportDestination;
    [SerializeField] public Transform cameraDestination;
    [SerializeField] private float targetFOV;
    [SerializeField] private float duration;

    [SerializeField] private bool hasEnterTransition, hasExitTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasEnterTransition) return;
        if (!other.gameObject.CompareTag("Player")) return;

        switch (triggerType)
        {
            case EventType.Teleport:
                SFXManager.Instance.PlaySound("Teleport");
                GameManager.Instance.TeleportPlayer(teleportDestination.position, cameraDestination, 1);
                break;
            case EventType.CameraEvent:
                Camera.main.GetComponent<CameraBehaviour>().MoveCamera(teleportDestination.position, targetFOV, duration);
                break;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!hasExitTransition) return;
        if (!other.gameObject.CompareTag("Player")) return;

        switch (triggerType)
        {
            case EventType.CameraEvent:
                Camera.main.GetComponent<CameraBehaviour>().DefaultCameraPosition(duration);
                break;
        }
    }
}
