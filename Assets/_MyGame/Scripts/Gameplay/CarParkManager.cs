using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class CarParkManager : MonoBehaviour
    {
        private static CarParkManager instance;
        public static CarParkManager Instance => instance;

        private List<CarParkSection> carParkSections = new List<CarParkSection>();
        private int totalCarsParked = 0;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddCarParkSectionToList(CarParkSection carParkSection)
        {
            carParkSections.Add(carParkSection);
        }

        public void CarReachedEnd()
        {
            totalCarsParked++;
            if (totalCarsParked >= carParkSections.Count)
            {
                GamePanel.Instance.LevelCompleted();
            }
        }

        public void CarBeginReached()
        {
            totalCarsParked--;
            if (totalCarsParked < 0) totalCarsParked = 0;
        }

    }
}