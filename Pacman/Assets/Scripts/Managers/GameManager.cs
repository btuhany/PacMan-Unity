using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] Ghost[] _ghostsArray;
    [SerializeField] Pacman _pacman;
    [SerializeField] Transform _pellets;
    [SerializeField] int _ghostMultiplier = 1;
    [SerializeField] float _powerModeDuration = 8f;

    Vector3 _pacmanInitialPos;
    public event System.Action OnScoreChanged;
    public static GameManager Instance;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    private void Awake()
    {
        _pacmanInitialPos = _pacman.transform.position;
        Instance = this;
       
    }

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
        ResetAllStates();
    }
    void ResetAllStates()
    {
        ResetGhostMultiplier();
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.DefaultLook();
            ghost.ResetState();
            ghost.Movement.Rb.simulated = true;
        }
        GhostStatesManager.Instance.ResetStates();
        //TODO: _pacman.ResetState();
        _pacman.ResetState();
        _pacman.transform.position = _pacmanInitialPos;
        _pacman.gameObject.SetActive(true);
    }
    private void GameOver()
    {
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.StopMovement();
        }
        _pacman.GameFinished();

    }

    public void GhostEaten(Ghost ghost)
    {
        IncreaseScore(_ghostMultiplier * ghost.Point);
        _ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        _pacman.Eaten();
        SetLives(this.Lives - 1);
        foreach (Ghost ghost in _ghostsArray)
        {
            ghost.Movement.IsActive = false;
            ghost.StopMovement();
            ghost.Movement.Rb.simulated = false;
        }
        if (Lives > 0)
        {
            Invoke(nameof(ResetAllStates), 2.5f);
        }
        else
        {
            GameOver();
        }
    }
    private void SetScore(int score)
    {
        this.Score = score;
        OnScoreChanged?.Invoke();
    }
    public void IncreaseScore(int score)
    {
        this.Score += score;
        OnScoreChanged?.Invoke();
    }
    void SetLives(int lives)
    {
        this.Lives = lives;
    }
    public void PowerPelletEaten()
    {
        //TODO
        GhostStatesManager.Instance.PowerPelletMode();
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
            GameOver();
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
