using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    [CreateAssetMenu(fileName = "VehicleData", menuName = "Curio/VehicleData")]
    public class VehicleVisualData : ScriptableObject
    {
        [SerializeField] private VehicleVisualList[] vehicleDataLists;

        public VehicleVisualList[] VehicleDataLists => vehicleDataLists;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visualIndex">Index of element in array from specific category</param>
        /// <param name="category">We have different vehicle categories</param>
        /// <returns></returns>
        public Sprite GetSprite(int visualIndex, int category)
        {
            Sprite sprite = null;

            sprite = vehicleDataLists[category].VehicleList[visualIndex];

            if (sprite == null)
            {
               sprite = vehicleDataLists[0].VehicleList[0];
            }

            return sprite;
        }
    }
}