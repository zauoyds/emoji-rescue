﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	private void OnTriggerEnter(Collider other){
		//if (other.tag != "Projetil") {
			Destroy (gameObject);
		//}
	}
}
