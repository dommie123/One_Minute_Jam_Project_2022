using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public TimerBehavior timer;

    [SerializeField] private PlayerController player;
    [SerializeField] private int emptyPedestalCount;  // Essential for win condition
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject congratulationsScreen;
    [SerializeField] private GameObject playerInventory; // Disable this for when player wins/loses
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Transform beansSpawnPos;
    [SerializeField] private Transform vanillaSpawnPos;
    [SerializeField] private Transform pumpkinSpawnPos;
    
    private bool gameWon;
    private bool gameLost;
    private bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        gameWon = false;
        gameLost = false;
        gamePaused = false;

        // Spawn ingredients for player to pick up
        // IngredientBehavior.SpawnIngredient(new Vector3(5, 5), new Item {itemType = Item.ItemType.Milk, amount = 1});
        // IngredientBehavior.SpawnIngredient(new Vector3(-5, 5), new Item {itemType = Item.ItemType.CoffeeBeans, amount = 1});
        IngredientBehavior.SpawnIngredient(beansSpawnPos.position, new Item {itemType = Item.ItemType.GroundCoffee, amount = 1});
        IngredientBehavior.SpawnIngredient(vanillaSpawnPos.position, new Item {itemType = Item.ItemType.VanillaExtract, amount = 1});
        // IngredientBehavior.SpawnIngredient(new Vector3(4, 1), new Item {itemType = Item.ItemType.WhippedCream, amount = 1});
        IngredientBehavior.SpawnIngredient(pumpkinSpawnPos.position, new Item {itemType = Item.ItemType.Pumpkin, amount = 1});
        // IngredientBehavior.SpawnIngredient(new Vector3(3, 0), new Item {itemType = Item.ItemType.Torch, amount = 1});
    }

    private void Awake() {
        // TODO intro dialogue

        timer.IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        CheckLoseCondition();

        if (gameWon)
        {
            timer.IsActive = false;
            congratulationsScreen.SetActive(true);
            playerInventory.SetActive(false);
        }

        if (gameLost)
        {
            gameOverScreen.SetActive(true);
            playerInventory.SetActive(false);
        }
    }

    public void FillPedestal()
    {
        emptyPedestalCount--;
    }

    public void PauseGame()
    {
        if (gameWon || gameLost)
        {
            return;
        }

        gamePaused = !gamePaused;

        if (gamePaused)
        {
            Time.timeScale = 0f;
            timer.IsActive = false;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            timer.IsActive = true;
            pauseMenu.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        dialogueManager.EndDialogue();
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        dialogueManager.EndDialogue();
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


