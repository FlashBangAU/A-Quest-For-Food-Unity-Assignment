using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    // Variables that need to be stored
    public int level;
    public float highScore;
    public float[] highScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [System.Serializable]
    class SaveData
    {
        public float[] highScores;
    }

    public void WriteData()
    {
        // Ensure highScores is initialized and has enough space
        if (highScores == null || level >= highScores.Length)
        {
            // Expand the array if necessary
            ExpandHighScoresArray(level + 1);
        }

        // Update high score for the current level
        highScores[level] = highScore;

        // Prepare the data to be saved
        SaveData data = new SaveData
        {
            highScores = (float[])highScores.Clone() // Clone to prevent external modification
        };

        // Serialize the data to JSON
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Data saved to: " + Application.persistentDataPath);
        Debug.Log($"High Scores after saving: {string.Join(", ", highScores)}"); // Debug output
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.highScores != null)
            {
                // Expand the current highScores array if necessary
                if (highScores == null || data.highScores.Length > highScores.Length)
                {
                    ExpandHighScoresArray(data.highScores.Length);
                }

                // Copy the loaded highScores into the current highScores
                Array.Copy(data.highScores, highScores, Math.Min(data.highScores.Length, highScores.Length));
                Debug.Log($"High Scores after loading: {string.Join(", ", highScores)}"); // Debug output
            }
            else
            {
                highScores = new float[0];
            }
        }
        else
        {
            highScores = new float[0];
        }
    }

    private void ExpandHighScoresArray(int newSize)
    {
        // If the newSize is less than or equal to the current length, do nothing
        if (highScores != null && newSize <= highScores.Length)
        {
            Debug.LogWarning($"No need to expand the array. Requested size: {newSize}, Current size: {highScores.Length}");
            return;
        }

        // Create a new array with the determined size
        float[] newHighScores = new float[newSize];

        if (highScores != null)
        {
            // Copy existing data to the new array
            Array.Copy(highScores, newHighScores, Math.Min(highScores.Length, newSize));
            Debug.Log($"Copied {Math.Min(highScores.Length, newSize)} elements to new array of size {newSize}."); // Debug output
        }
        else
        {
            Debug.Log($"Created new array of size {newSize} with no existing data."); // Debug output
        }

        // Assign the new array to highScores
        highScores = newHighScores;
        Debug.Log($"High Scores array expanded to size {newSize}."); // Debug output
    }

    // Method to get the high score for a specific level
    public float GetHighScoreForLevel(int level)
    {
        if (highScores != null && level >= 0 && level < highScores.Length)
        {
            return highScores[level];
        }
        return 0f; // Return a default value if out of range
    }

    public int highScoreGetLength()
    {
        return highScores.Length;
    }
}
