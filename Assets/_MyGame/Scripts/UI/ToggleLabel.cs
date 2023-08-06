using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Curio.Gameplay
{
    public class ToggleLabel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void ToggleVisibility(bool isVisible)
        {
            if (isVisible)
            {
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.25f);
            }
            else
            {
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.25f);
            }
        }
    }
}