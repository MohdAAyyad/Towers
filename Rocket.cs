using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {


	public void R_DestroySelf()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Tile1")|| col.gameObject.tag.Equals("Tile2") 
			|| col.gameObject.tag.Equals("Tile1T") || col.gameObject.tag.Equals("Tile2T")
			|| col.gameObject.tag.Equals("Tile1S") || col.gameObject.tag.Equals("Tile2S"))
		{
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}
}
