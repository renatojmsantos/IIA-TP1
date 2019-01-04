using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorScript : MonoBehaviour {

	public float angle;
	private bool useAngle = true;

	public float strength; //linear
	public int numObjects;

	public float invertido;

	// calculo gaussiana
	public float g;
	public float media;
	public float d_padrao;

	void Start () {
		strength = 0; //linear
		invertido = 0;
		g = 0;

		numObjects = 0;

		if (angle >= 360) {
			useAngle = false;
		}
	}

	// 1x por frame
	void FixedUpdate () {
		GameObject[] lights;

		if (useAngle) {
			lights = GetVisibleLights (); // todas as luzes dentro do angulo do sensor
		} else {
			lights = GetAllLights ();
		}

		strength = 0;
		numObjects = lights.Length;
	
		foreach (GameObject light in lights) {
			// linha desenhada da deteçao de luzes
			Debug.DrawLine (transform.position, light.transform.position, Color.green);

			float r = light.GetComponent<Light> ().range;
			//strength += 1.0f / ((transform.position - light.transform.position).sqrMagnitude / r + 1);
			strength += 1.0f / Mathf.Pow((transform.position - light.transform.position).magnitude / r + 1,2);
		}

		if (numObjects > 0) {
			strength = strength / numObjects;
		}

	}

	// Get linear output value
	public float GetLinearOutput()
	{
		return strength;
	}

	public float GetInvertedLinearOutput()
	{
		invertido = 1 - strength;
		return invertido;
	}

	// Get gaussian output value
	public virtual float GetGaussianOutput()
	{
		//valores enunciado
		media = 0.5f;
		d_padrao = 0.12f;
		g = Mathf.Exp (-((Mathf.Pow(strength-media, 2)) / (2 * Mathf.Pow (d_padrao, 2))));
		return g;
	}
		

	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllLights()
	{
		return GameObject.FindGameObjectsWithTag ("Light");
	}

	// Returns all "Light" tagged objects that are within the view angle of the Sensor. 
	// Only considers the angle over the y axis. Does not consider objects blocking the view.
	GameObject[] GetVisibleLights()
	{
		ArrayList visibleLights = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Light");

		foreach (GameObject light in lights) {
			Vector3 toVector = (light.transform.position - transform.position);
			Vector3 forward = transform.forward;
			toVector.y = 0;
			forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			if (angleToTarget <= halfAngle) {
				visibleLights.Add (light);
			}
		}

		return (GameObject[])visibleLights.ToArray(typeof(GameObject));
	}
}
