using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int MaxScore = 0;
    public int MaxHull = 3;
    public float MaxEnergy = 100f;

    public int Hull;
    public float Energy;
}