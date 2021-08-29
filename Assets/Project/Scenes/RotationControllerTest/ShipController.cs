using Exa.Ships.Rotation;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] RotationController ship;
    [SerializeField] private InputAction action;
 
    void Awake() {
        action.started += ctx => {
            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = ship.transform.position.z;
            ship.SetTargetVector(mousePos);
        };
        action.Enable();
    }
}