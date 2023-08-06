using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Curio.Gameplay
{
    public class TogglePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void ToggleVisibilityInstant(bool isVisible)
        {
            if (isVisible)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void ToggleVisibility(bool isVisible)
        {
            if (isVisible)
            {
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.25f).
                    OnStart(() => {
                        canvasGroup.interactable = true;
                        canvasGroup.blocksRaycasts = true;
                    });
            }
            else
            {
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, 0.25f).
                    OnStart(() => {
                        canvasGroup.interactable = false;
                        canvasGroup.blocksRaycasts = false;
                    });
            }
        }
    }
}