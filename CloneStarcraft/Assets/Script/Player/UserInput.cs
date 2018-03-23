using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;
using Assets.Script.HUD;

[RequireComponent(typeof(SelectionZone))]
public class UserInput : MonoBehaviour {

    private Player player;
    private Rect? colliderSelectionZone;
    private SelectionZone selectionZone;
    public GameObject prototype;

    private void buildSelectionZone()
    {
        if (player.hud.MouseInBounds())
        {
            colliderSelectionZone = new Rect(new Vector2(
                Input.mousePosition.x - (Screen.width * 0.05f),
                Input.mousePosition.y - (Screen.height * 0.05f)),
                new Vector2(Screen.width * 0.1f, Screen.height * 0.1f)
            );
            selectionZone.enabled = true;
            selectionZone.Zone = new Rect(
                new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y),
                new Vector2(0,0)
            );
        }
    }

    private void MouseActivity()
    {
        if (Input.GetMouseButtonDown(0)) buildSelectionZone();
        if (Input.GetMouseButtonDown(1)) RightMouseClick();

        if (Input.GetMouseButton(0))
        {
            if (colliderSelectionZone != null && ! colliderSelectionZone.Value.Contains(Input.mousePosition))
            {
                colliderSelectionZone = null;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (colliderSelectionZone != null)
            {
                colliderSelectionZone = null;
                LeftMouseClick();
            }
            else
            {
                Vector3 center = Camera.main.ScreenToViewportPoint(selectionZone.Zone.center);
                Collider[] colliders = Physics.OverlapBox(center, new Vector3(selectionZone.Zone.size.x, 1, selectionZone.Zone.y));
                if (colliders.Length == 1)
                {
                    LeftMouseClick();
                }
                else if (colliders.Length > 1)
                {
                    GameObject group = Instantiate(prototype);
                    group.transform.parent = gameObject.transform;
                    UnitGroup uGroup = group.GetComponent<UnitGroup>();
                    Debug.Log(uGroup.Group);
                    foreach (Collider c in colliders)
                    {
                        if (c.gameObject.GetComponent<Unit>() != null)
                        {
                            uGroup.Group.Add(c.gameObject.GetComponent<SteeringBehavior>());
                            Debug.Log(c.gameObject.name);
                        }
                    }
                    player.SelectedObject = uGroup;
                    uGroup.GetComponent<Unit>().SetSelection(true, player.hud.GetPlayingArea());
                }

            }
            selectionZone.enabled = false;
        }
    }

    private GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
            return hit.collider.gameObject;
        return null;
    }

    private Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
            return hit.point;
        return Vector3.negativeInfinity;
    }

    private void LeftMouseClick()
    {
        if (player.hud.MouseInBounds())
        {
            GameObject obj = FindHitObject();
            Vector3 pos = FindHitPoint();
            if (obj != null && pos != Vector3.negativeInfinity)
            {
                if (obj.name != "Terrain")
                {
                    WorldObject worldObject = obj.transform.root.GetComponent<WorldObject>();
                    if (worldObject != null)
                    {
                        if (player.SelectedObject != null)
                        {
                            player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
                            player.SelectedObject = null;
                        }
                        player.SelectedObject = worldObject;
                        worldObject.SetSelection(true, player.hud.GetPlayingArea());
                    }
                }
                else
                {
                    if (player.SelectedObject != null)
                    {
                        player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
                        player.SelectedObject = null;
                    }
                }
            }
            else
            {
                if (player.SelectedObject != null)
                {
                    player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
                    player.SelectedObject = null;
                }
            }
        }
    }

    private void RightMouseClick()
    {
        GameObject obj = FindHitObject();
        Vector3 pos = FindHitPoint();
        if (obj != null && pos != Vector3.negativeInfinity)
        {
            if (player.SelectedObject) player.SelectedObject.AlternateClick(obj, pos, player);
            Debug.Log(player.SelectedObject);
        }
    }

    private void MoveCamera()
    {
        /*******************************************
         * Camera Movement
         ******************************************/

        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //horizontal camera movement
        if (xpos >= 0 && xpos < ResourceManager.ScrollWidth)
        {
            movement.x -= ResourceManager.ScrollSpeed;
        }
        else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth)
        {
            movement.x += ResourceManager.ScrollSpeed;
        }

        //vertical camera movement
        if (ypos >= 0 && ypos < ResourceManager.ScrollWidth)
        {
            movement.z -= ResourceManager.ScrollSpeed;
        }
        else if (ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)
        {
            movement.z += ResourceManager.ScrollSpeed;
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        /*****************************************************************************
         * Camera "ZOOM"
         ****************************************************************************/

        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        if (destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        }
        else if (destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        //detect rotation amount if ALT is being held and the Right mouse button is down
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetMouseButton(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }

    // Use this for initialization
    void Start () {
        this.player = GetComponent<Player>();
        this.selectionZone = GetComponent<SelectionZone>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.human)
        {
            MoveCamera();
            RotateCamera();
            MouseActivity();
        }
	}
}
