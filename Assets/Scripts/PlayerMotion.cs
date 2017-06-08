using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour {

	//Declare physical variables - 

	private Vector2 planetCoordinates;

	private Vector2 playerCoordinates;

	private Vector2 displacement;
	private float displacement_mag;

	private Vector2 velocity;

	private Vector2 acceleration;
	private float acceleration_mag;

	private Vector2 accel_total;

	// Use this for initialization
	void Start () {

		//Grab initial velocity of player after release
		GameObject player = GameObject.Find("Player");
		StartInput scriptStartInput = player.GetComponent<StartInput> ();
		velocity.x = scriptStartInput.velX;
		velocity.y = scriptStartInput.velY;

	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (transform.position.x) > 30 || Mathf.Abs (transform.position.y) > 20) {
			Application.LoadLevel (0);
		}

		accel_total = new Vector2 (0,0);
		Debug.Log ("PlayerMotion update");

		//Calculate individual accelerations from any present planets
		GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
		foreach (GameObject p in planets) {

			PlanetMass scriptPlanetMass = p.GetComponent<PlanetMass> ();

			//Determine displacement vector
			planetCoordinates = p.transform.position;
			Debug.Log ("planetCoordinates: "+planetCoordinates);
			playerCoordinates = transform.position;
			Debug.Log ("playerCoordinates: "+playerCoordinates);
			displacement = planetCoordinates - playerCoordinates;
			Debug.Log ("displacement: "+displacement);
			displacement_mag = displacement.magnitude;
			Debug.Log ("displacement_mag: " + displacement_mag);

			//Determine acceleration
			acceleration_mag = scriptPlanetMass.planetMass / Mathf.Pow(displacement_mag, 2);
			Debug.Log (acceleration_mag);
			acceleration.x = acceleration_mag * displacement.x / displacement_mag;
			acceleration.y = acceleration_mag * displacement.y / displacement_mag;
			Debug.Log ("acceleration.x: "+acceleration.x);
			Debug.Log ("acceleration.y: "+acceleration.y);

			accel_total += acceleration;
			Debug.Log ("accel_total: "+accel_total);
		}


		//Calculate new velocity
		velocity.x += accel_total.x;
		velocity.y += accel_total.y;
		Debug.Log (velocity.x);
		Debug.Log (velocity.y);

		//Rotate player object
		transform.eulerAngles = new Vector3 (0,0, Mathf.Rad2Deg*Mathf.Atan2(velocity.y,velocity.x));

		//Move to new position given calculated velocity
		transform.position = new Vector2 (transform.position.x + velocity.x, transform.position.y + velocity.y);


		GameObject trail = GameObject.Find ("Trail");
		Instantiate(trail, transform.position, new Quaternion());
	}
}
