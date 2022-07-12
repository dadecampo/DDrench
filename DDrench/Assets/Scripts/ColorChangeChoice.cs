using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ColorsManager {
    public class ColorChangeChoice : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _color;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _color = _spriteRenderer.color;
        }

        void OnMouseDown()
        {
            ColorsManager.Instance.SelectedColor = _color;
        }
    }
}
