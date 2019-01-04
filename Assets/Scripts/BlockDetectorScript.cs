using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class BlockDetectorScript : MonoBehaviour {

	public float angle;
	public float strength; //linear

	public float distance;

	public float raio;
	public float maisPerto = 80;


	void Start () {
		strength = 0;
		angle = 90; //cubes 360/4 sensores = 90
		raio = 2f;
	}

	// 1x por frame -> fixed
	void FixedUpdate () {
		
		strength = 0;
		maisPerto = 80;

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, raio);

		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag == "Block") {
				
				float halfAngle = angle / 2.0f;
				Vector3 toVector = (hitColliders[i].transform.position - transform.position);
				Vector3 forward = transform.forward;
				toVector.y = 0;
				forward.y = 0;
				float angleToTarget = Vector3.Angle (forward, toVector);

				// linha desenhada da deteçao de cubos
				Debug.DrawLine (transform.position, hitColliders[i].transform.position, Color.red);

				if (angleToTarget <= halfAngle) {
					distance = Vector3.Distance (hitColliders[i].transform.position, transform.position);
					if (distance < maisPerto) { //atualiza distancia minima
						maisPerto = distance;
					}
				}
			}

			else if (hitColliders [i].tag == "Wall") {

				float halfAngle = angle / 0.2f;
				Vector3 toVector = (hitColliders [i].transform.position - transform.position);
				Vector3 forward = transform.forward;
				toVector.y = 0;
				forward.y = 0;
				float angleToTarget = Vector3.Angle (forward, toVector);

				// linha desenhada da deteçao de paredes
				Debug.DrawLine (transform.position, hitColliders[i].transform.position, Color.blue);

				if (angleToTarget <= halfAngle) {
					distance = Vector3.Distance (hitColliders[i].transform.position, transform.position);
					if (distance < maisPerto) { //atualiza distancia minima
						maisPerto = distance;
					}
				}
			}
		}

		if (maisPerto >= 80) {
			strength = 0; //nao faz nada
		} else {
			strength = 0.3f / Mathf.Pow (maisPerto, 2) + 0.40f;
		}
	}

	// Get linear output value
	public float GetLinearOutput()
	{
		return strength;
	}

}
