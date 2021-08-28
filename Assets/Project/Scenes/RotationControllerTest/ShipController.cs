using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] Spinning ship;
    [SerializeField] private bool buttonDown;
    [SerializeField] private InputAction action;
 
    void Awake() {
        action.started += ctx => {
            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = ship.transform.position.z;
            ship.SetTargetVector(mousePos);
            buttonDown = true;
        };
        action.Enable();
    }
}