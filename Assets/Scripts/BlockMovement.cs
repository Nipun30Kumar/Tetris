using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMovement : MonoBehaviour {


	public float lastSpawnTime = 0f;
    private Spawner block;
    private GameObject current, next;

    void Start(){

        block = Spawner.Instance;
        current = block.currentBlock;
        next = block.nextBlock;

    }

	// Update is called once per frame
	void Update () {
        

		if(GameController.obj.moveLeft == true){

            MoveLeft();
		}

		if(GameController.obj.moveRight == true){

            MoveRight();
		}

		if(GameController.obj.rotate == true){

            Rotate();
		}

		if(GameController.obj.moveDown == true){
            current.transform.position += new Vector3(0,-0.3F,0);
            Debug.Log("Block moved down");

            if (ValidPositionGrid(current.transform)){
				UpdateGridSystem();
			}else{
                
                current.transform.position += new Vector3(0,1,0);
				GridSystem.DeleteWholeRows();
                
                next.transform.position = new Vector3(4f, 18f, 0);
                current = block.nextBlock;
                current.transform.localScale = new Vector3(1f,1f,0);

                //next = FindObjectOfType<Spawner>().SpawnNextBlock();
                next = block.SpawnNextBlock();
                Debug.Log("Update spawn called !!!!!");

                enabled = false;

			}
		}
				
	}

	public void LateUpdate(){
		MoveDown();
		GameController.obj.moveLeft = false;
		GameController.obj.moveRight = false;
		GameController.obj.moveDown = false;
		GameController.obj.rotate = false;
	}



    public void MoveLeft()
    {
        current.transform.position += new Vector3(-1, 0, 0);
        Debug.Log("Block moved left");

        if (ValidPositionGrid(current.transform))
        {
            UpdateGridSystem();

        }
        else
        {
            current.transform.position += new Vector3(1, 0, 0);
        }
    }

    public void MoveRight()
    {
        current.transform.position += new Vector3(1, 0, 0);
        Debug.Log("Block moved right");

        if (ValidPositionGrid(current.transform))
        {
            UpdateGridSystem();
        }
        else
        {
            current.transform.position += new Vector3(-1, 0, 0);
        }
    }

    public void Rotate()
    {
        current.transform.Rotate(new Vector3(0, 0, -90));
        Debug.Log("Block rotated");

        if (ValidPositionGrid(current.transform))
        {
            UpdateGridSystem();
        }
        else
        {
            current.transform.Rotate(new Vector3(0, 0, 90));
        }
    }

	public void MoveDown(){
		if(Time.time - lastSpawnTime >= 1){

            current.transform.position += new Vector3(0,-0.3f,0);
            //Debug.Log("Block moved down by timer");
            if (ValidPositionGrid(current.transform)){
					UpdateGridSystem();
				}else{
                    
                    current.transform.position += new Vector3(0,1,0);
					GridSystem.DeleteWholeRows();

                    current = next;
                    next = null;
                                  
                    current.transform.position = new Vector3(3f, 19f, 0);
                    
                    current.transform.localScale = new Vector3(1f, 1f, 0);

                    next = block.SpawnNextBlock();
                    Debug.Log("MoveDown spawn called !!!!!");

                enabled = false;
				}

				lastSpawnTime = Time.time;
			}
	}

	public bool ValidPositionGrid(Transform x){
		foreach(Transform child in x.transform){
			Vector2 v = GridSystem.RoundVector(child.position);

			if(!GridSystem.IsInsideBorder(v)){
                Debug.Log("Child position : " + v);
                Debug.LogError("!!!!!!! Returning false in validposition !!!!!!");
                return false;
			}
			//Debug.Log("Array Lenght : " + GridSystem.grid.Length);
			if(GridSystem.grid[(int)v.x,(int)v.y] != null && GridSystem.grid[(int)v.x,(int)v.y].parent != x.transform){
				Debug.LogError("!!!!!!! Returning false in validposition !!!!!!");
				return false;

			}
		}
        Debug.Log("!!!!!! Returning true in validposition !!!!!!");
		return true;
	}


	public void UpdateGridSystem(){
		for(int y = 0; y < GridSystem.column ; ++y){
			for(int x = 0; x < GridSystem.row ; ++x){
				if(GridSystem.grid[x,y] != null){
					if(GridSystem.grid[x,y].parent == current.transform){
						GridSystem.grid[x,y] = null;
					}
				}
			}
		}

		foreach(Transform child in current.transform){
			Vector2 v = GridSystem.RoundVector(child.position);
			GridSystem.grid[(int)v.x,(int)v.y] = child;
		}
	}
}
