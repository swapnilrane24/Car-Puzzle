using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    [CreateAssetMenu(fileName = "VehicleList", menuName = "Curio/VehicleList")]
    public class VehicleVisualList : ScriptableObject
    {
        [SerializeField] private Sprite[] vehicleList;

        public Sprite[] VehicleList => vehicleList;
    }
}