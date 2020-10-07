﻿using Exa.Math;
using Exa.Ships.Targetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exa.Data;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class SimpleNavigation : INavigation
    {
        private readonly ThrustVectors _thrustVectors;
        private readonly Ship _ship;
        private readonly NavigationOptions _options;

        public ITarget LookAt { private get; set; }
        public ITarget MoveTo { private get; set; }
        public IThrustVectors ThrustVectors => _thrustVectors;

        public SimpleNavigation(Ship ship, NavigationOptions options, Scalar thrustModifier)
        {
            this._ship = ship;
            this._options = options;
            this._thrustVectors = new ThrustVectors(thrustModifier);
        }

        public void Update(float deltaTime)
        {
            if (LookAt != null)
            {
                UpdateRotation();
            }

            if (MoveTo != null)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            var currentPosition = (Vector2)_ship.transform.position;
            var moveToTargetPosition = MoveTo.GetPosition(currentPosition);

            if (currentPosition == moveToTargetPosition)
            {
                ThrustVectors.SetGraphics(Vector2.zero);
                return;
            }

            var headingAngle = _ship.transform.rotation.eulerAngles.z;
            var direction = (moveToTargetPosition - currentPosition).Rotate(-headingAngle).normalized;
            ThrustVectors.SetGraphics(direction);

            var deltaTime = Time.fixedDeltaTime;
            var newPosition = Vector2.MoveTowards(currentPosition, moveToTargetPosition, 30 * deltaTime);

            _ship.transform.position = newPosition;
        }

        private void UpdateRotation()
        {
            var currentPosition = (Vector2)_ship.transform.position;
            var lookAtTargetPosition = LookAt.GetPosition(currentPosition);

            var lookAtDelta = lookAtTargetPosition - currentPosition;
            var currentRotation = _ship.transform.rotation.eulerAngles;

            var targetRotation = new Vector3(0, 0, lookAtDelta.GetAngle());
            var deltaTime = Time.fixedDeltaTime;
            var newRotation = Vector3.RotateTowards(currentRotation, targetRotation, float.MaxValue, 100 * deltaTime);

            _ship.transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
