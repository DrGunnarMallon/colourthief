using UnityEngine;
// using System;
using System.Collections.Generic;

public class ColorHistoryManager : MonoBehaviour
{
    // [SerializeField] private int manNumberOfColors;
    [SerializeField] private ColorHistoryCircle colorHistoryCirclePrefab;
    [SerializeField] private float spacing = 0.5f;

    private int maxNumberOfColors;
    private int numberOfColors = 0;
    private ColorHistoryCircle[] colorHistory;
    private ColorHistoryCircle[] solutionCircles;
    private Vector3[] circlePositions;

    private void OnEnable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnAddColorToHistory += AddColor;
            EventsManager.Instance.OnNewLevel += () => { numberOfColors = 0; };
            EventsManager.Instance.OnShowSolution += ShowSolution;
        }
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnAddColorToHistory -= AddColor;
            EventsManager.Instance.OnNewLevel -= () => { numberOfColors = 0; };
            EventsManager.Instance.OnShowSolution -= ShowSolution;
        }
    }

    private void ClearCircles()
    {
        if (colorHistory != null)
        {
            foreach (var circle in colorHistory)
            {
                if (circle != null)
                {
                    Destroy(circle.gameObject);
                }
            }

            if (solutionCircles == null) return;

            foreach (var circle in solutionCircles)
            {
                if (circle != null)
                {
                    Destroy(circle.gameObject);
                }
            }
        }
    }

    public void CreateCircles(int numberOfCircles)
    {
        ClearCircles();

        maxNumberOfColors = numberOfCircles;
        numberOfColors = 0;
        colorHistory = new ColorHistoryCircle[maxNumberOfColors];
        circlePositions = new Vector3[maxNumberOfColors];

        Vector3 startPosition = transform.position;

        for (int i = 0; i < maxNumberOfColors; i++)
        {
            circlePositions[i] = startPosition + new Vector3(i * spacing, 0, 0);

            ColorHistoryCircle newCircle = Instantiate(
                colorHistoryCirclePrefab,
                circlePositions[i],
                Quaternion.identity
            );

            // Set the initial color to gray
            newCircle.SetColor(Color.gray);

            // Store the reference for later use
            colorHistory[i] = newCircle;
        }
    }

    public void ShowSolution(List<ColorData> colors)
    {
        Vector3 startPosition = transform.position;
        solutionCircles = new ColorHistoryCircle[colors.Count];

        for (int i = 0; i < colors.Count; i++)
        {
            Vector3 circlePosition = startPosition + new Vector3(i * spacing, 0.5f, 0);

            ColorHistoryCircle newCircle = Instantiate(
                colorHistoryCirclePrefab,
                circlePosition,
                Quaternion.identity
            );

            newCircle.SetColor(colors[i]);

            solutionCircles[i] = newCircle;
        }
    }

    private void AddColor(ColorData data)
    {
        if (numberOfColors < maxNumberOfColors)
        {
            colorHistory[numberOfColors].SetColor(data);
            numberOfColors++;
        }
        if (numberOfColors == maxNumberOfColors)
        {
            EventsManager.Instance.TriggerColorHistoryFull();
        }
    }
}
