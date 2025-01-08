using UnityEngine;
using System;

[System.Serializable]
public class ColorData : IEquatable<ColorData>
{
    public string colorName;
    public int[] bwryb = new int[5];
    public Color colorRGB;

    #region Equality methods

    public int NumberOfColors()
    {
        int count = 0;
        foreach (var value in bwryb)
        {
            count += value;
        }
        return count;
    }

    public bool Equals(ColorData other)
    {
        if (other == null)
            return false;

        if (!string.Equals(this.colorName, other.colorName, StringComparison.OrdinalIgnoreCase))
            return false;

        if (this.bwryb == null && other.bwryb != null || this.bwryb != null && other.bwryb == null)
            return false;

        if (this.bwryb != null && other.bwryb != null)
        {
            if (this.bwryb.Length != other.bwryb.Length)
                return false;

            for (int i = 0; i < this.bwryb.Length; i++)
            {
                if (this.bwryb[i] != other.bwryb[i])
                    return false;
            }
        }

        if (!this.colorRGB.Equals(other.colorRGB))
            return false;

        return true;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ColorData);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;

            hash = hash * 23 + (colorName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(colorName) : 0);

            if (bwryb != null)
            {
                foreach (var value in bwryb)
                {
                    hash = hash * 23 + value.GetHashCode();
                }
            }

            hash = hash * 23 + colorRGB.GetHashCode();

            return hash;
        }
    }

    public static bool operator ==(ColorData left, ColorData right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(ColorData left, ColorData right)
    {
        return !(left == right);
    }

    #endregion
}
