using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class PlayerInputInitialized : MonoBehaviour
    {
        [SerializeField] private TapInput tapInput;

        private PlayerInput playerInput;

        public PlayerInput PlayerInput { get => playerInput; }

        private void Awake()
        {
            playerInput = new PlayerInput();

            playerInput.Player.Press.Enable();
            playerInput.Player.ScreenPos.Enable();

            tapInput.AssignPlayerInput(playerInput);
            
        }

        private void Start()
        {
            //GamePanel.Instance.AssignPlayerInput(playerInput);
        }
    }
}