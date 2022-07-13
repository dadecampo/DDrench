using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.MatrixManager
{
    public class MatrixUtility
    {

        public static void AddRangeIfAlreadyNotInside(List<GameObject> listReceiver, List<GameObject> listToAdd)
        {
            foreach (GameObject go in listToAdd)
            {
                if (!listReceiver.Contains(go))
                {
                    listReceiver.Add(go);
                }
            }
        }

    }
}
