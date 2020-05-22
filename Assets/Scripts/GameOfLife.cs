using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour {
    public Vector2Int Board_Size;
    public GameObject[, ] Board;
    public int[, ] Next_Board;
    public GameObject Prefab_Square;
    private float TimerMax = 0.2f;
    private float Timer;

    // Start is called before the first frame update
    void Start() {
        Timer = TimerMax;
        Board = new GameObject[Board_Size.x, Board_Size.y];
        Next_Board = new int[Board_Size.x, Board_Size.y];
        for (int i = 0; i < Board_Size.x; i++) {
            for (int j = 0; j < Board_Size.y; j++) {
                Vector3 Position = new Vector3(i-Board_Size.x/2f, j-Board_Size.y/2f, 0);
                Board[i,j] = Instantiate(Prefab_Square, Position, Quaternion.identity);
                Board[i,j].GetComponent<DNA>().State = RandomState();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if(Timer >= TimerMax) {
            for (int i = 0; i < Board_Size.x; i++) {
                for (int j = 0; j < Board_Size.y; j++) {
                    int num_Heigh =  CheackStateOfNeighbor(i,j);
                    if(num_Heigh == 3 && Board[i,j].GetComponent<DNA>().State == 0) {
                        Next_Board[i,j] = 1;
                    } else if((num_Heigh < 2 || num_Heigh > 3) && Board[i,j].GetComponent<DNA>().State == 1) {
                        Next_Board[i,j] = 0;
                    } else if((num_Heigh == 2 || num_Heigh == 3) && Board[i,j].GetComponent<DNA>().State == 1) {
                        Next_Board[i,j] = 1;
                    }
                }
            }
            Timer = 0;
            UpdateBoard();
        }
        Timer += Time.deltaTime;
    }

    void UpdateBoard() {
        for (int i = 0; i < Board_Size.x; i++) {
            for (int j = 0; j < Board_Size.y; j++) {
                Board[i,j].GetComponent<DNA>().State = Next_Board[i,j];
            }
        }
    }

    int RandomState() {
        int ran_Num = Random.Range(0, 10);
        if(ran_Num < 5){
            return 0;
        } else {
            return 1;
        }
    }

    int CheackStateOfNeighbor(int i, int j) {
        int num_Heigh = Board[(i - 1 + Board_Size.x)%Board_Size.x, (j - 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i + Board_Size.x)%Board_Size.x, (j - 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j - 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i - 1 + Board_Size.x)%Board_Size.x, (j + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i - 1 + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Heigh = num_Heigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        return num_Heigh;
    }
}