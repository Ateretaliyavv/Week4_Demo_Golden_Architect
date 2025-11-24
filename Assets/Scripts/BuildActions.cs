using UnityEngine;

public class BuildActions : MonoBehaviour
{
    [Header("Part Prefabs")]
    public GameObject wallPrefab;
    public GameObject doorPrefab;
    public GameObject roofPrefab;
    public GameObject floorPrefab;

    [Header("Build Area")]
    public Transform buildAreaCenter;


    // ===== TOOLS  =====
    public void OnConcreteButton()
    {
        int cost = ResourceController.Instance.concreteCost;
        if (ResourceController.Instance.TrySpend(cost))
        {
            Debug.Log("Concrete used!");

            if (!LevelManager.Instance.floorCreated)
            {
                if (floorPrefab != null && buildAreaCenter != null)
                {
                    Vector3 pos = buildAreaCenter.position;
                    pos.y -= 1f;
                    pos.z = 0f;

                    Instantiate(floorPrefab, pos, Quaternion.identity);
                    LevelManager.Instance.floorCreated = true;
                    Debug.Log("Concrete floor created");
                }
                else
                {
                    Debug.LogWarning("floorPrefab or buildAreaCenter is not assigned");
                }
            }
        }
    }

    public void OnHammerButton()
    {
        int cost = ResourceController.Instance.hammerCost;
        if (ResourceController.Instance.TrySpend(cost))
        {
            Debug.Log("Hammer used");
            LevelManager.Instance.hammerUsed = true;
        }
    }

    // ===== WAREHOUSE  =====
    public void SpawnWall()
    {
        int cost = ResourceController.Instance.wallCost;
        if (!ResourceController.Instance.TrySpend(cost))
            return;

        if (wallPrefab == null || buildAreaCenter == null)
        {
            Debug.LogWarning("WallPrefab or BuildAreaCenter is not assigned!");
            return;
        }

        Vector3 pos = buildAreaCenter.position;
        pos.z = 0f;

        Instantiate(wallPrefab, pos, Quaternion.identity);
        LevelManager.Instance.wallCount++; 
    }

    public void SpawnDoor()
    {
        int cost = ResourceController.Instance.doorCost;
        if (!ResourceController.Instance.TrySpend(cost))
            return;

        if (doorPrefab == null || buildAreaCenter == null)
        {
            Debug.LogWarning("DoorPrefab or BuildAreaCenter is not assigned!");
            return;
        }

        Vector3 pos = buildAreaCenter.position;
        pos.y -= 0.5f;
        pos.z = 0f;

        Instantiate(doorPrefab, pos, Quaternion.identity);
        LevelManager.Instance.doorPlaced = true; 
    }

    public void SpawnRoof()
    {
        int cost = ResourceController.Instance.roofCost;
        if (!ResourceController.Instance.TrySpend(cost))
            return;

        if (roofPrefab == null || buildAreaCenter == null)
        {
            Debug.LogWarning("RoofPrefab or BuildAreaCenter is not assigned!");
            return;
        }

        Vector3 pos = buildAreaCenter.position + new Vector3(0f, 1.5f, 0f);
        pos.z = 0f;

        Instantiate(roofPrefab, pos, Quaternion.identity);
    }
}
