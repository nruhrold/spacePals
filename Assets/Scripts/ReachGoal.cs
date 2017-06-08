using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReachGoal : MonoBehaviour {

	void Start () { 

	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Player") {
			//GameObject text2 = GameObject.Find ("Text2");
			//Text t = text2.GetComponent<Text>();
			//t.enabled = true;
		}
	}
}
