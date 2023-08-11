using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class Tutorial : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                gameObject.SetActive(false);
        }


    }
}