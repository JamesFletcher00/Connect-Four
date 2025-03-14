using UnityEngine;

public class GameLogicController : MonoBehaviour
{
    public GameObject redChip;
    private GameObject RedChip;
    public GameObject yellowChip;
    private GameObject YellowChip;
    public Transform A7Spawn, B7Spawn, C7Spawn, D7Spawn, E7Spawn, F7Spawn, G7Spawn;
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
                    if (turn == 1)
                    {
                        RedSpawn(spawnPoint.position);
                    }
                    if (turn == 2)
                    {
                        YellowSpawn(spawnPoint.position);
                    }
                }
                
            }
        }
    }

    Transform SpawnOnGrid(string tag)
    {
        if(tag == "A") return A7Spawn;
        if(tag == "B") return B7Spawn;
        if(tag == "C") return C7Spawn;
        if(tag == "D") return D7Spawn;
        if(tag == "E") return E7Spawn;
        if(tag =="F") return F7Spawn;
        if(tag == "G") return G7Spawn;

        return null;
    }

    void RedSpawn(Vector3 position)
    {            
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        if (roundActive == true)
        {
            turn ++;
            RedChip = Instantiate(redChip, position, rotation);
        }
    }

    void YellowSpawn(Vector3 position)
    {
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        if(roundActive == true)
        {
            turn --;
            YellowChip = Instantiate(yellowChip, position, rotation);        
        }
    }
}