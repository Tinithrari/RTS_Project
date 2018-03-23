using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class HUD : MonoBehaviour {

    public GUISkin resourceSkin;
    public GUISkin ordersSkin;
    public GUISkin selectBoxSkin;

    private const int ORDERS_BAR_WIDTH = 150;
    private const int RESOURCE_BAR_HEIGHT = 40;
    private const int SELECTION_NAME_HEIGHT = 20;

    private Player player;

    public bool MouseInBounds()
    {
        Vector3 mousePos = Input.mousePosition;
        return (mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH) 
            && (mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT);
    }

    private void DrawOrdersBar()
    {
        GUI.skin = ordersSkin; // Get the orders skin
        GUI.BeginGroup(new Rect(Screen.width - ORDERS_BAR_WIDTH, RESOURCE_BAR_HEIGHT, 
            ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT)); // Define a new coordinate system

        GUI.Box(new Rect(0, 0, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT), ""); // Draw the box
        GUI.EndGroup();
    }

    private void DrawResourcesBar()
    {
        // Same code sample as upside
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT), "");
        string selectionName = "";
        if (player.SelectedObject)
        {
            selectionName = player.SelectedObject.objectName;
        }
        if (!selectionName.Equals(""))
        {
            GUI.Label(new Rect(0, 0, ORDERS_BAR_WIDTH, SELECTION_NAME_HEIGHT), selectionName);
        }
        GUI.EndGroup();
    }

    // Use this for initialization
    void Start () {
        this.player = transform.root.GetComponent<Player>();
        ResourceManager.StoreSelectBoxSkin(selectBoxSkin);
	}
	
	// Update is called once per frame
	void OnGUI () {
        if (player && player.human)
        {
            DrawOrdersBar();
            DrawResourcesBar();
        }
    }

    public Rect GetPlayingArea()
    {
        return new Rect(0, RESOURCE_BAR_HEIGHT, Screen.width - ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT);
    }
}
