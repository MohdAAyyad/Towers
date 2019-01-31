using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenParent : MonoBehaviour {

public void TGP_DestroyTG()
	{
		Destroy(gameObject);
	}

	private void Start()
	{
		SendMessageUpwards("T_TileGenExists");
	}
}
