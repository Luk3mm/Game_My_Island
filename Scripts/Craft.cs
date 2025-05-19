using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Scriptable Item/New Craft", order = 2)]
public class Craft : ScriptableObject
{
    public Recipe[] recipe;
    public GameObject produce;
    public int amount;
    public float timeToProduction;
}
