using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;
 
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {


	[SerializeField] float moveStopRadius= 0.02f;

	private bool isInDirectMode = false;
	private CameraRaycaster targeting;
	private ThirdPersonCharacter m_character;
	private Vector3 Destination;

	// Use this for initialization
	void Start () {
		targeting = Camera.main.GetComponent<CameraRaycaster>();
		m_character = GetComponent<ThirdPersonCharacter>();
		Destination = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.G)) {//G for GAMEPAD
			isInDirectMode = !isInDirectMode;
			Destination = transform.position;
		}

		if (isInDirectMode) {
			processDirectMovement();
		}

		processMouseMovement ();
	}

	private void processDirectMovement ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		// calculate camera relative direction to move:
          Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
          Vector3 m_Move = v*m_CamForward + h*Camera.main.transform.right;

          m_character.Move(m_Move,false,false);
	}

	private void processMouseMovement ()
	{
		if (Input.GetMouseButtonDown (0)) {
			switch (targeting.layerHit) {
			case Layer.Walkable:
				Destination = targeting.Hit.point;
				break;
			case Layer.Enemy:
				print ("shooting");
				break;
			default:
				return;
			}
		}
		// to stop character from rotating on self
		if (Destination.magnitude >= moveStopRadius) {
			m_character.Move (Destination - transform.position, false, false);
		}
		else {
			m_character.Move (Vector3.zero, false, false);
		}
	}
}
