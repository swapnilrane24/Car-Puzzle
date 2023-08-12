using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.Events;
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;

namespace Curio.Gameplay
{
    public class CarController : MonoBehaviour, ITappable , IObstacle
    {
        private enum CarState
        {
            Idle, Moving, Parked
        }

        [SerializeField] private Transform _parkingSpot;
        [SerializeField] private SplineFollower _follower;
        [SerializeField] private float moveSpeed;
        [SerializeField] private PathRender pathRender;
        [SerializeField] private GameEvent carReachedEvent;
        [SerializeField] private SoundType soundType, carHitSound;
        [Space(10)]
        [SerializeField] private SpriteRenderer visualSprite;
        [SerializeField] private VehicleVisualData vehicleVisualData;
        [OnValueChanged("UpdateVisual")]
        [SerializeField] private int visualIndex;

        public UnityEvent onEndReachedEvent;
        public UnityEvent onBeginReachedEvent;

        private bool _hit;

        private CarState carState = CarState.Idle;

        private bool moveForward = false; //set it false since on 1st tap it will be set to true

        private void OnDisable()
        {
            _follower.onEndReached -= EndReached;
            _follower.onBeginningReached -= BeginReached;
        }

        // Start is called before the first frame update
        void Start()
        {
            _follower.onEndReached += EndReached;
            _follower.onBeginningReached += BeginReached;

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (visualSprite != null && vehicleVisualData != null)
                visualSprite.sprite = vehicleVisualData.GetSprite(visualIndex, 0);
        }

        private void Update()
        {
            if (carState == CarState.Moving)
            {
                pathRender.UpdatePathRender(_follower.GetPercent());
            }
        }

        public void OnTap()
        {
            if (carState != CarState.Moving)
            {
                SoundManager.Instance.Play(soundType);
                GamePanel.Instance.ReduceMoveCount(1);
                moveForward = !moveForward;
                _follower.follow = true;
                if (moveForward)
                {
                    _follower.applyDirectionRotation = true;
                    _follower.followSpeed = moveSpeed;
                }
                else
                {
                    _follower.applyDirectionRotation = false;
                    _follower.followSpeed = -moveSpeed;
                }

                _follower.follow = true;
                carState = CarState.Moving;
            }
        }

        private void EndReached(double value)
        {
            onEndReachedEvent?.Invoke();
            carState = CarState.Parked;
            _follower.follow = false;
            carReachedEvent.Raise();
        }

        private void BeginReached(double value)
        {
            onBeginReachedEvent?.Invoke();
            carState = CarState.Idle;
            _follower.follow = false;
            carReachedEvent.Raise();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IObstacle>(out IObstacle obstacle))
            {
                SoundManager.Instance.Play(carHitSound);
                _follower.follow = false;
                GamePanel.Instance.LevelFailed();
            }
        }

    }
}