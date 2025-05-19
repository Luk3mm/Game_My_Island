using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WOOD, GOLD, MEAT, FRUIT, STONE
}

public enum ItemCategory
{
    MATERIAL, COMSUMABLE
}

public enum GameState
{
    GAMEPLAY, INVENTORY, CRAFT
}

[Serializable]
public struct Recipe
{
    public Item item;
    public int amount;
}

public class CoreGame : MonoBehaviour
{
    public static CoreGame instance;

    public PlayerController playerController;
    public GameManager gameManager;
    public Inventory inventory;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
