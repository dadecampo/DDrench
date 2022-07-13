using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMovesCounter: MonoBehaviour
{
    private static TMPro.TextMeshProUGUI Text;

    void Awake()
    {
        Text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>(); 
    }
    
    public static void UpdateMovesCounter(int remainMoves)
    {
        Text.text = "Moves: " + remainMoves;
    }
}
