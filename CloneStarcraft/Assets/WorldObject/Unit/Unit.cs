using RTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : WorldObject
{
    public override void MouseClick(GameObject obj, Vector3 position, Player player)
    {
        base.MouseClick(obj, position, player);
    }

    public override void AlternateClick(GameObject obj, Vector3 position, Player player)
    {
        base.AlternateClick(obj, position, player);
        if (obj.name == "Terrain")
        {
            GetComponent<SteeringBehavior>().Destination = position;
            GetComponent<Actions>().Run();
        }
    }

    public override void PerformAction(string actionToPerform)
    {
        base.PerformAction(actionToPerform);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    protected override void Update()
    {
        base.Update();
    }
}
