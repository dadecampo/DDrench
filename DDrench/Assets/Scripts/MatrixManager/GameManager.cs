using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MatrixManager
{
    public class GameManager: MonoBehaviour 
    {
        private Matrix _matrix;
        private float _sizeBackground;
        private Vector2 _topLeftPosition;

        public Matrix Matrix
        {
            get
            {
                return _matrix;
            }
        }

        private void Awake()
        {
            _sizeBackground = this.gameObject.transform.localScale.x;
            _topLeftPosition = new Vector2(this.gameObject.transform.GetChild(0).position.x, this.gameObject.transform.GetChild(0).position.y);
            GenNewMatrix(6);
        }


        public void GenNewMatrix(int size)
        {
            _matrix = new Matrix(size);
            _matrix.DrawMatrix(_sizeBackground, _topLeftPosition);
        }


    }
}