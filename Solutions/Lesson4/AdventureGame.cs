﻿using UnityEngine;
using System.Collections;

public class AdventureGame : MonoBehaviour {
	public GameObject sword;
	public NPCManager npcManager;


	// Use this for initialization
	void Start () {
		ItemHandler.SpawnItem (sword, 0, 3, 10);
		npcManager.SpawnEnemies (10, 10, 0, 10, 10);
	}
	
	// Update is called once per frame
	void Update () {

	}
}


