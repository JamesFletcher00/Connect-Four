using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject redChip;
    private GameObject RedChip;
    public GameObject yellowChip;
    private GameObject YellowChip;
    public Transform A7Spawn;
    public Transform B7Spawn;
    public Transform C7Spawn;
    public Transform D7Spawn;
    public Transform E7Spawn;
    public Transform F7Spawn;
    public Transform G7Spawn;
    public int turn = 1;
    public string playerTurn;
    public bool roundActive;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("" + hit.collider.gameObject.name);
                Transform spawnPoint = SpawnOnGrid(hit.collider.tag);

                if (spawnPoint != null)
                {
                    SpawnItem(spawnPoint.position);
                }
            }
        }
    }
    void RoundChecker()
    {

    }

    void SpawnOnGrid(string tag)
    {
        switch(tag)
        {
            case "A":
                return A7spawn; break;
            case "B":
                return B7spawn; break;
            case "C":
                return C7spawn; break;
            case "D":
                return D7spawn; break;
            case "E":
                return E7spawn; break;
            case "F":
                return F7spawn; break;
            case G:
                return G7spawn; break;
            default:
                return null;

        }
    }
    void ChipSpawner()
    {
        if (roundActive == true)
            if (turn == 1)
            {
                turn ++;
                RedChip = Instantiate(redChip, position, Quarternion.identity);
            }
            if (turn == 2)
            {
                turn --;
                YellowChip = Instantiate(yellowChip, position, Quarternion.identity);
            }
    }

}