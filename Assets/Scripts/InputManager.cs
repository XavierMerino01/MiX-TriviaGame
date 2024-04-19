using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerControlls playerControlls;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        playerControlls = new PlayerControlls();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
        playerControlls.Player.Move.performed += ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        playerControlls.Player.Move.canceled += ctx => OnMoveCanceled();

        playerControlls.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        playerControlls.Disable();
        playerControlls.Player.Move.performed -= ctx => OnMovePerformed(ctx.ReadValue<Vector2>());
        playerControlls.Player.Move.canceled -= ctx => OnMoveCanceled();

        playerControlls.Player.Interact.Disable();

    }

    private void OnMovePerformed(Vector2 movement)
    {
        playerMovement.SetMovementInput(movement);
    }

    private void OnMoveCanceled()
    {
        playerMovement.SetMovementInput(Vector2.zero);
    }


    public bool InteractButtonDown()
    {
        return playerControlls.Player.Interact.WasPressedThisFrame();
    }
    public bool InteractButtonUp()
    {
        return playerControlls.Player.Interact.WasReleasedThisFrame();
    }
}
