using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Curio.Gameplay
{
    public class PushButton : MonoBehaviour
    {
        [SerializeField] UnityEvent onPushEvent;

        private bool eventTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CarController>())
            {
                if (eventTriggered == false)
                {
                    onPushEvent?.Invoke();
                    eventTriggered = true;
                }
            }
        }
    }
}