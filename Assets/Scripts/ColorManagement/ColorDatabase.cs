using UnityEngine;

[CreateAssetMenu(fileName = "ColorDatabase", menuName = "Game/ColorDatabase")]
public class ColorDatabase : ScriptableObject
{
    [Tooltip("List of all named colors")]
    public ColorData[] colors;
}
