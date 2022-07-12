using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ColorsManager
{
    public class ColorsManager
    {
        private Color _selectedColor;
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
            _selectedColor = Color.red;
        }
    }
}
