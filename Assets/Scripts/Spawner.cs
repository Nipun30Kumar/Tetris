using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] TetrisBlocks;
    public GameObject currentBlock;
    public GameObject nextBlock;
    public static Spawner Instance;

    void Awake(){

        Instance = this;

    }

    void Start () {

        SpawnFirstBlock();
        SpawnNextBlock();

	}

    //This function will spawn the initial block which won't be visible in the next window.
	public GameObject SpawnFirstBlock(){
		int index = Random.Range(0,TetrisBlocks.Length);
        currentBlock = Instantiate(TetrisBlocks[index], transform.position, Quaternion.identity);
        return currentBlock;
	}

    //This function will spawn the next block in the next window.
	public GameObject SpawnNextBlock(){
		int index = Random.Range(0,TetrisBlocks.Length);
		nextBlock = Instantiate(TetrisBlocks[index], new Vector2(12f,19f), Quaternion.identity);
        nextBlock.transform.localScale += new Vector3(-0.5f, -0.5f,0);
        return nextBlock;
	}

}
