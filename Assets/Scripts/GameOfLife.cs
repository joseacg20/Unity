using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public Vector2Int Board_Size;
    public GameObject[, ] Board;
    public GameObject Prefab_Square;

    // Start is called before the first frame update
    void Start()
    {
        Board = new GameObject[Board_Size.x, Board_Size.y];
        for (int i = 0; i < Board_Size.x; i++) {
            for (int j = 0; j < Board_Size.y; j++) {
                Vector3 Position = new Vector3(i = Board_Size.x/2, j = Board_Size.y/2, 0);
                Board[i,j] = Instantiate(Prefab_Square, Position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
