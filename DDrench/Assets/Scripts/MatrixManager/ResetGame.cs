using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MatrixManager
{
    public class ResetGame: MonoBehaviour
    {
        public void OnMouseDown()
        {
            GameManager.Instance.RegenMatrix();
        }

    }
}
