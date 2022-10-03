using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Data", menuName = "Data/Player Data",order = 51)]
public class PlayerData : ScriptableObject
{
    [field:SerializeField] public float Speed { get; private set; }
    [field:SerializeField] public int MaxInvetoryItems { get; private set; }
}
