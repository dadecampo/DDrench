using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MatrixManager
{
    public class GameManager : MonoBehaviour
    {
        private Matrix _matrix;
        private float _sizeBackground;
        private Vector2 _topLeftPosition;
        private List<GameObject> _frontierCells;
        private List<GameObject> _dominateCells;
        private ShowMovesCounter _showMovesCounter;

        private static GameManager _instance;
        private static object _instanceLock = new object();

        public Matrix Matrix
        {
            get
            {
                return _matrix;
            }
        }

        public int RemainMoves { get; set; }

        public static GameManager Instance { get; private set; }

        private void Start()
        {

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                _sizeBackground = this.gameObject.transform.localScale.x;
                _topLeftPosition = new Vector2(this.gameObject.transform.GetChild(0).position.x, this.gameObject.transform.GetChild(0).position.y);
                GenNewMatrix(14);
                RemainMoves = 30;
                ShowMovesCounter.UpdateMovesCounter(RemainMoves);
                WinLoseAdvise.UpdateWinLoseVisible(false);
                Instance = this;
            }
        }

        /// <summary>
        /// Alla distruzione della matrice ne genero una nuova
        /// </summary>
        public void RegenMatrix()
        {
            DeleteAll();
            _sizeBackground = this.gameObject.transform.localScale.x;
            _topLeftPosition = new Vector2(this.gameObject.transform.GetChild(0).position.x, this.gameObject.transform.GetChild(0).position.y);
            GenNewMatrix(14);
            RemainMoves = 30;
            ShowMovesCounter.UpdateMovesCounter(RemainMoves);
            WinLoseAdvise.UpdateWinLoseVisible(false);
            Instance = this;
        }

        private void OnDestroy()
        {
            DeleteAll();
        }

        private void DeleteAll()
        {
            foreach(GameObject go in _matrix.MatrixCells)
            {
                Destroy(go);
            }
            _matrix.Delete();
        }


        public void GenNewMatrix(int size)
        {
            _matrix = new Matrix(size);
            _matrix.DrawMatrix(_sizeBackground, _topLeftPosition);
            _dominateCells = _matrix.GetStartDominateCells(0, 0, 0, 0);
            _frontierCells = _matrix.GetFrontierCells();
        }

        public void PlayWithColor(Color color)
        {
            if (RemainMoves > 0)
            {
                RemainMoves--;
                ShowMovesCounter.UpdateMovesCounter(RemainMoves);
                for (int i = 0; i < _matrix.SizeN; i++)
                {
                    for (int j = 0; j < _matrix.SizeN; j++)
                    {
                        if (_dominateCells.Contains(_matrix.MatrixCells[i, j]))
                        {
                            _matrix.MatrixCells[i, j].GetComponent<SpriteRenderer>().color = color;
                            _matrix.ColorMatrix[i, j] = color;
                        }
                        if (_frontierCells.Contains(_matrix.MatrixCells[i, j]) && _matrix.ColorMatrix[i, j] == color)
                        {
                            MatrixUtility.AddRangeIfAlreadyNotInside(_dominateCells, _matrix.GetStartDominateCells(i, j, i, j));
                        }
                    }
                }

                for (int i = 0; i < _matrix.SizeN; i++)
                {
                    for (int j = 0; j < _matrix.SizeN; j++)
                    {
                        if (_dominateCells.Contains(_matrix.MatrixCells[i, j]))
                        {
                            _matrix.MatrixCells[i, j].GetComponent<SpriteRenderer>().color = color;
                            _matrix.ColorMatrix[i, j] = color;
                        }
                    }
                }

                _frontierCells = _matrix.GetFrontierCells();
                bool isWin = CheckIfWin();
                if (isWin || RemainMoves == 0)
                {
                    WinLoseAdvise.UpdateWinLoseText(isWin);
                    WinLoseAdvise.UpdateWinLoseVisible(true);
                }
            }
        }

        private bool CheckIfWin()
        {
            Color currentColor = _matrix.ColorMatrix[0, 0];
            for (int i = 0; i < _matrix.SizeN; i++)
            {
                for (int j = 0; j < _matrix.SizeN; j++)
                {
                    if(!currentColor.Equals(_matrix.ColorMatrix[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}