using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("Color Data")]
    [SerializeField] private ColorDatabase colorDataList;
    [SerializeField] private ColorManager colorManager;

    [Header("Bubble Settings")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private int spawnAreaWidth = 10;
    [SerializeField] private int spawnAreaHeight = 10;
    [SerializeField] private Vector2 spawnAreaCenter = Vector2.zero;

    [Header("Level Settings")]
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> spawnedBubbles = new List<GameObject>();
    private List<Vector3> exisitingPositions = new List<Vector3>();
    private float bubbleRadius = 0.5f;
    private int extraBubbles = 5;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        ClearLevel();

        if (colorDataList == null || colorDataList.colors.Length == 0) return;

        int randomIndex = Random.Range(0, colorDataList.colors.Length);
        ColorData selectedColorData = colorDataList.colors[randomIndex];

        List<ColorData> bubbleColors = new List<ColorData>();

        for (int i = 0; i < selectedColorData.bwryb[0]; i++)
        {
            bubbleColors.Add(colorManager.GetColorByName("Black"));
        }

        for (int i = 0; i < selectedColorData.bwryb[1]; i++)
        {
            bubbleColors.Add(colorManager.GetColorByName("White"));
        }

        for (int i = 0; i < selectedColorData.bwryb[2]; i++)
        {
            bubbleColors.Add(colorManager.GetColorByName("Red"));
        }

        for (int i = 0; i < selectedColorData.bwryb[3]; i++)
        {
            bubbleColors.Add(colorManager.GetColorByName("Yellow"));
        }

        for (int i = 0; i < selectedColorData.bwryb[4]; i++)
        {
            bubbleColors.Add(colorManager.GetColorByName("Blue"));
        }

        for (int i = 0; i < extraBubbles; i++)
        {
            int randomColorIndex = Random.Range(0, 4);
            ColorData randomColor = colorDataList.colors[randomColorIndex];
            bubbleColors.Add(randomColor);
        }


        foreach (var bubbleInfo in bubbleColors)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject newBubble = Instantiate(bubblePrefab, randomPosition, Quaternion.identity);
            spawnedBubbles.Add(newBubble);

            BubbleController bubbleController = newBubble.GetComponent<BubbleController>();
            if (bubbleController != null)
            {
                bubbleController.SetBubbleColor(bubbleInfo);
            }
            else
            {
                Debug.LogError("Bubble Controller is not found in the Bubble Prefab");
            }
        }

        UIManager.Instance.UpdateTargetColor(selectedColorData);
        UIManager.Instance.ClearCurrentColor();
    }

    private Vector3 GetRandomPosition()
    {
        int maxAttempts = 100;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float randomX = Random.Range(spawnAreaCenter.x - spawnAreaWidth / 2f, spawnAreaCenter.x + spawnAreaWidth / 2f);
            float randomY = Random.Range(spawnAreaCenter.y - spawnAreaHeight / 2f, spawnAreaCenter.y + spawnAreaHeight / 2f);
            Vector3 newPos = new Vector3(randomX, randomY, 0);

            bool overlap = false;
            foreach (Vector3 pos in exisitingPositions)
            {
                if (Vector3.Distance(pos, newPos) < bubbleRadius * 2f)
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
            {
                exisitingPositions.Add(newPos);
                return newPos;
            }
        }

        float fallbackX = Random.Range(spawnAreaCenter.x - spawnAreaWidth / 2f, spawnAreaCenter.x + spawnAreaWidth / 2f);
        float fallbackY = Random.Range(spawnAreaCenter.y - spawnAreaHeight / 2f, spawnAreaCenter.y + spawnAreaHeight / 2f);
        return new Vector3(fallbackX, fallbackY, 0);
    }

    private void ClearLevel()
    {
        foreach (GameObject bubble in spawnedBubbles)
        {
            if (bubble != null)
            {
                Destroy(bubble);
            }
        }

        spawnedBubbles.Clear();
        exisitingPositions.Clear();
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetScore();
        UIManager.Instance.UpdateScore(GameManager.Instance.GetScore());
        PlayerController.Instance.Stop();
        PlayerController.Instance.ResetPlayer(spawnPoint);
        ClearLevel();
        UIManager.Instance.ResetUI();
    }

    public IEnumerator LevelComplete()
    {
        GameManager.Instance.IncreaseScore();
        UIManager.Instance.UpdateScore(GameManager.Instance.GetScore());
        UIManager.Instance.ShowLevelCompleteText();
        yield return new WaitForSeconds(2f);
        UIManager.Instance.HideLevelCompleteText();
        PlayerController.Instance.Stop();
        PlayerController.Instance.ResetPlayer(spawnPoint);
        ClearLevel();
        UIManager.Instance.ResetUI();
        GenerateLevel();
    }

    public void NewLevel()
    {
        GameManager.Instance.ResetScore();
        UIManager.Instance.UpdateScore(GameManager.Instance.GetScore());
        PlayerController.Instance.Stop();
        PlayerController.Instance.ResetPlayer(spawnPoint);
        ClearLevel();
        UIManager.Instance.ResetUI();
        GenerateLevel();
    }
}
