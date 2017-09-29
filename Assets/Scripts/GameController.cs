using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController obj;
	public bool moveLeft, moveRight, moveDown, rotate = false;


	void Start(){
		obj = this;
	}
		

	public void OnQuit(){
		SceneManager.LoadScene("MainMenu");
	}


	public void MoveLeft(){
		Debug.Log("Left Button pressed !!!");
		moveLeft = true;
	}

	public void MoveRight(){
		Debug.Log("Right Button pressed !!!");
		moveRight = true;
	}

	public void MoveDown(){
		Debug.Log("Down Button pressed !!!");
		moveDown = true;
	}

	public void Rotate(){
		Debug.Log("Rotate Button pressed !!!");
		rotate = true;
	}
}
