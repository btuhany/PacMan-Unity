using TMPro;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();        
    }
    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged += HandleOnHighScore;
    }
    void HandleOnHighScore()
    {
        _text.SetText(GameManager.Instance.Score.ToString());   
    }
}
