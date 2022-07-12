using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ColorsManager
{
    public class ColorsManager
    {
        private Color _selectedColor;
        private List<Color> _possibleColors;
        private static ColorsManager _instance;
        private static object _instanceLock = new object();

        public Color SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                _selectedColor = value;
            }
        }

        public List<Color> PossibleColors
        {
            get
            {
                return _possibleColors;
            }
        }

        public static ColorsManager Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ColorsManager();
                    }
                    return _instance;
                }
            }
            
        }

        private ColorsManager()
        {
            _possibleColors = new List<Color>();
            BuildPossibleColorsList();
            if (_possibleColors.Count > 0)
            {
                _selectedColor = _possibleColors[0];
            }
        }

        private void BuildPossibleColorsList()
        {
            _possibleColors.Add(GameObject.Find("Red").GetComponent<SpriteRenderer>().color);
            _possibleColors.Add(GameObject.Find("Pink").GetComponent<SpriteRenderer>().color);
            _possibleColors.Add(GameObject.Find("Green").GetComponent<SpriteRenderer>().color);
            _possibleColors.Add(GameObject.Find("Purple").GetComponent<SpriteRenderer>().color);
            _possibleColors.Add(GameObject.Find("Yellow").GetComponent<SpriteRenderer>().color);
            _possibleColors.Add(GameObject.Find("LightBlue").GetComponent<SpriteRenderer>().color);
        }
    }
}
