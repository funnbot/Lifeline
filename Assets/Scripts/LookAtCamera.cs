using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	void Update() {
		var cam = Camera.main.transform;
		var cameraPlane = new Plane(cam.forward, cam.position);
		var point = cameraPlane.ClosestPointOnPlane(transform.position);
		transform.LookAt(point);
	}
}