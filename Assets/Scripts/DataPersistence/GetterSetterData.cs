using UnityEngine;

public class GetterSetterData : MonoBehaviour
{
    public int level;
    public float highScore;
    public float[] highScores;

    public void SaveOnClick()
    {
        DataManager.Instance.highScore = highScore;
        DataManager.Instance.level = level;
        DataManager.Instance.highScores = highScores;
        DataManager.Instance.WriteData();
    }

    public void LoadOnClick()
    {
        DataManager.Instance.LoadData();
        highScores = DataManager.Instance.highScores;
    }
}
