using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // static instance for easy access
    public static LevelManager Instance { get; private set; }

    [Header("UI")]
    public TMP_Text budgetText;
    public TMP_Text scoreText;
    public TMP_Text totalCostText;
    public GameObject endPanel;
    public TMP_Text endMessageText;
    public GameObject nextLevelButton;

    [Header("Score Settings")]
    public int currentScore = 0;
    public int passScore = 80;

    // Budget tracking is moved to ResourceController, but we keep the UI updates here
    [HideInInspector] public int currentBudget;
    [HideInInspector] public int totalCost = 0;
    public int startBudget = 1000; 

    [Header("Gameplay Flags")]
    public int wallCount = 0;          // How many walls the player created
    public bool floorCreated = false;  // True if concrete was used to create the floor
    public bool doorPlaced = false;    // True if at least one door was spawned
    public bool hammerUsed = false;    // True if hammer was used at least once
    public bool budgetExceeded = false; // Flag for budget penalty

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentBudget = startBudget;
        totalCost = 0;
        budgetExceeded = false;

        UpdateUI();

        if (endPanel != null)
            endPanel.SetActive(false);

        if (nextLevelButton != null)
            nextLevelButton.SetActive(false);
    }

    // function to update all UI elements
    public void UpdateUI()
    {
        if (budgetText != null)
            budgetText.text = "Budget: " + currentBudget;

        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (totalCostText != null)
            totalCostText.text = "Total: " + totalCost;
    }

    int CalculateScore()
    {
        int score = 100;

        // must create a concrete floor (-30)
        if (!floorCreated)
        {
            score -= 30;
            Debug.Log("Penalty: No concrete floor (-30)");
        }

        
        if (!hammerUsed)
        {
            score -= 20;
            Debug.Log("Penalty: Hammer not used (-20)");
        }

        //must use at least two walls (-30)
        if (wallCount < 2)
        {
            score -= 40;
            Debug.Log("Penalty: Less than two walls used (-30)");
        }

        // panishment for exceeding budget
        if (budgetExceeded)
        {
            score -= 10;
            Debug.Log("Penalty: Budget Exceeded (-10)");
        }

        if (score < 0)
            score = 0;

        return score;
    }

    public void OnFinishLevelButton()
    {
        int finalScore = CalculateScore();
        currentScore = finalScore;
        UpdateUI(); 

        if (endPanel != null)
            endPanel.SetActive(true);

        if (finalScore >= passScore)
        {
            if (endMessageText != null)
                endMessageText.text = "Great job! You passed";

            if (nextLevelButton != null)
                nextLevelButton.SetActive(true);
        }
        else
        {
            if (endMessageText != null)
                endMessageText.text = "Score below " + passScore + ". Try again - " +
                    "Pass this level to unlock the next one.";

            if (nextLevelButton != null)
                nextLevelButton.SetActive(false);
        }
    }

    public void OnRetryButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}