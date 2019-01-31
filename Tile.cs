using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	protected bool T_Ability;
	protected TileMaster T_TileMaster;
	protected int T_TileNumber;
	protected bool T_TileGoKinematic;
	protected float T_TileGoKinematicTimer;
	protected bool T_Sabotaged;
	protected bool T_ContainsTileGen;


	protected void Start()
	{
		//Kinematic variables control whether the tile is kinematic or dynamic and for how much time
		T_TileGoKinematic = true;
		T_TileGoKinematicTimer = 1.0f;

		T_Sabotaged = false;

		T_ContainsTileGen = false;

		if (T_TileMaster = GameObject.Find("Tile Master").GetComponent<TileMaster>())
		{
			if (gameObject.tag.Equals("Tile1") || gameObject.tag.Equals("Tile1T") || gameObject.tag.Equals("Tile1S"))
			{
				TileMaster.TM_Tiles1.Add(this.gameObject);
				T_TileNumber = TileMaster.TM_Tiles1.Count;

				//Debug.Log("TileNumber1:  " + T_TileNumber);
			}
			else if (gameObject.tag.Equals("Tile2") || gameObject.tag.Equals("Tile2T") || gameObject.tag.Equals("Tile2S"))
			{
				TileMaster.TM_Tiles2.Add(this.gameObject);
				T_TileNumber = TileMaster.TM_Tiles2.Count;

				//Debug.Log("TileNumber2:  " + T_TileNumber);
			}
		}
	}

	private void Update()
	{
		
		if (!T_TileGoKinematic)
		{
			
			T_TileGoKinematicTimer -= Time.deltaTime;
			if (T_TileGoKinematicTimer <= 0.0f)
			{
				T_TileGoKinematicTimer = 1.0f;
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

	protected void OnCollisionEnter2D(Collision2D col)
	{
		if (T_TileGoKinematic)
		{
			if (col.gameObject.tag.Equals("Tile1") || col.gameObject.tag.Equals("Tile2") 
				|| col.gameObject.tag.Equals("Tile1T") || col.gameObject.tag.Equals("Tile2T")
				|| col.gameObject.tag.Equals("Tile1S") || col.gameObject.tag.Equals("Tile2S"))
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
				gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				//gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
				T_Ability = true;

			}
			if(gameObject.tag.Equals("Tile1") || gameObject.tag.Equals("Tile1T") || gameObject.tag.Equals("Tile1S"))
			{
				gameObject.transform.position = new Vector2(-7873.0f, gameObject.transform.position.y);
			}
			else
			{
				gameObject.transform.position = new Vector2(8130.0f, gameObject.transform.position.y);
			}

		}
	}

	protected void OnCollisionStay2D(Collision2D col)
	{
		if (T_TileGoKinematic)
		{

			if (col.gameObject.tag.Equals("Tile1") || col.gameObject.tag.Equals("Tile2")
				|| col.gameObject.tag.Equals("Tile1T") || col.gameObject.tag.Equals("Tile2T")
				|| col.gameObject.tag.Equals("Tile1S") || col.gameObject.tag.Equals("Tile2S"))
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
				gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				//gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

			}
		}
	}


	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Rocket"))
		{
			//Instantiate a tile to replace this tile
			if(gameObject.tag.Equals("Tile1") || gameObject.tag.Equals("Tile1T") || gameObject.tag.Equals("Tile1S"))
			{
				if(T_ContainsTileGen)
				{
					TileMaster.TM_NumberOfTileGens1--;
				}
				T_TileMaster.TM_InstaTileAfterDestruction(1,T_TileNumber);
			}
			else if(gameObject.tag.Equals("Tile2") || gameObject.tag.Equals("Tile2T") || gameObject.tag.Equals("Tile2S"))
			{
				if (T_ContainsTileGen)
				{
					TileMaster.TM_NumberOfTileGens2--;
				}
				T_TileMaster.TM_InstaTileAfterDestruction(2, T_TileNumber);
			}

			Destroy(gameObject);
			Destroy(col.gameObject);
		}

		else if(col.gameObject.tag.Equals("RocketS"))
		{
			T_Sabotaged = true;
			Destroy(col.gameObject);
		}
	}

	public void T_UpdateTileNumber()
	{
		T_TileGoKinematic = false;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		T_TileNumber--;
		Debug.Log("My number:  " + T_TileNumber + gameObject.GetComponent<Rigidbody2D>().isKinematic);
	}

	public int T_GetTileNumber()
	{
		return T_TileNumber;
	}

	public void T_TileGenExists()
	{
		T_ContainsTileGen = true;
	}
}
