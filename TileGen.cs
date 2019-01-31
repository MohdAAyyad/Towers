using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGen : MonoBehaviour {

	private int TG_TileIndex;
	private int TG_TileType;
	private bool TG_Generate1;
	private bool TG_Generate2;
	private GameObject TG_Player;


	// Use this for initialization
	void Start () {
		//Booleans to check which player is accessing the TileGen
		TG_Generate1 = false;
		TG_Generate2 = false;


		TG_TileIndex = Random.Range(0, 15);

		//1: Basic Tile
		//2: Basic Tile twice
		//3: Turret Tile
		//4: Sabotage tile

		if (TG_TileIndex>=0 && TG_TileIndex<4)
		{
			//Debug.Log("Case 1");
			gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/1", typeof(Sprite));
			TG_TileType = 1;
		}
		else if(TG_TileIndex>=4 && TG_TileIndex<8)
		{
			//Debug.Log("Case 2");
			gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/2", typeof(Sprite));
			TG_TileType = 2;
		}
		else if(TG_TileIndex >= 8 && TG_TileIndex < 11)
		{
			//Debug.Log("Case 3");
			gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/Turret", typeof(Sprite));
			TG_TileType = 3;
		}
		else
		{
			//Debug.Log("Case 3");
			gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/SabotageTurret", typeof(Sprite));
			TG_TileType = 4;
		}

		
	}

	private void Update()
	{
		//Debug.Log(TG_Player.gameObject.tag);
		if (TG_Generate1)
		{
			if (Input.GetButtonDown("CTRL - X"))
			{
				//We're calling the P_Generate from inside the player's script rather than here is that there will always be 3 TileGens. This is to minimize
				//The number of "Find" statements
				TG_Player.GetComponent<Player>().P_GenerateTiles(TG_TileType, 1);
				TileMaster.TM_NumberOfTileGens1--;
				SendMessageUpwards("TGP_DestroyTG");
			}
		}
		if(TG_Generate2)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				TG_Player.GetComponent<Player>().P_GenerateTiles(TG_TileType, 2);
				TileMaster.TM_NumberOfTileGens2--;
				SendMessageUpwards("TGP_DestroyTG");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		
		if (col.gameObject.tag.Equals("Player"))
		{
			TG_Generate1 = true;
		}
		else if(col.gameObject.tag.Equals("Player2"))
		{
			TG_Generate2 = true;
		}

		TG_Player = col.gameObject;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Player"))
		{
			TG_Generate1 = false;

		}
		else if (col.gameObject.tag.Equals("Player2"))
		{
			TG_Generate2 = false;

		}

	}
}
