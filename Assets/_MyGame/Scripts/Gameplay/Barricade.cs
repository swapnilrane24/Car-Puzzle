using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ScriptableObjectArchitecture;

namespace Curio.Gameplay
{
    public class Barricade : MonoBehaviour, IObstacle
    {

        [SerializeField] private Transform movingPart;
        [SerializeField] private bool barricadeOpen;
        [SerializeField] private float barricadeCloseAngle, barricadeOpenAngle;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private GameEvent carReachedEvent;
        [SerializeField] private SpriteRenderer redSignal;

        private void OnDisable()
        {
            carReachedEvent.RemoveListener(UpdateBarricate);
        }

        private void Start()
        {
            if (!barricadeOpen)
            {
                movingPart.DOLocalRotate(Vector3.forward * barricadeCloseAngle, 0.25f).
                    OnComplete(() => {
                        redSignal.enabled = !barricadeOpen;
                        boxCollider.enabled = !barricadeOpen;
                    });
            }

            redSignal.enabled = !barricadeOpen;
            boxCollider.enabled = !barricadeOpen;

            carReachedEvent.AddListener(UpdateBarricate);
        }

        public void UpdateBarricate()
        {
            barricadeOpen = !barricadeOpen;

            if (barricadeOpen)
            {
                movingPart.DOLocalRotate(Vector3.forward * barricadeOpenAngle, 0.25f).
                    OnComplete(() =>
                    {
                        redSignal.enabled = !barricadeOpen;
                        boxCollider.enabled = !barricadeOpen;
                    });
            }
            else
            {
                movingPart.DOLocalRotate(Vector3.forward * barricadeCloseAngle, 0.25f).
                    OnComplete(() => {
                        redSignal.enabled = !barricadeOpen;
                        boxCollider.enabled = !barricadeOpen;
                    });
            }

        }
    }
}