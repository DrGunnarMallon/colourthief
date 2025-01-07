using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public ColorDatabase colorDatabase;

    #region Private methods

    private bool IsExactMatch(int[] bwryb1, int[] bwryb2)
    {
        for (int i = 0; i < 5; i++)
        {
            if (bwryb1[i] != bwryb2[i])
            {
                return false;
            }
        }
        return true;
    }

    private float Distance5D(int[] bwryb1, int[] bwryb2)
    {
        float sum = 0;
        for (int i = 0; i < bwryb1.Length; i++)
        {
            float diff = (float)bwryb1[i] - bwryb2[i];
            sum += diff * diff;
        }
        return Mathf.Sqrt(sum);
    }

    #endregion

    #region Public methods

    public ColorData GetColorExact(int[] bwrybValue)
    {
        if (bwrybValue == null || bwrybValue.Length != 5)
        {
            return null;
        }

        foreach (ColorData colorData in colorDatabase.colors)
        {
            if (IsExactMatch(colorData.bwryb, bwrybValue))
            {
                return colorData;
            }
        }
        return null;
    }


    public ColorData GetColorNearest(int[] bwrybValue)
    {
        if (bwrybValue == null || bwrybValue.Length != 5)
        {
            Debug.LogError("Invalid color value");
            return null;
        }

        float bestDist = float.MaxValue;
        ColorData bestMatch = null;

        foreach (ColorData colorData in colorDatabase.colors)
        {
            float d = Distance5D(colorData.bwryb, bwrybValue);
            if (d < bestDist)
            {
                bestDist = d;
                bestMatch = colorData;
            }
        }
        return bestMatch;
    }


    public ColorData GetColorByName(string colorName)
    {
        foreach (ColorData colorData in colorDatabase.colors)
        {
            if (colorData.colorName == colorName)
            {
                return colorData;
            }
        }
        return null;
    }


    public ColorData MixColors(int[] bwryb, int[] bwryb2)
    {
        int[] newBwryb = new int[5];
        for (int i = 0; i < 5; i++)
        {
            newBwryb[i] = bwryb[i] + bwryb2[i];
        }

        ColorData returnColor = GetColorExact(newBwryb);
        if (returnColor == null)
        {
            returnColor = GetColorNearest(newBwryb);
        }
        return returnColor;
    }

    #endregion
}