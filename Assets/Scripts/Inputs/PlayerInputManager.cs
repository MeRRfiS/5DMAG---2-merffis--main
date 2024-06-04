using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public void Moving(InputAction.CallbackContext callback)
    {
        Vector2 inputMovement = callback.ReadValue<Vector2>();

        PlayerController.Instance.ChangeMovement(inputMovement);
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if (!callback.started) return;

        PlayerController.Instance.ToAttack();
    }
}
