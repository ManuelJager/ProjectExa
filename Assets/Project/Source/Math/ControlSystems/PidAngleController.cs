namespace Exa.Math.ControlSystems
{
    // NOTE: This is completely broken
    public class PidAngleController : PidController
    {
        public PidAngleController(float proportional, float integral, float derivitive)
            : base(proportional, integral, derivitive)
        {
        }

        public float ComputeRequiredAngularAcceleration(float currentAngle, float desiredAngle, float currentAngularVelocity, float deltaTime)
        {
            var diff = (float)AngleDifference(currentAngle, desiredAngle);
            var delta = currentAngularVelocity * diff;
            var output = ComputeOutput(diff, delta, deltaTime);
            return output;
        }

        public double AngleDifference(double angle1, double angle2)
        {
            double diff = (angle2 - angle1 + 180) % 360 - 180;
            return diff < -180 ? diff + 360 : diff;
        }
    }
}