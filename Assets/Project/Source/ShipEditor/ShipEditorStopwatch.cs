using UnityEngine;
using UnityEngine.Events;

namespace Exa.ShipEditor
{
    public class ShipEditorStopwatch : MonoBehaviour
    {
        public UnityEvent onTime;

        [SerializeField] private float _invokeOnTime;
        private float _timeElapsedFromLastEdit = 0f;
        private bool _invokedOnTime = false;

        public void OnEnable()
        {
            _timeElapsedFromLastEdit = 0f;
            _invokedOnTime = false;
        }

        public void Reset()
        {
            _timeElapsedFromLastEdit = 0f;
            _invokedOnTime = false;
        }

        public void EmulateInvoke()
        {
            _invokedOnTime = true;
            onTime?.Invoke();
        }

        public void Update()
        {
            if (_timeElapsedFromLastEdit > _invokeOnTime && !_invokedOnTime)
            {
                _invokedOnTime = true;
                onTime?.Invoke();
            }

            _timeElapsedFromLastEdit += Time.deltaTime;
        }
    }
}