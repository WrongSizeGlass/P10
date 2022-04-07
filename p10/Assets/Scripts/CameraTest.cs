using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
	float mouseX;
	float mouseY;
	public float mouseSensitivity = 1;
	float xAxisClamp;
	float yAxisClamp;
	public float maxRot = 45;
	public float minRot = -45;
	[SerializeField] private Transform player;
	// Start is called before the first frame update
	void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{

		rotateCamra();
		//	unlockMouse();
		///	rb.velocity = playermodelRb.velocity;
		//transform.position = myPos- differencePos;//new Vector3(myPos.x - differencePos.x, myPos.y - differencePos.y, myPos.z-differencePos.z);

	}
	public Vector3 getTargetRotationBody() {
		return targetRotationBody;
	}
	Vector3 targetRotationCamra;
	Vector3 targetRotationBody;
	//This method does that when the mouse turns the character body rotates with the camra
	void rotateCamra()
	{

		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");

		float rotAmountX = mouseX * mouseSensitivity;
		float rotAmountY = mouseY * mouseSensitivity;

		xAxisClamp -= rotAmountY;
		yAxisClamp -= rotAmountX;

		targetRotationCamra = transform.rotation.eulerAngles;
		targetRotationBody = player.rotation.eulerAngles;
		//Vector3 targetRotationPrefab = playermodelRb.rotation.eulerAngles;

		targetRotationCamra.x -= rotAmountY;//invert the input = -=
		//targetRotationCamra.y -= rotAmountX;//invert the input = -=
		targetRotationBody.y += rotAmountX;
		//targetRotationBody.y += rotAmountX; //rotates the body
		//targetRotationBody.x += rotAmountY; //rotates the body
		targetRotationCamra.z = 0; // no cam flip
		targetRotationCamra.z = 0;
		//locks the camra rotation's  x coordinat between -90 and 90 degrees 
		// look at the 3D camera degress
		if (xAxisClamp > maxRot)
		{
			xAxisClamp = maxRot;
			targetRotationCamra.x = maxRot;
			//targetRotationCamra.y = maxRot;

		}
		else if (xAxisClamp < minRot)
		{

			xAxisClamp = minRot;
			targetRotationCamra.x = minRot;
			//targetRotationCamra.y = minRot;
		}
		

		
		//rb.MoveRotation(Quaternion.Euler(targetRotationBody));
		//targetRotationBody.y = targetRotationBody.y;
		
		//playermodelRb.MoveRotation(Quaternion.Euler(targetRotationCamra));
		 
		 
		 


		//targetRotationCamra.x = -5;
		transform.rotation = Quaternion.Euler(targetRotationCamra);
		//targetRotationBody = targetRotationCamra);
		//deltaRotation = Quaternion.Euler(targetRotationBody * Time.deltaTime);
		//rb.rotation = Quaternion.Euler(targetRotationBody);

		//rb.MoveRotation(Quaternion.Euler(targetRotationBody));
		//targetRotationBody.y = targetRotationBody.y;

		//playermodelRb.MoveRotation(Quaternion.Euler(targetRotationCamra));



	}
}
