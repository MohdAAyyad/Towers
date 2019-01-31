/************************BUGS*******************************/

 /*[Solved - Needs more testing]If the top tile doesn't have a tilegen, and a tilegen exists somewhere in the tower, and the top tile is destroyed, a new tile will spawn with a tilegen attached
  *[Solved - Needs more testing]If a tile in the middle has a tilegen and is destroyed, a tile with no tilegen is spawned when one with a tile gen should have been spawned
  * If the player is at the edge of a left platform, P_Grounded becomes false, and walled down left gets true so if the player jumps, it jumps in the opposite direction
  * 

/**********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaster : MonoBehaviour {

	public GameObject TM_BasicTile1;
	public GameObject TM_BasicTile2;
	public GameObject TM_BasicTileTwoFloors1;
	public GameObject TM_BasicTileTwoFloors2;
	public GameObject TM_TurretTile1;
	public GameObject TM_TurretTile2;
	public GameObject TM_SabotageTile1;
	public GameObject TM_SabotageTile2;


	protected Vector2 TM_NextTilePos1;
	protected Vector2 TM_NextTilePos2;

	protected float TM_TwoFloorsTimer1;
	protected bool TM_TwoFloors1;
	protected float TM_TwoFloorsTimer2;
	protected bool TM_TwoFloors2;

	public static int TM_NumberOfTileGens1;
	public static int TM_NumberOfTileGens2;

	protected float TM_Delay1;
	protected float TM_Delay2;

	protected bool TM_InstaAfterDestruction1;
	protected bool TM_InstaAfterDestruction2;


	public static List<GameObject> TM_Tiles1;
	public static List<GameObject> TM_Tiles2;



	// Use this for initialization
	void Start () {

		TM_Tiles1 = new List<GameObject>();
		TM_Tiles2 = new List<GameObject>();

		TM_NextTilePos1 = new Vector2(-7873.0f, 4873.0f);
		TM_NextTilePos2 = new Vector2(8130.0f, 4873.0f);

		TM_TwoFloors1 = false;
		TM_TwoFloors2 = false;

		TM_TwoFloorsTimer1 = 1.0f;
		TM_TwoFloorsTimer2 = 1.0f;

		//We always start with no gens
		TM_NumberOfTileGens1 = 1;
		TM_NumberOfTileGens2 = 1;
		
		TM_InstaAfterDestruction1 = false;
		TM_InstaAfterDestruction2 = false;

		TM_Delay1 = 0.5f;
		TM_Delay2 = 0.5f;


	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("Number of TileGens 1: "  + TM_NumberOfTileGens1);
		//Debug.Log("Number of TileGens 2: " + TM_NumberOfTileGens1);

		//Add some delay between the two tiles
		if (TM_TwoFloors1)
		{
			TM_TwoFloorsTimer1 -= Time.deltaTime;

			if(TM_TwoFloorsTimer1 <= 0.0f)
			{
				TM_TwoFloors1 = false;
				TM_TwoFloorsTimer1 = 1.0f;
				Instantiate(TM_BasicTile1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
			}
		}

		if (TM_TwoFloors2)
		{
			TM_TwoFloorsTimer2 -= Time.deltaTime;
			if (TM_TwoFloorsTimer2 <= 0.0f)
			{
				TM_TwoFloors2 = false;
				TM_TwoFloorsTimer2 =1.0f;
				Instantiate(TM_BasicTile2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
			}
		}


		//Add some delay before the replacement tile is spawned.
		if(TM_InstaAfterDestruction1)
		{
			TM_Delay1 -= Time.deltaTime;

			if(TM_Delay1<=0.0f)
			{
				//Check how many TileGens there are and if there are none, instantiate a tile that contains a tilegen
				if (TM_NumberOfTileGens1 > 0)
				{
					Instantiate(TM_BasicTileTwoFloors1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
				}
				else
				{
					Instantiate(TM_BasicTile1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens1++;
				}

				TM_Delay1 = 0.5f;
				TM_InstaAfterDestruction1 = false;
			}
		}

		if (TM_InstaAfterDestruction2)
		{
			TM_Delay2 -= Time.deltaTime;

			if (TM_Delay2 <= 0.0f)
			{
				//Check how many TileGens there are and if there are none, instantiate a tile that contains a tilegen
				if (TM_NumberOfTileGens2 > 0)
				{
					Instantiate(TM_BasicTileTwoFloors2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
				}
				else
				{
					Instantiate(TM_BasicTile2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens2++;
				}

				TM_Delay2 = 0.5f;
				TM_InstaAfterDestruction2 = false;
			}
		}


	}

	public void TM_InstaTile(int TileIndex, int TileType)
	{
		if(TileIndex == 1)
		{

			switch(TileType)
			{
				//1: Basic Tile
				//2: Basic Tile twice
				//3: Turret Tile
				//4: Sabotage Tile

				case 1:
					Instantiate(TM_BasicTile1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens1++;
					break;
				case 2:
					Instantiate(TM_BasicTileTwoFloors1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens1++;
					TM_TwoFloors1 = true;

					break;
				case 3:
					Instantiate(TM_TurretTile1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens1++;
					break;
				case 4:
					Instantiate(TM_SabotageTile1, TM_NextTilePos1, Quaternion.Euler(0f, 0f, 0.0f));
					
					break;

			}

		}

		else if(TileIndex ==2)
		{
			switch (TileType)
			{
				//1: Basic Tile
				//2: Basic Tile twice
				//3: Turret Tile
				//4: Sabotage Tile
				case 1:
					Instantiate(TM_BasicTile2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens2++;
					break;
				case 2:
					Instantiate(TM_BasicTileTwoFloors2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens2++;
					TM_TwoFloors2 = true;
					break;
				case 3:
					Instantiate(TM_TurretTile2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
					TM_NumberOfTileGens2++;
					break;
				case 4:
					Instantiate(TM_SabotageTile2, TM_NextTilePos2, Quaternion.Euler(0f, 0f, 0.0f));
					
					break;

			}

		}
	}

	public void TM_InstaTileAfterDestruction(int TileIndex, int TileNumber)
	{

		Debug.Log(TileNumber);
		//See which player had his tile destroyed
		//Then see if you need to insta a basic tile with a TileGen or not based on the number of existing TileGens at that player's side

		if(TileIndex == 1)
		{
			
			//Update the tile number and turn every tile on top of the one destroyed into dynamic so that they fall.
			//They will turn back into kinematic in 1.0 second
			for (int i = 0; i < TM_Tiles1.Count; i++)
			{
				if (TM_Tiles1[i])
				{
					if (TM_Tiles1[i].GetComponent<Tile>().T_GetTileNumber() > TileNumber)
					{
						TM_Tiles1[i].GetComponent<Tile>().T_UpdateTileNumber();
					}
				}
			}
			TM_Tiles1.RemoveAt(TileNumber-1);

			TM_InstaAfterDestruction1 = true;
		}
		else if(TileIndex == 2)
		{
			for (int i = 0; i < TM_Tiles2.Count; i++)
			{
				if (TM_Tiles2[i])
				{
					if (TM_Tiles2[i].GetComponent<Tile>().T_GetTileNumber() > TileNumber)
					{
						//Debug.Log("INVOOOOOOKED BY   " + TM_Tiles2[i].GetComponent<Tile>().T_GetTileNumber());
						TM_Tiles2[i].GetComponent<Tile>().T_UpdateTileNumber();
					}
				}
			}
			TM_Tiles2.RemoveAt(TileNumber-1);

			TM_InstaAfterDestruction2 = true;

		}

	}
	//Fade in the same type of tile to the sabotaging player
	//Then call TM_InstaTileAfterDestruction for the sabotaged player

	public void TM_InstaAfterSabotage(string TileTag, int TileNumber)
	{
		if (TileTag == "Tile1")
		{
			TM_InstaTile(2, 1);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles1.Count);
			if (TileNumber >= TM_Tiles1.Count)
			{
				TM_NumberOfTileGens1--;
			}
			TM_InstaTileAfterDestruction(1, TileNumber);
		}
		else if(TileTag == "Tile1T")
		{
			TM_InstaTile(2, 3);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles1.Count);
			if (TileNumber >= TM_Tiles1.Count)
			{
				TM_NumberOfTileGens1--;
			}
			TM_InstaTileAfterDestruction(1, TileNumber);
		}
		else if (TileTag == "Tile1S")
		{
			TM_InstaTile(2, 4);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles1.Count);
			if (TileNumber >= TM_Tiles1.Count)
			{
				TM_NumberOfTileGens1--;
			}
			TM_InstaTileAfterDestruction(1, TileNumber);
		}
		else if (TileTag == "Tile2")
		{
			TM_InstaTile(1, 1);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles2.Count);
			if (TileNumber >= TM_Tiles2.Count)
			{
				TM_NumberOfTileGens2--;
			}
			TM_InstaTileAfterDestruction(2, TileNumber);
		}
		else if (TileTag == "Tile2T")
		{
			TM_InstaTile(1, 3);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles2.Count);
			if (TileNumber >= TM_Tiles2.Count)
			{
				TM_NumberOfTileGens2--;
			}
			TM_InstaTileAfterDestruction(2, TileNumber);
		}
		else if (TileTag == "Tile2S")
		{
			TM_InstaTile(1, 4);
			Debug.Log("TileNumber:   " + TileNumber);
			Debug.Log("TileNumber:   " + TM_Tiles2.Count);
			if (TileNumber >= TM_Tiles2.Count)
			{
				TM_NumberOfTileGens2--;
			}
			TM_InstaTileAfterDestruction(2, TileNumber);
		}

		
	}
}
