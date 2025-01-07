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

    private List<GameObject> spawnedBubbles = new List<GameObject>();
    private List<Vector3> exisitingPositions = new List<Vector3>();
    private float bubbleRadius = 0.5f;
    private int totalBubbles = 10;

    private ColorData targetColor;

    #region Events

    private void OnEnable()
    {
        EventsManager.Instance.OnNewLevel += NewLevel;
        EventsManager.Instance.OnCreateBubble += CreateBubble;
        EventsManager.Instance.OnMixingColorChanged += CheckColorMatch;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnNewLevel -= NewLevel;
        EventsManager.Instance.OnCreateBubble -= CreateBubble;
        EventsManager.Instance.OnMixingColorChanged -= CheckColorMatch;
    }

    #endregion

    #region Level Generation

    public void NewLevel()
    {
        EventsManager.Instance.TriggerResetScore();
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        Debug.Log("Generating Level");

        ClearLevel();

        if (colorDataList == null || colorDataList.colors.Length == 0) return;

        int randomIndex = Random.Range(0, colorDataList.colors.Length);
        ColorData selectedColorData = colorDataList.colors[randomIndex];

        List<ColorData> bubbleColors = new List<ColorData>();

        bubbleColors.AddRange(makeBubbles("Black", selectedColorData.bwryb[0]));
        bubbleColors.AddRange(makeBubbles("White", selectedColorData.bwryb[1]));
        bubbleColors.AddRange(makeBubbles("Red", selectedColorData.bwryb[2]));
        bubbleColors.AddRange(makeBubbles("Yellow", selectedColorData.bwryb[3]));
        bubbleColors.AddRange(makeBubbles("Blue", selectedColorData.bwryb[4]));

        for (int i = bubbleColors.Count; i < totalBubbles; i++)
        {
            bubbleColors.Add(colorDataList.colors[Random.Range(0, 4)]);
        }

        foreach (var bubbleInfo in bubbleColors)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject newBubble = Instantiate(bubblePrefab, randomPosition, Quaternion.identity);
            spawnedBubbles.Add(newBubble);

            BubbleController bubbleController = newBubble.GetComponent<BubbleController>();
            bubbleController.SetBubbleColor(bubbleInfo);
        }

        targetColor = selectedColorData;

        Debug.Log("LEVELMANAGER: Triggering Target Change Event");
        EventsManager.Instance.TriggerTargetChanged(selectedColorData);
    }

    private List<ColorData> makeBubbles(string color, int count)
    {
        List<ColorData> bubbles = new List<ColorData>();
        for (int i = 0; i < count; i++)
        {
            bubbles.Add(colorManager.GetColorByName(color));
        }
        return bubbles;
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

    #endregion

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

    public IEnumerator LevelComplete()
    {
        EventsManager.Instance.TriggerIncreaseScore(1);
        UIManager.Instance.ShowLevelCompleteText();
        yield return new WaitForSeconds(2f);
        UIManager.Instance.HideLevelCompleteText();
        ClearLevel();
        GenerateLevel();
    }

    private void CreateBubble(Vector3 position, ColorData color)
    {
        GameObject newBubble = Instantiate(bubblePrefab, position, Quaternion.identity);
        spawnedBubbles.Add(newBubble);

        BubbleController bubbleController = newBubble.GetComponent<BubbleController>();
        bubbleController.SetBubbleColor(color);
    }

    public void CheckColorMatch(ColorData color)
    {
        if (color == targetColor)
        {
            GameManager.Instance.LevelComplete();
        }
    }

}
