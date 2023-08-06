using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Curio.Gameplay
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private GameObject on, off;

        private bool onStatus = true;
        [SerializeField] private Button buttonComponent;

        private void Start()
        {
            buttonComponent.onClick.AddListener(OnValueChange);
        }

        private void OnValueChange()
        {
            onStatus = !onStatus;
            on.gameObject.SetActive(onStatus);
            off.gameObject.SetActive(!onStatus);
        }

    }
}