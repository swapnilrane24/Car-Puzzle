using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class LevelData : MonoBehaviour
    {
        [SerializeField] private float cameraFOV = 60;
        [SerializeField] private float orthoSize = 4f;
        [SerializeField] private Vector3 cameraTransform;
        [SerializeField] private int moveCount = 10;
        [SerializeField] private int rewardAmount;

        public int RewardAmount => rewardAmount;
        public bool UnlimitedMoveCount => moveCount <= 0;

        private void Start()
        {
            //Camera.main.fieldOfView = cameraFOV;
            Camera.main.orthographicSize = orthoSize;
            Camera.main.transform.position = cameraTransform;
            GamePanel.Instance.SetMoveValue(moveCount);
        }
    }
}