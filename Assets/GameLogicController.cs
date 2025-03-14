using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject redChip;
    private GameObject RedChip;
    public GameObject yellowChip;
    private GameObject YellowChip;
    public int turn = 1;
    public string playerTurn;
    public bool roundActive;

    void RoundChecker()
    {

    }

    void ChipSpawner()
    {

    }
    
    void RedSpawn()
    {
        if (roundActive == true)
        {
            turn ++;
            RedChip = Instantiate(redChip);
        }
    }
    void YellowSpawn()
    {
        if (roundActive == true)
        {
            turn --;
            YellowChip = Instantiate(yellowChip);
            roundActive = false;
        }
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("" + hit.collider.gameObject.name);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && roundActive == false)
        {
            roundActive = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && turn == 1)
        {
            RedSpawn();
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && turn == 2)
        {
            YellowSpawn();
        }
        Debug.Log(turn);
    }
}