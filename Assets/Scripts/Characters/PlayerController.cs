using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isFistPerson = false;

	Transform catModel, centerPoint, playerCam;

	CharacterController playerController;
    Animator playerAnimator;

	float rotX, rotY;

	float Sensitivity = 10f;

	float zoom = -10;

    Vector3 translateCamera = new Vector3(0, 6, -6);
    float rotationSpeed = 10f;

	float moveFB, moveLR;

	float verticalVelocity;
    float jumpDist = 30f;
    float gravityMultiplier = 10f;

	Character player;

    bool hasJumped;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if(isFistPerson)
        {
            zoom = 0;
            translateCamera = new Vector3(0, 0, 0);
        }

		catModel = transform.Find("CatModel").transform;
		centerPoint = transform.Find("CatCameraPlacement").transform;
		playerCam = transform.Find("CatCameraPlacement/CatCamera").transform;

		player = (Character)GetComponent(typeof(Character));
		playerController = GetComponent<CharacterController>();
        playerAnimator = catModel.gameObject.GetComponent<Animator>();

        centerPoint.localPosition = new Vector3(centerPoint.transform.localPosition.x + translateCamera.x, centerPoint.transform.localPosition.y + translateCamera.y, centerPoint.transform.localPosition.z + translateCamera.z);
    }

    void Update()
    {
        // Order is important for animation /!\
		UpdateMovement();
		UpdateJump();
		UpdateAttack();

        ApplyGravity();
    }

    void UpdateAttack()
    {
        if(player.isMovingAttack)
            playerAnimator.SetInteger("State", 4);
    }

    private float walkSpeedMultiplier = 7.6f, runMSpeedMultiplier = 15f;

    void UpdateMovement()
    {
        //Mouse Zoom Input
        /*if(zoom < zoomMin)
        {
			zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
			if (zoom > zoomMin)
				zoom = zoomMin;
			if (zoom < zoomMax)
				zoom = zoomMax;
        }*/

		//Mouse Camera Input
		playerCam.transform.localPosition = new Vector3(0, 0, zoom);

		//Mouse Rotation
		rotX += Input.GetAxis("Mouse X") * Sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * Sensitivity;

		//Clamp Camera
		rotY = Mathf.Clamp(rotY, zoom >= 0 ? -60f : -10f, 60f);
		playerCam.LookAt(centerPoint);
		centerPoint.localRotation = Quaternion.Euler(rotY, 0, 0);
        playerController.gameObject.transform.rotation = Quaternion.Euler(0, rotX, 0);

        //Movement Speed
        moveFB = 0;
        moveLR = 0;

        if (player.isMovingBackward)
        {
            moveFB = -walkSpeedMultiplier;
            playerAnimator.SetInteger("State", 5); // Walk Backward
        }
		else if (player.isMovingForward)
        {
            if(player.isMovingSprint) // Is sprinting
            {
				moveFB = runMSpeedMultiplier;
				playerAnimator.SetInteger("State", 2); // Run
            }
            else
            {
				moveFB = walkSpeedMultiplier;
				playerAnimator.SetInteger("State", 1); // Walk
            }
        }
        else
        {
            playerAnimator.SetInteger("State", 0); // Idle
        }

		if (player.isMovingLeft)
        {
			if (player.isMovingSprint) // Is sprinting
			{
				moveLR = -runMSpeedMultiplier;
				playerAnimator.SetInteger("State", 2); // Run
			}
			else
			{
				moveLR = -walkSpeedMultiplier;
                playerAnimator.SetInteger("State", player.isMovingBackward ? 5 : 1); // Walk Backward or Forward
			}
        }
		else if (player.isMovingRight)
        {
			if (player.isMovingSprint) // Is sprinting
			{
				moveLR = runMSpeedMultiplier;
				playerAnimator.SetInteger("State", 2); // Run
			}
			else
			{
				moveLR = walkSpeedMultiplier;
                playerAnimator.SetInteger("State", player.isMovingBackward ? 5 : 1); // Walk Backward or Forward
			}
		}

		// Apply speed
		moveFB *= player.Speed;
		moveLR *= player.Speed;

		//Movement Direction
		Vector3 movement = new Vector3(moveLR, verticalVelocity, moveFB);

		//Movement Rotation
		movement = transform.rotation * movement;

		playerController.Move(movement * Time.deltaTime);

		//centerPoint.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		//Movement Input
		Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotationSpeed);
    }

    void UpdateJump()
    {
        if(player.isMovingJump)
        {
            playerAnimator.SetInteger("State", 3);
            hasJumped = true;
        }
    }

    void ApplyGravity()
    {
        if(playerController.isGrounded)
        {
            verticalVelocity = hasJumped ? jumpDist : Physics.gravity.y * gravityMultiplier;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
            verticalVelocity = Mathf.Clamp(verticalVelocity, -50f, jumpDist);
            hasJumped = false;
        }
    }
}
