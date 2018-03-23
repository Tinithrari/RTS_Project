using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RTS;

public class WorldObject : MonoBehaviour {

    public string objectName;
    public Texture2D buildImage;

    public int cost;
    public int sellValue;
    public int hitPoints;
    public int maxHitPoints;

    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    protected Bounds boundsSelection;
    protected Rect playingArea;

    protected virtual void Awake()
    {
        this.boundsSelection = ResourceManager.InvalidBounds;
        CalculateBounds();
    }

    public virtual void AlternateClick(GameObject obj, Vector3 position, Player player)
    {

    }

    public void CalculateBounds()
    {
        this.boundsSelection = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            this.boundsSelection.Encapsulate(r.bounds);
        }
    }

    private void DrawSelection()
    {
        GUI.skin = ResourceManager.SelectBoxSkin;
        Rect selectBox = WorkHelper.CalculateSelectionBox(boundsSelection, playingArea);
        //Draw the selection box around the currently selected object, within the bounds of the playing area
        GUI.BeginGroup(playingArea);
        DrawSelectionBox(selectBox);
        GUI.EndGroup();
    }

    protected virtual void DrawSelectionBox(Rect selectBox)
    {
        GUI.Box(selectBox, "");
    }

    // Use this for initialization
    void Start ()
    {
        player = transform.root.GetComponent<Player>();
        this.playingArea = new Rect(0, 0, 0, 0);
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
		
	}

    protected virtual void OnGUI()
    {
        if (currentlySelected) DrawSelection();
    }

    public virtual void MouseClick(GameObject obj, Vector3 position, Player player)
    {
        //only handle input if currently selected
        if (currentlySelected && obj && obj.name != "Ground")
        {
            WorldObject worldObject = obj.transform.root.GetComponent<WorldObject>();
            //clicked on another selectable object
            if (worldObject) ChangeSelection(worldObject, player);
        }
    }

    private void ChangeSelection(WorldObject obj, Player player)
    {
        SetSelection(false, playingArea);
        if (player.SelectedObject) player.SelectedObject.SetSelection(false, playingArea);
        player.SelectedObject = obj;
        obj.SetSelection(true, playingArea);
    }

    public void SetSelection(bool selected, Rect playingArea)
    {
        currentlySelected = selected;
        if (selected) this.playingArea = playingArea;
    }

    public string[] GetActions()
    {
        return actions;
    }

    public virtual void PerformAction(string actionToPerform)
    {
        //it is up to children with specific actions to determine what to do with each of those actions
    }
}
