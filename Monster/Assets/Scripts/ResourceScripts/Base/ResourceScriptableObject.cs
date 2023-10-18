using UnityEngine;

[CreateAssetMenu(fileName = "ResourceScriptableObject", menuName = "ScriptableObject/GNA")]
public class ResourceScriptableObject :ScriptableObject
{
    public int currentGNA;
    public int inGameGNA;

    private void OnEnable()
    {
        inGameGNA = 0;
    }
}
