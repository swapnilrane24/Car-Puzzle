using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using ScriptableObjectArchitecture;
using System.Linq;
using UnityEngine.EventSystems;

namespace Curio.Gameplay
{
    public class TapInput : MonoBehaviour
    {
        private PlayerInput playerInput;

        private Vector3 curScreenPos;
        private bool inputAssigned;

        private void OnDisable()
        {
            playerInput.Player.Press.performed -= Press_performed;
            playerInput.Player.Press.canceled -= Press_canceled;
        }

        public void AssignPlayerInput(PlayerInput inputAction)
        {
            playerInput = inputAction;

            playerInput.Player.Press.performed += Press_performed;
            playerInput.Player.Press.canceled += Press_canceled;

            inputAssigned = true;
        }

        private void Press_canceled(InputAction.CallbackContext context)
        {
            
        }

        private void Press_performed(InputAction.CallbackContext context)
        {
            DetectObject();
        }

        private void DetectObject()
        {
            curScreenPos = playerInput.Player.ScreenPos.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(curScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && !IsPointerOverUI())
            {
                if (hit.transform.TryGetComponent<ITappable>(out ITappable assignableStand))
                {
                    if (GameManager.Instance.GameState == GameState.PLAYING)
                        assignableStand.OnTap();
                }
            }
        }

        private bool IsPointerOverUI()
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                //position = Mouse.current.position.ReadValue()
                position = playerInput.Player.ScreenPos.ReadValue<Vector2>()
            };

            var raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            return raycastResultsList.Any(result => result.gameObject is GameObject);
        }

    }
}