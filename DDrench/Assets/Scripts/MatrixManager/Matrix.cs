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
        private List<GameObject> _dominateCells;

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

        public Color[,] ColorMatrix
        {
            get
            {
                return _colorMatrix;
            }
        }

        public GameObject[,] MatrixCells
        {
            get
            {
                return _matrixCells;
            }
        }

        /// <summary>
        /// Costruttore di una matrice quadrata N x N
        /// </summary>
        /// <param name="n"></param>
        public Matrix(int N)
        {
            _dominateCells = new List<GameObject>();
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
                    GameObject newCell = new GameObject(String.Format("{0},{1}", i, j));
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
                    _matrixCells[i, j].transform.position = new Vector3(topLeftPivot.x + (j + 0.5f) * sizeMatrixCell, topLeftPivot.y - (i + 0.5f) * sizeMatrixCell, -2);
                    _matrixCells[i, j].GetComponent<SpriteRenderer>().sprite = Sprite.Create(new Texture2D(256, 256), new Rect(0.0f, 0.0f, 256, 256), new Vector2(0.5f, 0.5f), 256.0f);
                    _matrixCells[i, j].GetComponent<SpriteRenderer>().color = _colorMatrix[i, j];
                }
            }

        }

        /// <summary>
        /// Restituisce le celle dominate all'inizio del gioco
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetStartDominateCells(int startX, int startY, int x, int y)
        {

            if (_matrixCells != null)
            {

                if (x >= 0 && x < _sizeN && y >= 0 && y < _sizeN && _colorMatrix[x, y] == _colorMatrix[startX, startY])
                {
                    if (!_dominateCells.Contains(_matrixCells[x, y]))
                    {
                        _dominateCells.Add(_matrixCells[x, y]);
                    }
                    else
                    {
                        return new List<GameObject>();
                    }
                }
                else
                {
                    return new List<GameObject>();
                }

                MatrixUtility.AddRangeIfAlreadyNotInside(_dominateCells,GetStartDominateCells(startX, startY, x + 1, y));
                MatrixUtility.AddRangeIfAlreadyNotInside(_dominateCells,GetStartDominateCells(startX, startY, x - 1, y));
                MatrixUtility.AddRangeIfAlreadyNotInside(_dominateCells,GetStartDominateCells(startX, startY, x, y + 1));
                MatrixUtility.AddRangeIfAlreadyNotInside(_dominateCells,GetStartDominateCells(startX, startY, x, y - 1));

                return _dominateCells;

            }

            return new List<GameObject>();
        }

        public List<GameObject> GetFrontierCells()
        {
            List<GameObject> frontierCells = new List<GameObject>();
            for (int i = 0; i < _sizeN; i++)
            {
                for (int j = 0; j < _sizeN; j++)
                {
                    if (_dominateCells.Contains(_matrixCells[i, j]))
                    {
                        if (i - 1 >= 0 && i - 1 < _sizeN && j >= 0 && j < _sizeN && _colorMatrix[i - 1, j] != _colorMatrix[0, 0])
                        {
                            frontierCells.Add(_matrixCells[i - 1, j]);
                        }
                        if (i + 1 >= 0 && i + 1 < _sizeN && j >= 0 && j < _sizeN && _colorMatrix[i + 1, j] != _colorMatrix[0, 0])
                        {
                            frontierCells.Add(_matrixCells[i + 1, j]);
                        }
                        if (i >= 0 && i < _sizeN && j - 1 >= 0 && j - 1 < _sizeN && _colorMatrix[i, j - 1] != _colorMatrix[0, 0])
                        {
                            frontierCells.Add(_matrixCells[i, j - 1]);
                        }
                        if (i >= 0 && i < _sizeN && j + 1 >= 0 && j + 1 < _sizeN && _colorMatrix[i, j + 1] != _colorMatrix[0, 0])
                        {
                            frontierCells.Add(_matrixCells[i, j + 1]);
                        }
                    }
                }

            }
            return frontierCells;
        }

        public void Delete()
        {
            _matrixCells = null;
            _colorMatrix = null;
            _dominateCells = null;
        }

    }
}
