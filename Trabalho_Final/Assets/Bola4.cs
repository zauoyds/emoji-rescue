﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Bola4 : MonoBehaviour {

	Rigidbody rb;
	public float speed;
	public Text text;
	public Rigidbody projetil;
	Gerenciador gerenciador = Gerenciador.getInstance();
	public AudioClip recarregar;
	public AudioClip sem_municao;
	public AudioClip tiro;

	// Use this for initialization
	void Start () {		
		gerenciador.setCena (SceneManager.GetActiveScene ().buildIndex);
		gerenciador.setCarregado (false);
		gerenciador.setVidas (5);
		rb = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () {
		Vector3 v = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
		gerenciador.setPosicaoBola (v);
		if (Input.GetKeyDown ("space")) {
			if (gerenciador.isCarregado ()) {
				gerenciador.setDisparar (true);
				GetComponent<AudioSource> ().clip = tiro;
				GetComponent<AudioSource> ().Play ();
			} else {
				GetComponent<AudioSource> ().clip = recarregar;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	private void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	private void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Projetil")) {
			text.text = "Você perdeu!";
			speed = 0;
			//StartCoroutine(Whatever());
			SceneManager.LoadScene(5);
		}
		if (other.CompareTag ("WinTrigger")) {
			speed = 0;

			if (gerenciador.isZerou ()) {
				SceneManager.LoadScene (4);
			} else {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
			}
		}
		if (other.CompareTag ("Botao")) {
			if (Gerenciador.getInstance ().isLiberadoBotao1()) {
				if (!gerenciador.isCarregado()) {
					GetComponent<AudioSource> ().clip = sem_municao;
					GetComponent<AudioSource> ().Play ();
				}
				gerenciador.setCarregado (true);
				Gerenciador.getInstance ().setLiberadoBotao2 (false);
			}
		}
		if (other.CompareTag ("Botao2")) {
			if (Gerenciador.getInstance ().isLiberadoBotao2()) {
				if (!gerenciador.isCarregado()) {
					GetComponent<AudioSource> ().clip = sem_municao;
					GetComponent<AudioSource> ().Play ();
				}
				gerenciador.setCarregado (true);
				Gerenciador.getInstance ().setLiberadoBotao1 (false);
			}
		}
			
	}



	void ReloadScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	IEnumerator Whatever()
	{
		float timeToWait = 2;
		float incrementToRemove  = 0.5f;
		while(timeToWait > 0)
		{
			yield return new WaitForSeconds(incrementToRemove );
			timeToWait -= incrementToRemove;
		}
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
