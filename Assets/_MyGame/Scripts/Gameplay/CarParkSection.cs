using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class CarParkSection : MonoBehaviour
    {
        [SerializeField] private CarController carController;

        private void OnDisable()
        {
            carController.onEndReachedEvent.RemoveListener(CarEndReached);
            carController.onBeginReachedEvent.RemoveListener(CarBeginReached);
        }

        private void Start()
        {
            carController.onEndReachedEvent.AddListener(CarEndReached);
            carController.onBeginReachedEvent.AddListener(CarBeginReached);
            CarParkManager.Instance.AddCarParkSectionToList(this);
        }

        private void CarEndReached()
        {
            CarParkManager.Instance.CarReachedEnd();
        }

        private void CarBeginReached()
        {
            CarParkManager.Instance.CarBeginReached();
        }
    }
}