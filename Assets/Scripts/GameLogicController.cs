using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogicController : MonoBehaviour
{
    public GameObject redChip;
    public GameObject yellowChip;
    private GameObject chip;
    public Transform A1, A2, A3, A4, A5, A6;
    public Transform B1, B2, B3, B4, B5, B6;
    public Transform C1, C2, C3, C4, C5, C6;
    public Transform D1, D2, D3, D4, D5, D6;
    public Transform E1, E2, E3, E4, E5, E6;
    public Transform F1, F2, F3, F4, F5, F6;
    public Transform G1, G2, G3, G4, G5, G6;

    public Transform A7Spawn, B7Spawn, C7Spawn, D7Spawn, E7Spawn, F7Spawn, G7Spawn;
    public bool redTurn = true;
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
                string columnTag = hit.collider.tag;
                Transform spawnPoint = SpawnOnGrid(columnTag);

                if (spawnPoint != null)
                {
                    ChipSpawner(columnTag);

                }
                
            }
        }
    }

    private Dictionary<string, int> columnHeights = new Dictionary<string, int>
    {
        {"A", 0},{"B", 0},{"C", 0}, {"D", 0},{"E", 0}, {"F", 0},{"G", 0}
    };

    Transform GetNextAvailableSlot(string columnTag)
    {
        if (!columnHeights.ContainsKey(columnTag)) return null;

        int rowIndex = columnHeights[columnTag];
        if (rowIndex >= 6) return null;

        return GetGridPosition(columnTag, rowIndex);
    }

    Transform GetGridPosition(string columnTag, int rowIndex)
{
    // Replace these with actual row transforms
    Dictionary<string, Transform[]> columnPositions = new Dictionary<string, Transform[]>
    {
        {"A", new Transform[] { A1, A2, A3, A4, A5, A6 }},
        {"B", new Transform[] { B1, B2, B3, B4, B5, B6 }},
        {"C", new Transform[] { C1, C2, C3, C4, C5, C6 }},
        {"D", new Transform[] { D1, D2, D3, D4, D5, D6 }},
        {"E", new Transform[] { E1, E2, E3, E4, E5, E6 }},
        {"F", new Transform[] { F1, F2, F3, F4, F5, F6 }},
        {"G", new Transform[] { G1, G2, G3, G4, G5, G6 }}
    };

    return columnPositions[columnTag][rowIndex]; // Return the correct Transform
}

    Transform SpawnOnGrid(string tag)
    {
        if(tag == "A") return A7Spawn;
        if(tag == "B") return B7Spawn;
        if(tag == "C") return C7Spawn;
        if(tag == "D") return D7Spawn;
        if(tag == "E") return E7Spawn;
        if(tag == "F") return F7Spawn;
        if(tag == "G") return G7Spawn;

        return null;
    }

    void ChipSpawner(string columnTag)
    {        
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        Transform spawnPoint = SpawnOnGrid(columnTag);

        Transform targetSlot = GetNextAvailableSlot(columnTag);

        if (targetSlot == null) return; // No available slot, column is full

        if (redTurn && roundActive)
        {
            redTurn = false;
            chip = Instantiate(redChip, spawnPoint.position, rotation);            
        }
        else if (!redTurn && roundActive)
        {
            redTurn = true;
            chip = Instantiate(yellowChip, spawnPoint.position, rotation); 
        }
        else
        {
            return;
        }

        StartCoroutine(ChipMovement(chip, targetSlot.position, columnTag));

    }

    IEnumerator ChipMovement(GameObject chip, Vector3 targetPosition, string columnTag)
    {
    float speed = 5f; // Adjust for slower/faster fall
    while (chip.transform.position.y > targetPosition.y)
    {
        chip.transform.position = Vector3.MoveTowards(
            chip.transform.position, targetPosition, speed * Time.deltaTime);
        yield return null;
    }

    chip.transform.position = targetPosition; // Ensure precise stop
    columnHeights[columnTag]++;
    }


}