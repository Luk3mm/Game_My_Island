using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CheckRecipeIsReady : MonoBehaviour
{
    public Button button;
    public Craft recipe;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    public void CheckRecipe()
    {
        bool isReady = CoreGame.instance.gameManager.recipes.First(x => x.recipe == recipe).isReady;
        button.interactable = isReady;
    }
}
