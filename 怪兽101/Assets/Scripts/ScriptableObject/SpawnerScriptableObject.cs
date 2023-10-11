using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerScriptableObject", menuName = "ScriptableObject/Spawner")]
public class SpawnerScriptableObject : ScriptableObject
{
    public GameObject spawnedEntity;
    public float spawnRate;
    public Transform spawnLoc;
}
