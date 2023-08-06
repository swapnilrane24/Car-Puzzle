using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

namespace Curio.Gameplay
{
    public class PathRender : MonoBehaviour
    {
        [SerializeField] private SplineRenderer splineRendererLight;

        public void UpdatePathRender(double value)
        {
            splineRendererLight.clipFrom = value;
        }
    }
}