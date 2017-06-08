using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInput : MonoBehaviour {

	public float scaleFactor;

	public float accelX;
	public float accelY;

	public float velX;
	public float velY;

	private float x;
	private float y;

	private float initialPositionX;
	private float initialPositionY;

	private GameObject playerClone;

	void Start () {
		velX = 0;
		velY = 0;
		transform.rotation = new Quaternion ();

		initialPositionX = transform.position.x; 
		initialPositionY = transform.position.y;

	}

	// Update is called once per frame
	void Update () {
		x = Input.mousePosition.x;
		y = Input.mousePosition.y;
	}

	void OnMouseDrag () {
		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 10.0f));
		transform.eulerAngles = new Vector3 (0,0, Mathf.Rad2Deg*Mathf.Atan2(initialPositionY - transform.position.y, initialPositionX - transform.position.x));
	}

	void OnMouseUp () {
		velX = scaleFactor * (initialPositionX - transform.position.x);
		velY = scaleFactor * (initialPositionY - transform.position.y);

		PlayerMotion scriptPlayerMotion = GetComponent<PlayerMotion> ();
		scriptPlayerMotion.enabled = true;

		/*GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
		foreach (GameObject p in planets) {
			PlanetGravity planetGravity = p.GetComponent<PlanetGravity> ();
			planetGravity.enabled = true;
		}*/
	}
}
