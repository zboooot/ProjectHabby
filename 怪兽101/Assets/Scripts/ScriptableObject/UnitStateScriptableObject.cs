using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    idleState,
    moveState,
    attackState,
    ultimateState
}

public class UnitStateScriptableObject : ScriptableObject
{

    public UnitState currentState { get; set; }
}
