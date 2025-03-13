using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject redChip;
    private GameObject RedChip;
    public GameObject yellowChip;
    private GameObject YellowChip;
    public int turn = 1;
    public string playerTurn;

    void TurnChecker()
    {
        
    }
    
    void RedSpawn()
    {
        turn ++;
        RedChip = Instantiate(redChip);
    }
    void YellowSpawn()
    {
        turn = turn - 2;
        YellowChip = Instantiate(yellowChip);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && turn == 1)
        {
            turn++;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && turn == 2)
        {
            RedSpawn();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && turn == 3)
        {
            YellowSpawn();
        }
        Debug.Log(turn);
    }
}