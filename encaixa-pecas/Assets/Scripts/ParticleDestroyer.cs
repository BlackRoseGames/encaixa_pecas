using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour {

	private ParticleSystem ps;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, GetComponent<ParticleSystem>().main.duration); 
	}
}
