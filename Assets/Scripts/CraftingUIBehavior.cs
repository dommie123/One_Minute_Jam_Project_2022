using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIBehavior : MonoBehaviour
{
    public Button btnExit;
    public GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        btnExit.onClick.AddListener(ExitCraftingPanel);
    }

    private void ExitCraftingPanel()
    {
        this.gameObject.SetActive(false);
        hud.SetActive(true);
    }
}
