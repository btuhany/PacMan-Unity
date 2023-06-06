using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] Ghost[] _ghostsArray;
    [SerializeField] Pacman _pacman;
    [SerializeField] Transform _pellets;
    [SerializeField] int _ghostMultiplier = 1;
    [SerializeField] float _powerModeDuration = 8f;

    bool _isGameOn;
    public static GameManager Instance;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    private void Awake()
    {
        Instance = this;
       
    }

    private void Start()
    {
        NewGame();
    }
    void NewGame()
    {
        _isGameOn = true;
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
        ResetAllStates();
    }
    void ResetAllStates()
    {
        ResetGhostMultiplier();
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
    public void IncreaseScore(int score)
    {
        this.Score += score;
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
        _isGameOn = false;
    }

    public void GhostEaten(Ghost ghost)
    {
        IncreaseScore(_ghostMultiplier * ghost.Point);
        _ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        _pacman.gameObject.SetActive(false);
        SetLives(this.Lives - 1);
        if(Lives > 0)
        {
            Invoke(nameof(ResetAllStates), 2.5f);
        }
        else
        {
            GameOver();
        }
    }
    public void PowerPelletEaten()
    {
        //TODO
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), _powerModeDuration);
    }
    bool IsThereAnyPelletLeft()
    {
        foreach (Transform pellet in _pellets)
        {
            if (pellet.gameObject.activeSelf)
            {

                return true;
            }

        }
        return false;
    }
    public void IsGameEnded()
    {
        if (!IsThereAnyPelletLeft())
        {
            _pacman.gameObject.SetActive(false);
            Debug.Log("bitti");
            Invoke(nameof(NewRound), 3f);
        }

    }
    private void ResetGhostMultiplier()
    {
        _ghostMultiplier = 1;
    }
    public void DebugPellet()
    {
        foreach (Transform pellet in _pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                Debug.Log(pellet.name);
            
            }

        }
    }
}
