using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public partial class CentreOfMassCache : Dictionary<Vector2, float>
    {
        private Vector2 _vectoredTotal = new Vector2();
        private float _totalMass = 0f;

        public new void Add(Vector2 position, float mass)
        {
            base.Add(position, mass);

            _totalMass += mass;
            _vectoredTotal += position * mass;
        }

        public new void Remove(Vector2 position)
        {
            var mass = base[position];
            base.Remove(position);

            _totalMass -= mass;
            _vectoredTotal -= position * mass;
        }

        public Vector2 GetCentreOfMass()
        {
            return _vectoredTotal / _totalMass;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Centre of mass: {GetCentreOfMass()}");
            return sb.ToString();
        }
    }
}