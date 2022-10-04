using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public TimerBehavior timer;

    [SerializeField] private PlayerController player;
    [SerializeField] private int emptyPedestalCount;  // Essential for win condition
    private bool gameWon;
    private bool gameLost;
    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gameWon = false;
        gameLost = false;
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        CheckLoseCondition();

        if (gameWon)
        {
            timer.IsActive = false;
            Debug.Log("I won!");
        }

        if (gameLost)
        {
            Debug.Log("Game Over!");
        }
    }

    public void FillPedestal()
    {
        emptyPedestalCount--;
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Time.timeScale = 0f;
            timer.IsActive = false;
        }
        else
        {
            Time.timeScale = 1f;
            timer.IsActive = true;
        }
    }

    private void CheckWinCondition()
    {
        gameWon = emptyPedestalCount == 0 && !gameLost;
    }

    private void CheckLoseCondition()
    {
        if (timer == null)
        {
            return;
        }

        gameLost = timer.timeLeft <= 0 && !gameWon;
    }
}
