using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    public GameObject enemyObj;
    public float reactionTime;
}

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public List<EnemyData> enemyData;
}
