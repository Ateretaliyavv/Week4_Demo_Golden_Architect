using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public static ResourceController Instance { get; private set; }

    [Header("Tool Costs")]
    public int concreteCost = 100;
    public int hammerCost = 20;

    [Header("Part Costs")]
    public int wallCost = 200;
    public int doorCost = 100;
    public int roofCost = 300;

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

    public bool TrySpend(int amount)
    {
        // Update totalCost only
        LevelManager.Instance.totalCost += amount;

        // Check for budget overrun by comparing totalCost to startBudget
        if (LevelManager.Instance.totalCost > LevelManager.Instance.startBudget)
        {
            if (!LevelManager.Instance.budgetExceeded)
            {
                Debug.LogWarning("Budget Exceeded! Total Cost is: " + LevelManager.Instance.totalCost +
                                 " (Limit: " + LevelManager.Instance.startBudget + ")");
            }
            LevelManager.Instance.budgetExceeded = true;
        }

        LevelManager.Instance.UpdateUI();

        return true;
    }
}