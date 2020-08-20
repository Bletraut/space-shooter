using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Space Shooter/Level Data", order = 1)]
public class LevelData: ScriptableObject
{
    public enum Conditions
    {
        Survive,
        Destroy
    }
    [Header("Level settings")]
    public string LevelName = "Level 1";
    public Conditions VictoryCondition = Conditions.Survive;
    public float SurviveTime = 20f;
    public int MeteorsCount = 40;

    [Header("Player settings")]
    public int PlayerLifes = 3;

    [Header("Hazards settings")]
    public GameObject[] HazardsTypes = new GameObject[0];
    public Bounds SpawnBounds;

    [Header("Wave settings")]
    public float WaveStartDelay = 1f;
    public float HazardsCount = 5;
    public float HazardsDelay = 5;
}
