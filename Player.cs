using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody2D P_RigidBody;

	//Movement
	private bool P_FacingRight;
	private float P_Horizontal;
	private float P_Speed;
	private float P_JumpTimer;
	private bool P_Jumped;
	private bool P_Running;
	private bool P_IsGrounded;
	private bool P_IsGrounded1;
	private bool P_IsGrounded2;
	private bool P_IsGrounded3;
	private Vector2 P_LineCastPos;
	private float P_Height;
	private float P_Width;
	private bool P_Walled;
	private bool P_JumpOffWall;
	private bool P_LoseControl;
	private float P_LoseControlTimer;
	public LayerMask P_LayerMask;
	public LayerMask P_WalledMask;

	//TileMaster
	private GameObject P_TileMaster;


	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		P_RigidBody = gameObject.GetComponent<Rigidbody2D>();
		P_FacingRight = true;
		P_Speed = 2000.0f;
		P_JumpTimer = 0.7f;
		P_Jumped = false;
		P_Running = false;
		P_Height = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
		P_Width = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
		P_Walled = false;
		P_JumpOffWall = false;
		P_LoseControl = false;
		P_LoseControlTimer = 0.5f;
		P_TileMaster = GameObject.Find("Tile Master");

	}
	
	// Update is called once per frame
	void Update () {
		//3 lines to determine if the player is gounded or not.
		P_LineCastPos = gameObject.transform.position;
		P_IsGrounded1 = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f, P_LayerMask);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f);

		P_LineCastPos = new Vector2 (gameObject.transform.position.x + P_Width,gameObject.transform.position.y);
		P_IsGrounded2 = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f, P_LayerMask);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f);

		P_LineCastPos = new Vector2(gameObject.transform.position.x - P_Width, gameObject.transform.position.y);
		P_IsGrounded3 = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f, P_LayerMask);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.down * P_Height * 2.4f);

		P_LineCastPos = gameObject.transform.position;

		P_IsGrounded = P_IsGrounded1 || P_IsGrounded2 || P_IsGrounded3;

		//Check if walled 
		if (!P_IsGrounded)
		{
			//from the right
			if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f, P_WalledMask))
			{
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
			//down right
			else if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f + Vector2.down * P_Height * 1.1f, P_WalledMask))
			{
				//Debug.Log("Down Right");
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
			//up right
			else if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f + Vector2.up * P_Height * 1.1f, P_WalledMask))
			{
				//Debug.Log("Up Right");
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
			//Check if walled from the left
			else if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f, P_WalledMask))
			{
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
			//down left
			else if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f + Vector2.down * P_Height * 1.1f, P_WalledMask))
			{
				//Debug.Log("Down Left");
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
			//up left
			else if (P_Walled = Physics2D.Linecast(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f + Vector2.up * P_Height * 1.1f, P_WalledMask))
			{
				//Debug.Log("Up Right");
				P_JumpOffWall = true;
				P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -1000.0f);
			}
		}
		else
		{
			P_JumpOffWall = false;
		}
		
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f + Vector2.down * P_Height *1.1f);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * 1.1f + Vector2.up * P_Height * 1.1f);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f + Vector2.down * P_Height * 1.1f);
		Debug.DrawLine(P_LineCastPos, P_LineCastPos + Vector2.right * P_Width * -1.1f + Vector2.up * P_Height * 1.1f);

		//Player 1
		if (gameObject.tag.Equals("Player"))
		{
			P_Horizontal = Input.GetAxis("Horizontal");
		}
		//Player 2
		else
		{
			P_Horizontal = Input.GetAxis("HorizontalKeyBoard");
		}
		P_HandleMovement(P_Horizontal);
		P_Flip(P_Horizontal);

		//Debug.Log(P_IsGrounded);
		//Debug.Log(Input.GetAxisRaw("CTRL - LT"));

		//Jumping
		//Player 1
		if (Input.GetButtonDown("CTRL - A") && !P_Jumped && P_IsGrounded && gameObject.tag.Equals("Player"))
		{
			P_Jumped = true;
			P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed, 120000.0f));

		}
		//Player 2
		else if(Input.GetKeyDown(KeyCode.Space) && !P_Jumped && P_IsGrounded && gameObject.tag.Equals("Player2"))
		{
			P_Jumped = true;
			P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed, 120000.0f));
		}
		//Jump off wall player1
		if(Input.GetButtonDown("CTRL - A") && P_JumpOffWall && gameObject.tag.Equals("Player"))
		{
			//Reset Jump
			P_LoseControl = true;
			P_Jumped = true;
			P_JumpOffWall = false;
			P_JumpTimer = 0.7f;
			P_LoseControlTimer = 0.5f;
			P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed * -100.0f, 120000.0f));
		}
		//Jump off wall player2
		else if (Input.GetKeyDown(KeyCode.Space) && P_JumpOffWall && gameObject.tag.Equals("Player2"))
		{
			//Reset Jump
			P_LoseControl = true;
			P_Jumped = true;
			P_JumpOffWall = false;
			P_JumpTimer = 0.7f;
			P_LoseControlTimer = 0.5f;
			P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed * -100.0f, 120000.0f));
		}
		if (P_Jumped)
		{
			//Debug.Log("Jumped");
			
			P_JumpTimer -= Time.deltaTime;
			if(P_JumpTimer<=0.0f)
			{
				P_Jumped = false;
				P_JumpTimer = 0.7f;
			}
			
		}
		//The player loses control for 0.5 seconds after jumping off a wall
		if(P_LoseControl)
		{
			P_LoseControlTimer -= Time.deltaTime;
			if(P_LoseControlTimer<=0.0f)
			{
				P_LoseControl = false;
				P_LoseControlTimer = 0.5f;
			}
		}
		//Allow the player to fall quicker into the ground
		//Player1
		if(!P_IsGrounded)
		{
			if (Input.GetAxis("Vertical") < 0.0f && Input.GetAxis("Horizontal")==0.0f && gameObject.tag.Equals("Player"))
			{
				if (Input.GetButtonDown("CTRL - A"))
				{
					//P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed, -500000.0f));
					P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -60000.0f);
				}
			}
			//Player 2
			else if (Input.GetAxis("VerticalKeyBoard") < 0.0f && Input.GetAxis("HorizontalKeyBoard") == 0.0f && gameObject.tag.Equals("Player2"))
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					//P_RigidBody.AddForce(new Vector2(P_Horizontal * P_Speed, -500000.0f));
					P_RigidBody.velocity = new Vector2(P_RigidBody.velocity.x, -60000.0f);
				}
			}
		}

		//Running
		if(Input.GetAxisRaw("CTRL - LT")>0 && !P_Running && P_IsGrounded && gameObject.tag.Equals("Player"))
		{
	
			P_Speed *= 2.0f;			
			P_Running = true;	
			//Debug.Log(P_Speed);
		}
		else if(Input.GetAxisRaw("CTRL - LT") <= 0 && P_Running && gameObject.tag.Equals("Player"))
		{
			//Debug.Log("ThisHappened");
			P_Speed /= 2.0f;
			//Debug.Log(P_Speed);
			P_Running = false;
		}

		if (Input.GetKey(KeyCode.LeftShift) && !P_Running && P_IsGrounded && gameObject.tag.Equals("Player2"))
		{

			P_Speed *= 2.0f;
			P_Running = true;

		}
		else if (Input.GetKeyUp(KeyCode.LeftShift) && P_Running)
		{
			P_Speed /= 2.0f;
			P_Running = false;
		}
	}


	private void P_HandleMovement(float horizontal)
	{
		// Move the player body based on the horizontal value
		if (!P_LoseControl)
		{
			P_RigidBody.velocity = new Vector2(horizontal * P_Speed, P_RigidBody.velocity.y);
		}
	}

	private void P_Flip(float horizontal)
	{
		if ((horizontal > 0 && !P_FacingRight || horizontal < 0 && P_FacingRight))
		{

			//Correct the facingRight condition.
			P_FacingRight = !P_FacingRight;

			//Flip the character using the scale attribute.
			Vector3 P_Scale = transform.localScale;
			P_Scale.x *= -1;
			transform.localScale = P_Scale;
		}
	}
	//Player index determines which player is getting the tile
	public void P_GenerateTiles(int TileType, int PlayerIndex)
	{
		if(P_TileMaster)
		{
			P_TileMaster.GetComponent<TileMaster>().TM_InstaTile(PlayerIndex, TileType);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		//When a player touches their flag, it becomes their child
		if(col.gameObject.tag.Equals("FlagPlayer1")&& gameObject.tag.Equals("Player"))
		{
			col.gameObject.transform.parent = gameObject.transform;
		}
		else if(col.gameObject.tag.Equals("FlagPlayer2") && gameObject.tag.Equals("Player2"))
		{
			col.gameObject.transform.parent = gameObject.transform;
		}
	}
}

