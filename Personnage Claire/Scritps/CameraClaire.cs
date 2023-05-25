using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClaire : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    [SerializeField] int rotationSpeed = 3;
    [SerializeField] bool rotationCam = true;

	void Start () {
        offset = transform.position - player.transform.position;
	}

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if(rotationCam)
        {
            Quaternion turnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") *
                rotationSpeed, Vector3.up);
            offset = turnAngle * offset;

            transform.LookAt(player.transform);

        }
    }
}
