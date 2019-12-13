using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccelerationControlScript : MonoBehaviour {

	// References to controlled game objects that will fall
	// when you shake the mobile device hard enough
	public GameObject dishes1, dishes2, dishes3, dishes4;

	// variable to hold shaking acceleration value
	Vector3 accelerationDir;

	// Update is called once per frame
	void Update () {
		
		// Read new acceleration Input from mobile device
		accelerationDir = Input.acceleration;

		// If you shake the mobile device hard enough
		// (accelerations Square Magnitude greater then 5 for example)
		if (accelerationDir.sqrMagnitude >= 5f) {

			// then dishes fall off the shelves getting
			// RigidBodies isKinematic option as false (become Dynamic)
			dishes1.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			dishes2.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			dishes3.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
			dishes4.gameObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		}
	}

	// Public method to reset the scene by pressing Reset UI button
	public void ResetScene()
	{
		SceneManager.LoadScene ("Scene01");
	}
}
