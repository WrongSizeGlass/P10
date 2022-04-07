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

		targetRotationCamra.x -= rotAmountY;//invert the input = -=
		targetRotationBody.y += rotAmountX;
		targetRotationCamra.z = 0; // no cam flip
		targetRotationCamra.z = 0;

		if (xAxisClamp > maxRot){
			xAxisClamp = maxRot;
			targetRotationCamra.x = maxRot;

		}else if (xAxisClamp < minRot){
			xAxisClamp = minRot;
			targetRotationCamra.x = minRot;
		}
		
		transform.rotation = Quaternion.Euler(targetRotationCamra);

	}
}
