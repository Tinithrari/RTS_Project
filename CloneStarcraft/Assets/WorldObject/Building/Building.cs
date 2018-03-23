using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : WorldObject
{ 
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
