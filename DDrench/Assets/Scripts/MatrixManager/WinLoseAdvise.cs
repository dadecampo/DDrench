using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseAdvise : MonoBehaviour
{
    private static TMPro.TextMeshProUGUI _text;
    private static SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        _text = GameObject.FindObjectOfType<TMPro.TextMeshProUGUI>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        UpdateWinLoseVisible(false);
    }

    public static void UpdateWinLoseVisible(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
        _text.enabled=isVisible;
    }

    public static void UpdateWinLoseText(bool isWin)
    {
        if (isWin)
            _text.text = "You Win!";
        else
            _text.text = "You Lose!";
    }

}
