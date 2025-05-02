using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gamestate;
    public float interactionDistance;
    public float timeToDelete;
    public int playerLife;
    public int playerLifeMax;
    public int playerEnergy;
    public int playerEnergyMax;

    public GameObject actionCursor;

    private GameObject interactiveObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        }
    }
}
