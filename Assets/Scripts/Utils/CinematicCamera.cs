using UnityEngine;
using System.Collections;

public class CinematicCamera : MonoBehaviour
{
	CharacterController camera;

	Vector3 moveDirection = Vector3.zero;
	private float turner;

	void Start()
	{
		camera = GetComponent<CharacterController>();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update ()
	{
		// Movement X Z
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		// Speed handling
		moveDirection = transform.TransformDirection(moveDirection) * 5.0f * (Input.GetKey(KeyCode.LeftControl) ? 2.0f : 1.0f);

		// Rotation X
		turner += Input.GetAxis ("Mouse X") / 8.0f;
		if(turner != 0)
			transform.eulerAngles += new Vector3 (0,turner,0);

		// Movement Y
		moveDirection.y = Input.GetKey(KeyCode.Space) ? 1.0f : (Input.GetKey(KeyCode.LeftShift) ? -1.0f : 0.0f);

		// Application of the movement
		camera.Move(moveDirection * Time.deltaTime);
	}
}
