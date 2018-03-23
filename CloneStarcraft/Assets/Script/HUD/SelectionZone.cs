using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.HUD
{
    public class SelectionZone : MonoBehaviour
    {
        public Rect Zone { get; set; }
        private readonly Color selectionColor = new Color(1f, 0.88f, 0.54f, 0.5f);
        private Color opaqueColor;
        private Texture2D texture;

        public void Start()
        {
            texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, selectionColor);
            texture.Apply();
            opaqueColor = new Color(selectionColor.r, selectionColor.g, selectionColor.b);
        }

        public void Update()
        {
            Vector2 size = new Vector2(Input.mousePosition.x - Zone.position.x,
                    (Screen.height - Input.mousePosition.y) - Zone.position.y);
            Zone = new Rect(Zone.position, size);
        }

        public void OnGUI()
        {
            GUI.color = opaqueColor;
            GUI.DrawTexture((Rect)Zone, texture);
        }
    }
}
