using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level List", menuName = "Space Shooter/Level List", order = 2)]
public class LevelList : ScriptableObject
{
    public LevelData[] Levels = new LevelData[0];
}
