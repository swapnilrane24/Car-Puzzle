using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Curio.Gameplay
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private Collider objCollider;

        public void DeactivateObstacle()
        {
            transform.DOLocalMoveY(-1.5f, 0.5f).OnComplete(() => objCollider.enabled = false);


        }
    }
}