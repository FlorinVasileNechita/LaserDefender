﻿using UnityEngine;
using System.Collections;

public class GameConstants : MonoBehaviour {

	//Random
	public static Random rand = new Random();


	//Controlling the player object speeds
	public static float PLAYER_MOVEMENT_SPEED = 10f;
	public static float LASER_MOVEMENT_SPEED = 10f;

	//Controlling the rate at which the laser fires.
	public static float FIRE_DELAY = 0.25f;


	//Enemy Movement
	public static float ENEMY_HORZ_SPEED = 5f;
	public static float ENEMY_VERT_SPEED = 0.5f;
	public static float ENEMY_SPAWN_RATE = 1f;

	//Enemy Firing
	public static float ENEMY_FIRE_RATE = 1f;

	public static int NUM_OF_FORMATIONS = 2;


	//Formation Widths
	public static float PENTAGON_WIDTH = 3.0f;
	public static float V_WIDTH = 5.0f;

}
