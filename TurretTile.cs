using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTile : Tile {
	public GameObject TT_Rocket;
	public GameObject TT_Rocket2;
	private GameObject TT_RocketInsta;
	private Vector2 TT_RocketPos;
	private Vector2 TT_RocketForce;
	private bool TT_RocketWasShot;


	private void Awake()
	{
		//This boolean is used to prevent the tile from shooting more than once (in the scenario where the tiles below it get destroyed)
		TT_RocketWasShot = false;
	}

	
	// Update is called once per frame
	void Update () {

		if(T_Ability && !TT_RocketWasShot)
		{
			if(gameObject.tag.Equals("Tile1T")|| gameObject.tag.Equals("Tile1S"))
			{
				//Instantiate Rocket
				TT_RocketPos = new Vector2(gameObject.transform.position.x + 5283.08f, gameObject.transform.position.y + 160.371f);
				TT_RocketInsta = (GameObject) Instantiate(TT_Rocket, TT_RocketPos, Quaternion.Euler(0f, 0f, 0.0f));
				TT_RocketForce = new Vector2(1200000.0f, 0.0f);
				TT_RocketInsta.GetComponent<Rigidbody2D>().AddForce(TT_RocketForce);
				T_Ability = false;
				TT_RocketWasShot = true;

			}
			else if (gameObject.tag.Equals("Tile2T")|| gameObject.tag.Equals("Tile2S"))
			{
				//Instantiate Rocket
				TT_RocketPos = new Vector2(gameObject.transform.position.x - 5283.08f, gameObject.transform.position.y + 160.371f); //658.985f);
				TT_RocketInsta = (GameObject)Instantiate(TT_Rocket2, TT_RocketPos, Quaternion.Euler(0f, 0f, 0.0f));
				TT_RocketForce = new Vector2(-1200000.0f, 0.0f);
				TT_RocketInsta.GetComponent<Rigidbody2D>().AddForce(TT_RocketForce);
				T_Ability = false;
				TT_RocketWasShot = true;
			}
		}

		if(!T_TileGoKinematic)
		{
			T_TileGoKinematicTimer -= Time.deltaTime;
			if(T_TileGoKinematicTimer<=0.0f)
			{
				T_TileGoKinematicTimer = 2.0f;
				T_TileGoKinematic = true;
			}
		}

		if (T_Sabotaged)
		{
			Debug.Log("Alright, insta after sabotage!");
			T_TileMaster.TM_InstaAfterSabotage(gameObject.tag, T_TileNumber);
			Destroy(gameObject);
		}

	}
}