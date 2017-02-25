using UnityEngine;
using System.Collections;

public class TempCamera : MonoBehaviour {
	
	void MouseListener()
	{
		if (Input.GetAxis("Mouse X") < 0)
		{
			transform.Rotate(new Vector3(0, -5f, 0));
		}
		else if (Input.GetAxis("Mouse X") > 0)
		{
			transform.Rotate(new Vector3(0, 5f, 0));
		}

		Cursor.lockState = CursorLockMode.Locked;
	}

	void KeyboardListener()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			transform.position -= transform.rotation * new Vector3(.1f, 0, 0);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.position += transform.rotation * new Vector3(.1f, 0, 0);
		}

		if (Input.GetKey(KeyCode.Z))
		{
			transform.position += transform.rotation * new Vector3(0, 0, .1f);
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.rotation * new Vector3(0, 0, .1f);
		}

		if (Input.GetKey(KeyCode.Space))
		{
			transform.position += transform.rotation * new Vector3(0, .1f, 0);
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			transform.position -= transform.rotation * new Vector3(0, .1f, 0);
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(new Vector3(0, -10f, 0));
		}

		if (Input.GetKey(KeyCode.E))
		{
			transform.Rotate(new Vector3(0, 10f, 0));
		}
	}

	void Update()
	{
		KeyboardListener();
		MouseListener ();
	}
}
