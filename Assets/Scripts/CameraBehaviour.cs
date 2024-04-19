using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Coroutine moveCoroutine; // Reference to the running coroutine

    private Vector3 targetPosition;
    [SerializeField] private Vector3 startPosition;
    private float duration = 2.0f;
    private float initialFOV = 60f;
    private float currentFOV;
    private float targetFOV = 60f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        mainCamera.transform.position = startPosition;
        mainCamera.fieldOfView = initialFOV;
    }

    public void MoveCamera(Vector3 targetPosition, float targetFOV, float duration)
    {
        this.targetPosition = targetPosition;
        this.targetFOV = targetFOV;
        this.duration = duration;

        // If a coroutine is already running, stop it before starting a new one
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveCameraCoroutine());
    }

    public void DefaultCameraPosition(float duration)
    {
        MoveCamera(startPosition, initialFOV, duration);
    }

    public void SnapToDefault()
    {
        transform.position = startPosition;
        mainCamera.fieldOfView = initialFOV;
    }

    private IEnumerator MoveCameraCoroutine()
    {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;
        currentFOV = mainCamera.fieldOfView;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        mainCamera.fieldOfView = targetFOV;

        // Reset the reference to the coroutine
        moveCoroutine = null;
    }
}
