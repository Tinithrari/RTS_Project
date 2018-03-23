using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitGroup : Unit{
    public List<SteeringBehavior> Group { get; private set; }

    private SteeringBehavior principal;

    private static readonly string BASE_NAME = "Group of {0} unit";

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    protected override void DrawSelectionBox(Rect selectBox)
    {
        
    }

    private SteeringBehavior getSlowestEntity()
    {
        float minSpeed = float.MaxValue;
        SteeringBehavior min = null;

        foreach (SteeringBehavior behavior in Group)
        {
            if (behavior.MaxSpeed < minSpeed)
            {
                minSpeed = behavior.MaxSpeed;
                min = behavior;
            }
        }

        return min;
    }

    public override void AlternateClick(GameObject obj, Vector3 position, Player player)
    {
        
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    protected override void Update()
    {
        base.Update();
        objectName = String.Format(BASE_NAME, Group.Count);
    }

    public override void MouseClick(GameObject obj, Vector3 position, Player player)
    {
        base.MouseClick(obj, position, player);
        if (currentlySelected)
            Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        Group = new List<SteeringBehavior>();
    }
}
