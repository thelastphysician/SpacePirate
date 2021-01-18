using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int MaxScore = 0;
    public int MaxHull = 3;
    public float MaxEnergy = 100f;
    public float BaseFirerate = 10f;
    public float Range = 100f;
    public float Volume = 1f;
    public bool isFullscreen = true;

    public int Hull;
    public float Energy;
    public int Score;

}