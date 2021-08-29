using UnityEngine;

public class Rigidbody2DProxy : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;

    public void AddTorque(float torque) {
        rb.AddTorque(torque);
    }

    public float AngularVelocity {
        get => rb.angularVelocity;
        set => rb.angularVelocity = value;
    }

    public float InertiaTensor {
        get => rb.inertia;
    }

    public Vector2 Velocity {
        get => rb.velocity;
    }
}
 
