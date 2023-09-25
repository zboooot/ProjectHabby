using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ZooGameManagerScriptableObject", menuName = "ScriptableObject/ZooManager")]
public class ZooGameManagerScriptableObject : ScriptableObject
{
    public int incomeEarned;
    public int storedIncome;
}
