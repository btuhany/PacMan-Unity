using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ghost[] _ghostsArray;
    [SerializeField] Pacman _pacman;
    [SerializeField] Transform[] _pellets;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Start()
    {
        NewGame();
    }
    void NewGame()
    {
        SetScore(0);
        SetLives(3);
    }
    void NewRound()
    {
        foreach (Transform pellet in _pellets)
        {
            if(!pellet.gameObject.activeSelf)
                pellet.gameObject.SetActive(true);
        }
        ResetPositions();
    }
    void ResetPositions()
    {
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.gameObject.SetActive(true);
        }
        _pacman.gameObject.SetActive(true);
    }
    private void SetScore(int score)
    {
        this.Score = score;
    }
    void SetLives(int lives)
    {
        this.Lives = lives;
    }
    private void GameOver()
    {
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.gameObject.SetActive(false);
        }
        _pacman.gameObject.SetActive(false);
    }
}
