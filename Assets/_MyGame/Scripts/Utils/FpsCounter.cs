using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class FpsCounter : MonoBehaviour
    {
        private float count;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            GUI.depth = 2;
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnGUI()
        {
            //GUI.Label(new Rect(5, 40, 100, 25), "FPS: " + Mathf.Round(count));

            Rect location = new Rect(20, Screen.height - 60, 85, 25);
            string text = $"FPS: {Mathf.Round(count)}";
            Texture black = Texture2D.linearGrayTexture;
            GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
            GUI.color = Color.black;
            GUI.skin.label.fontSize = 18;
            GUI.Label(location, text);
        }
    }
}
