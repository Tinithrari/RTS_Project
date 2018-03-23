﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string username;
    public bool human;
    public HUD hud;
    public WorldObject SelectedObject { get; set; }

	// Use this for initialization
	void Start () {
        this.hud = transform.GetChild(0).GetComponent<HUD>(); // Get the player HUD
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
