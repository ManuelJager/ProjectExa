using Exa.Math;
using UnityEngine;

public interface IRotationController {
    public void AddTorque(float torque);
    public float AngularVelocity { get; set; }
    public float InertiaTensor { get; }
    public Vector3 Velocity { get; }
}

public class RigidbodyController : IRotationController {
    private readonly Rigidbody rb;

    public RigidbodyController(Rigidbody rb) {
        this.rb = rb;
    }

    public void AddTorque(float torque) {
        rb.AddTorque(0, 0, torque);
    }

    public float AngularVelocity {
        get => rb.angularVelocity.z; 
        set => rb.angularVelocity = rb.angularVelocity.SetZ(value);
    }

    public float InertiaTensor {
        get => rb.inertiaTensor.z;
    }

    public Vector3 Velocity {
        get => rb.velocity;
    }
}
 
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Spinning : MonoBehaviour
{
    float heading, bearing;
    float angularAcceleration, turnSpeed;
 
    public float MaxTorque = 7.5f;
    public float StopThresholdMultiplier = 0.05f;
 
    Vector3 tVec;
    private IRotationController controller;
 
    void Awake()
    {
        controller = new RigidbodyController(GetComponent<Rigidbody>());
    }
 
    void Start()
    {
        Debug.Log("Inertia: " + controller.InertiaTensor.ToString("F2") + " kg*m²");
        Debug.Log("Max. Torque: " + MaxTorque + " N*m");
        angularAcceleration = MaxTorque / controller.InertiaTensor * Mathf.Rad2Deg;
        Debug.Log("Max. Angular Acceleration: " + angularAcceleration.ToString("F2") + "°/s².");
 
        tVec = Vector3.zero;
    }
 
    void FixedUpdate()
    {
        turnSpeed = controller.AngularVelocity * Mathf.Rad2Deg;
 
        if (tVec != Vector3.zero) {
            tVec += controller.Velocity * Time.fixedDeltaTime;
            Debug.DrawLine(transform.position, tVec);
            var transform1 = transform;
            var dRot = Quaternion.FromToRotation(transform1.up * -1f, tVec - transform1.position);
            var difference = 360f - dRot.eulerAngles.z - 180f;
 
            var stopDistance = angularAcceleration * Mathf.Pow(turnSpeed / angularAcceleration, 2f) / 2f;
            var torqueDirection = -1f * Mathf.Sign(difference);
            if (stopDistance >= Mathf.Abs(difference)) {
                torqueDirection *= -1f;
            }
            if (Mathf.Abs(difference) > MaxTorque * StopThresholdMultiplier) {
                controller.AddTorque(torqueDirection * MaxTorque);
            } else {
                controller.AngularVelocity = 0;
                tVec = Vector3.zero;
            }
        }
    }
 
    public void SetTargetVector(Vector3 newTarget)
    {
        tVec = newTarget;
    }
}