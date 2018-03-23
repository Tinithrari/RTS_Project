using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraControl : MonoBehaviour {
    /*
    public GameObject Ankle;
    public float ZoomAcceleration;
    public float ZoomVelocity;
    public float Acceleration;
    public float Velocity;
    private static readonly float BOUND = 60;

    public static UnitGroup unitGroup;
    public static Rect? selectionZone;
    private Vector2 ? origin;
    private Texture2D texture;
    private readonly Color selectionColor = new Color(1f, 0.88f, 0.54f, 0.5f);
    private Color opaqueColor;
    private bool isBuildingSelection;

    private Vector3 oldPosition;

    void Start () {
        oldPosition = gameObject.transform.position;
        selectionZone = null;
        isBuildingSelection = false;
        texture = new Texture2D(1,1);
        texture.SetPixel(0, 0, selectionColor);
        texture.Apply();
        opaqueColor = new Color(selectionColor.r, selectionColor.g, selectionColor.b);
	}

    void moveCamera()
    {
        if (Input.mousePosition.x >= Screen.width - BOUND)
            gameObject.transform.Translate(new Vector3(Acceleration * Velocity, 0), Space.World);
        if (Input.mousePosition.x <= BOUND)
            gameObject.transform.Translate(new Vector3(-Acceleration * Velocity, 0), Space.World);
        if (Input.mousePosition.y <= BOUND)
            gameObject.transform.Translate(new Vector3(0, 0, -Acceleration * Velocity), Space.World);
        if (Input.mousePosition.y >= Screen.height - BOUND)
            gameObject.transform.Translate(new Vector3(0, 0, Acceleration * Velocity), Space.World);

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Camera cam = (Camera)gameObject.GetComponent("Camera");
            cam.fieldOfView = (cam.fieldOfView >= 60 ? 60 : cam.fieldOfView + ZoomAcceleration * ZoomVelocity);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Camera cam = (Camera)gameObject.GetComponent("Camera");
            cam.fieldOfView = (cam.fieldOfView <= 20 ? 20 : cam.fieldOfView - ZoomAcceleration * ZoomVelocity);
        }

        RaycastHit hit;

        if (! Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100))
            gameObject.transform.position = oldPosition;
        oldPosition = gameObject.transform.position;
    }

    void drawSelectCube()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBuildingSelection = true;
            unitGroup = new UnitGroup();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectionZone = null;
            origin = null;
            isBuildingSelection = false;
            if (unitGroup.units.Count == 0)
                unitGroup = null;
        }

        if (isBuildingSelection)
        {
            if (!selectionZone.HasValue)
            {
                origin = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                selectionZone = new Rect((Vector2)origin, new Vector2(0, 0));
            }
            else
            {
                Vector2 size = new Vector2(Input.mousePosition.x - ((Vector2)origin).x,
                    (Screen.height - Input.mousePosition.y) - ((Vector2)origin).y);
                selectionZone = new Rect((Vector2)origin, size);
            }
        }
    }

    void RightClick()
    {
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && unitGroup != null)
            {
                unitGroup.Navigate(hit.point);
            }
        }
    }

    void OnGUI()
    {
        if (isBuildingSelection)
        {
            GUI.color = opaqueColor;
            GUI.DrawTexture((Rect)selectionZone, texture);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        moveCamera();
        drawSelectCube();
        RightClick();
    }*/
}
