using UnityEngine;
using System.Collections;

public class Thirdpersoncamera : Photon.MonoBehaviour {
    public Transform lookat;
    public Transform camTransform;
    private Camera cam;
    private float distance= 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;

	// Use this for initialization
	void Start () {
        camTransform = transform;
        cam = Camera.main;
	}
	private void LateUpdate()
    {
        Vector2 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        camTransform.position = lookat.position + rotation * dir;
        camTransform.LookAt(lookat.position);

    }
	// Update is called once per frame
	void Update () {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
    }
}
