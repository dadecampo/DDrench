using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MatrixManager
{
    public class Matrix
    {

        private int _sizeN;
        private Color[,] _colorMatrix;
        private GameObject[,] _matrixCells;

        public int SizeN
        {
            get
            {
                return _sizeN;
            }
            set
            {
                _sizeN = value;
            }
        }

        /// <summary>
        /// Costruttore di una matrice quadrata N x N
        /// </summary>
        /// <param name="n"></param>
        public Matrix(int N)
        {
            _sizeN = N;
            BuildMatrix(_sizeN);
        }


        /// <summary>
        /// Costruisce una matrice di taglia size x size e la riempie di colori casuali
        /// tra quelli disponibili dai pulsanti.
        /// </summary>
        /// <param name="size"></param>
        public void BuildMatrix(int size)
        {
            _colorMatrix = new Color[size, size];
            _matrixCells = new GameObject[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {

                    _colorMatrix[i, j] = RandomizeColor();
                    GameObject newCell = new GameObject();
                    newCell.AddComponent<SpriteRenderer>();
                    _matrixCells[i, j] = newCell;
                }
            }
            return;
        }

        /// <summary>
        /// Restituisce un colore randomico tra quelli disponibili dai pulsanti.
        /// </summary>
        /// <returns></returns>
        private Color RandomizeColor()
        {
            System.Random r = new System.Random();
            int selected = r.Next(6);
            if (ColorsManager.ColorsManager.Instance.PossibleColors.Count == 6)
            {
                return ColorsManager.ColorsManager.Instance.PossibleColors[selected];
            }
            else
            {
                return default;
            }

        }

        /// <summary>
        /// Disegna la matrice a video
        /// </summary>
        public void DrawMatrix(float sizeBackground, Vector2 topLeftPivot)
        {
            float sizeMatrixCell = sizeBackground / _sizeN;
            for (int i = 0; i < _sizeN; i++)
            {
                for (int j = 0; j < _sizeN; j++)
                {
                    _matrixCells[i, j].transform.localScale = new Vector3(sizeMatrixCell, sizeMatrixCell, 1);
                    _matrixCells[i, j].transform.position= new Vector3(topLeftPivot.x + (j + 0.5f) * sizeMatrixCell, topLeftPivot.y - (i + 0.5f) * sizeMatrixCell, -2);
                    _matrixCells[i, j].GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(1,1), new Rect(0.0f, 0.0f, 1, 1), new Vector2(0.5f, 0.5f), 100.0f);
                    _matrixCells[i, j].GetComponent<SpriteRenderer>().color = _colorMatrix[i, j];
                }
            }

        }
    }
}
