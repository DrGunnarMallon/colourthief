using UnityEngine;
using System;

public class GameEvents
{
    // UIEvents
    public static event Action OnLevelComplete;
    public static event Action<int> OnScoreUpdate;
}
