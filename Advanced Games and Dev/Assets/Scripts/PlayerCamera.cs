using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] float cameraMovementSpeed = 120.0f;
    [SerializeField] float rotationSpeed = 7.0f;
    [SerializeField] float clampAngle = 80.0f;
    [SerializeField] float inputSensitivity = 150.0f;
    [SerializeField] GameObject cameraFollowObject;

    private float mouseX, mouseY;
    private float cameraRotationX, cameraRotationY = 0.0f;
    Vector3 followPosition, cameraRotation;

	// Use this for initialization
	void Start ()
    {
        cameraRotation = transform.localRotation.eulerAngles;
        cameraRotationX = cameraRotation.x;
        cameraRotationY = cameraRotation.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButton("Left Alt"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
           
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            cameraRotationX += mouseY * inputSensitivity * Time.deltaTime;
            cameraRotationY += mouseX * inputSensitivity * Time.deltaTime;

            cameraRotationX = Mathf.Clamp(cameraRotationX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0.0f);
            transform.rotation = localRotation;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cameraRotationX = cameraRotationY = 0.0f;
            Quaternion localRotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        Transform cameraTarget = cameraFollowObject.transform;

        float moveTowardsObject = cameraMovementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, cameraTarget.position, moveTowardsObject);
    }
}
