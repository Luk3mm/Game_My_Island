using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ResourceLoot
{
    public GameObject resource;
    public int amount;
}

public class GameManager : MonoBehaviour
{
    public GameState gamestate;
    public float interactionDistance;
    public float timeToDelete;
    public int playerLife;
    public int playerLifeMax;
    public int playerEnergy;
    public int playerEnergyMax;
    public int playerLevel;
    public float distanceToSpawnResource;
    public float timeToSpawnResource;


    public GameObject actionCursor;
    public GameObject objCraft;

    public List<IslandManager> islandManageList = new List<IslandManager>();

    public RecipeReady[] recipes;

    private GameObject interactiveObj;

    private void FixedUpdate()
    {
        if(interactiveObj != null)
        {
            if(Vector2.Distance(CoreGame.instance.playerController.transform.position, interactiveObj.transform.position) <= interactionDistance)
            {
                actionCursor.SetActive(true);
            }
            else
            {
                actionCursor.SetActive(false);
            }
        }
    }

    public void ActiveCursor(GameObject obj)
    {
        if(gamestate != GameState.GAMEPLAY)
        {
            return;
        }

        interactiveObj = obj;

        if(Vector2.Distance(CoreGame.instance.playerController.transform.position, interactiveObj.transform.position) <= interactionDistance)
        {
            actionCursor.transform.position = obj.transform.position;
            actionCursor.SetActive(true);
        }
    }

    public void DisableCursor()
    {
        actionCursor.SetActive(false);
        interactiveObj = null;
    }

    public void ObjectHit()
    {
        if(interactiveObj == null)
        {
            return;
        }

        if(actionCursor.activeSelf == true)
        {
            interactiveObj.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Loot(Item item, Vector3 position)
    {
        DisableCursor();

        int dir = -1;

        for(int i = 0; i < item.lootAmount; i++)
        {
            GameObject loot = Instantiate(item.lootPrefab, position, transform.localRotation);

            loot.SendMessage("Active", dir, SendMessageOptions.DontRequireReceiver);

            dir *= -1;
        }
    }

    public bool IsNeedLife()
    {
        bool needLife = playerLife < playerLifeMax;

        return needLife;
    }

    public bool IsNeedEnergy()
    {
        bool needEnergy = playerEnergy < playerEnergyMax;

        return needEnergy;
    }

    public void SetPlayerLife(int amount)
    {
        playerLife += amount;

        if(playerLife > playerLifeMax)
        {
            playerLife = playerLifeMax;
        }
    }

    public void SetPlayerEnergy(int amount)
    {
        playerEnergy += amount;

        if(playerEnergy > playerEnergyMax)
        {
            playerEnergy = playerEnergyMax;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        gamestate = newState;

        switch (gamestate)
        {
            case GameState.INVENTORY:
                interactiveObj = null;
                actionCursor.SetActive(false);
                break;

            case GameState.CRAFT:
                
                break;
        }
    }

    public bool PlayerDistance(Vector3 position)
    {
        float distance = Vector3.Distance(CoreGame.instance.playerController.transform.position, position);
        bool isReady = distance >= distanceToSpawnResource;
        return isReady;
    }

    public void StartCraftMode(GameObject obj)
    {
        objCraft = obj;
        ChangeGameState(GameState.CRAFT);

        foreach (IslandManager im in islandManageList)
        {
            im.CraftMode();
        }

        CoreGame.instance.inventory.inventoryPanel.SetActive(false);
    }

    public void SetCraftObject(IslandSlotGrid slot)
    {
        GameObject obj = Instantiate(objCraft);
        obj.transform.position = slot.transform.position;
        slot.isBusy = true;
        slot.ShowIcon(false);

        ChangeGameState(GameState.GAMEPLAY);

        foreach(IslandManager im in islandManageList)
        {
            im.GameplayMode();
        }
    }
}
