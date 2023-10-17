using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level")]
public class LevelData : ScriptableObject
{
    public List<LevelDataItem> levelDataItems;

}
