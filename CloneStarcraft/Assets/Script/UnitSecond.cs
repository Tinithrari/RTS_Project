using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSecond : MonoBehaviour{
    /*
    bool selected;
    Color[] colors;

    void Start()
    {
        selected = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                selected = hit.collider.gameObject == gameObject;
                var selectedComponent = transform.Find("Selected");
                selectedComponent.gameObject.SetActive(selected);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (selected)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    Navigate(hit.point);
                    var lookAt = new Vector3(hit.point.x, 0, hit.point.z);
                    gameObject.transform.LookAt(lookAt);
                    //GetComponent<SeekBehavior>().Velocity = new Vector3();
                }
            }
        }
        if (Input.GetMouseButton(0) && FreeCameraControl.selectionZone.HasValue && ((Rect)FreeCameraControl.selectionZone).size != Vector2.zero)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            position.y = Screen.height - position.y;
            if (FreeCameraControl.selectionZone.HasValue && ((Rect)FreeCameraControl.selectionZone)
                .Contains(position, true))
            {
                selected = true;
                var selectedComponent = transform.Find("Selected");
                selectedComponent.gameObject.SetActive(selected);
                //FreeCameraControl.unitGroup.addUnit(this);
            }
            else
            {
                selected = false;
                var selectedComponent = transform.Find("Selected");
                selectedComponent.gameObject.SetActive(selected);
               /* if (FreeCameraControl.unitGroup.units.Contains(this))
                    FreeCameraControl.unitGroup.units.Remove(this);
            }
        }
        
    }

    public void RemoveJoint()
    {
        SpringJoint[] joints = gameObject.GetComponents<SpringJoint>();
        foreach (var joint in joints)
            Destroy(joint);
    }

    public void Navigate(Vector3 destination)
    {
        //agent.destination = destination;
        Actions a = gameObject.GetComponent<Actions>();
        a.Run();
       // GetComponent<SeekBehavior>().Target = destination;
    }
*/
}
