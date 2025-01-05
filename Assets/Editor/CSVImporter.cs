using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CSVImporter : Editor
{
    [MenuItem("Tools/Import Colors from CSV")]
    public static void ImportCSV()
    {
        // 1. Pick the CSV file
        string path = EditorUtility.OpenFilePanel("Select color CSV file", "", "csv");
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogWarning("Import cancelled – no file selected.");
            return;
        }

        // 2. Ask for the ScriptableObject we want to populate
        ColorDatabase colorDatabase = SelectColorDatabase();
        if (colorDatabase == null)
        {
            Debug.LogWarning("Import cancelled – no ColorDatabase selected.");
            return;
        }

        // 3. Parse the CSV
        List<ColorData> importedColors = ParseCSV(path);

        // 4. Set the ScriptableObject data
        colorDatabase.colors = importedColors.ToArray();

        // 5. Mark the SO as dirty to ensure changes are saved
        EditorUtility.SetDirty(colorDatabase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Successfully imported {importedColors.Count} colors into {colorDatabase.name}");
    }

    private static ColorDatabase SelectColorDatabase()
    {
        // This opens an Object Picker in the Editor to select a ColorDatabase asset
        string soPath = EditorUtility.OpenFilePanelWithFilters("Select ColorDatabase asset", "Assets",
                                                               new string[] { "ScriptableObject", "asset" });
        if (string.IsNullOrEmpty(soPath))
            return null;

        // Convert the system path to an AssetDatabase relative path
        // Must be inside "Assets" folder
        string assetPath = "Assets" + soPath.Substring(Application.dataPath.Length);

        // Load the ColorDatabase from the asset path
        return AssetDatabase.LoadAssetAtPath<ColorDatabase>(assetPath);
    }

    private static List<ColorData> ParseCSV(string filePath)
    {
        List<ColorData> colorList = new List<ColorData>();

        // Read all lines from the CSV
        string[] lines = File.ReadAllLines(filePath);
        // If there's a header row, you might skip the first line

        foreach (string line in lines)
        {
            // Skip empty lines or header
            if (string.IsNullOrWhiteSpace(line))
                continue;
            if (line.StartsWith("ColorName")) // If your CSV has headers
                continue;

            string[] tokens = line.Split(',');

            if (tokens.Length < 9)
            {
                Debug.LogWarning($"Skipping invalid line: {line}");
                continue;
            }

            // Assuming the format: 
            // 0=ColorName, 1=Black,2=White,3=Red,4=Yellow,5=Blue, 6=R, 7=G, 8=B
            string name = tokens[0].Trim();

            int bVal = int.Parse(tokens[1].Trim());
            int wVal = int.Parse(tokens[2].Trim());
            int rVal = int.Parse(tokens[3].Trim());
            int yVal = int.Parse(tokens[4].Trim());
            int b2Val = int.Parse(tokens[5].Trim());

            int rInt = int.Parse(tokens[6].Trim());
            int gInt = int.Parse(tokens[7].Trim());
            int bInt = int.Parse(tokens[8].Trim());

            // Convert 0-255 to 0-1 range for UnityEngine.Color
            Color color = new Color(rInt / 255f, gInt / 255f, bInt / 255f, 1f);

            // Create the new ColorData
            ColorData colorData = new ColorData
            {
                colorName = name,
                bwryb = new int[] { bVal, wVal, rVal, yVal, b2Val },
                colorRGB = color
            };

            colorList.Add(colorData);
        }

        return colorList;
    }
}
