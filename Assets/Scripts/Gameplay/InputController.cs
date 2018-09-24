using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	bool leftHeld = false;
	bool rightHeld = false;

	[SerializeField] PlayerController player;

	void Start ()
	{
		player = (PlayerController)FindObjectOfType(typeof(PlayerController));
	}
	
	void Update ()
	{
		if (leftHeld) player.Move("left");
		if (rightHeld) player.Move("right");
	}

	public void Press(string button)
	{
		print("Pressed button");
		if (button == "left")
		{
			rightHeld = false;
			leftHeld = true;
		}
		else if (button == "right")
		{
			leftHeld = false;
			rightHeld = true;
		}
	}

	public void Release(string button)
	{
		if (button == "left")
		{
			leftHeld = false;
		}
		else if (button == "right")
		{
			rightHeld = false;
		}
	}
}
