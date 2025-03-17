using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameLogicController : MonoBehaviour
{
    private bool isChipFalling = false;
    public GameObject redChip;
    public GameObject yellowChip;
    private GameObject chip;
    public GameObject arrow;
    private GameObject playerArrow;
    public Transform A1, A2, A3, A4, A5, A6;
    public Transform B1, B2, B3, B4, B5, B6;
    public Transform C1, C2, C3, C4, C5, C6;
    public Transform D1, D2, D3, D4, D5, D6;
    public Transform E1, E2, E3, E4, E5, E6;
    public Transform F1, F2, F3, F4, F5, F6;
    public Transform G1, G2, G3, G4, G5, G6;

    public Transform A7Spawn, B7Spawn, C7Spawn, D7Spawn, E7Spawn, F7Spawn, G7Spawn;
    public bool redTurn = true;
    public TMP_Text playerTurn;
    public bool roundActive;

    public TMP_Text RedScore;
    public TMP_Text YellowScore;
    public int redScore;
    public int yellowScore;
    public GameObject playAgainButton;

    void Update()
    {
        if(redTurn && roundActive){
            playerTurn.text = "Red's Turn!";
            playerTurn.color = Color.red;
        }else if (!redTurn && roundActive){
            playerTurn.text = "Yellow's Turn!";
            playerTurn.color = Color.yellow;
        }
        RedScore.text = redScore.ToString();
        YellowScore.text = yellowScore.ToString();

        if(isChipFalling) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            string columnTag = hit.collider.tag;
            Transform spawnPoint = SpawnOnGrid(columnTag);
            Debug.Log(hit.collider.gameObject.name);

            if (columnPositions.ContainsKey(columnTag))
            {
                PlayerArrow(columnTag); // Call PlayerArrow function
            }
            else
            {
                arrowIndicator.SetActive(false);
            }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (spawnPoint != null && columnHeights[columnTag] < 6)
            {
                ChipSpawner(columnTag);
                isChipFalling = true;

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
    private int[,] gridState = new int[7, 6]; // 7 columns (A-G), 6 rows (1-6)

    public GameObject arrowIndicator; // Assign in Inspector
    private Dictionary<string, Vector3> columnPositions = new Dictionary<string, Vector3>();

    void Start()
    {
        // Set predefined positions for the arrow above each column
        columnPositions["A"] = A7Spawn.position + Vector3.up * 1.5f;
        columnPositions["B"] = B7Spawn.position + Vector3.up * 1.5f;
        columnPositions["C"] = C7Spawn.position + Vector3.up * 1.5f;
        columnPositions["D"] = D7Spawn.position + Vector3.up * 1.5f;
        columnPositions["E"] = E7Spawn.position + Vector3.up * 1.5f;
        columnPositions["F"] = F7Spawn.position + Vector3.up * 1.5f;
        columnPositions["G"] = G7Spawn.position + Vector3.up * 1.5f;

        arrowIndicator.SetActive(false); // Hide initially
        playAgainButton.SetActive(false);
    }

    void PlayerArrow(string columnTag)
    {
        if (columnPositions.ContainsKey(columnTag))
        {
            arrowIndicator.transform.position = columnPositions[columnTag];
            arrowIndicator.SetActive(true);
        }
        else
        {
            arrowIndicator.SetActive(false);
        }
    }


    void ChipSpawner(string columnTag)
    {   
        if (isChipFalling) return; 
          
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        Transform spawnPoint = SpawnOnGrid(columnTag);

        Transform targetSlot = GetNextAvailableSlot(columnTag);
        int columnIndex = columnTag[0] - 'A';
        int rowIndex = columnHeights[columnTag]; 

        if (targetSlot == null) return; // No available slot, column is full

        if (redTurn && roundActive)
        {
            redTurn = false;
            chip = Instantiate(redChip, spawnPoint.position, rotation);   
            gridState[columnIndex, rowIndex] = 1;      //stores red chip in grid     
        }
        else if (!redTurn && roundActive)
        {
            redTurn = true;
            chip = Instantiate(yellowChip, spawnPoint.position, rotation);
            gridState[columnIndex, rowIndex] = -1;  //stores yellow chip in grid
        }
        else
        {
            return;
        }
        isChipFalling = true;
        StartCoroutine(ChipMovement(chip, targetSlot.position, columnTag, columnIndex, rowIndex));

    }

    IEnumerator ChipMovement(GameObject chip, Vector3 targetPosition, string columnTag, int col, int row)
    {
        float speed = 5f; // Adjust for slower/faster fall
        while (chip.transform.position.y > targetPosition.y)
        {
            chip.transform.position = Vector3.MoveTowards(
                chip.transform.position, targetPosition, (speed*2) * Time.deltaTime);
            yield return null;
        }

        chip.transform.position = targetPosition; // Ensure precise stop
        columnHeights[columnTag]++;

        isChipFalling = false;

        if (CheckForWin(col, row))
        {
            roundActive = false;

            if (gridState[col, row] == 1)
            {
                RedWin();
            }
            else
            {
                YellowWin();
            }        
            }
    }

    public void RestartGame()
    {
    // Reset the grid state
        for (int col = 0; col < 7; col++)
        {
            for (int row = 0; row < 6; row++)
            {
                gridState[col, row] = 0;
            }
        }

        // Destroy all chips from the previous round
        GameObject[] chips = GameObject.FindGameObjectsWithTag("Chip");
        foreach (GameObject chip in chips)
        {
            Destroy(chip);
        }      

        redTurn = true;
        roundActive = true;
        playAgainButton.SetActive(false);  
    }
    void RedWin()
    {
        redScore++; // Increase Red's score
        playerTurn.text = "Game Over! Red Wins!";
        playerTurn.color = Color.red;
        playAgainButton.SetActive(true);
    }
    void YellowWin()
    {
        yellowScore++; // Increase Yellow's score
        playerTurn.text = "Game Over! Yellow Wins!";
        playerTurn.color = Color.yellow;
        playAgainButton.SetActive(true);
    }
    bool CheckForWin(int col, int row)
    {
        int player = gridState[col, row];
        if (player == 0) return false; // Empty slot, no win possible

        return CheckDirection(col, row, 1, 0, player) // Horizontal
            || CheckDirection(col, row, 0, 1, player) // Vertical
            || CheckDirection(col, row, 1, 1, player) // Diagonal \
            || CheckDirection(col, row, 1, -1, player); // Diagonal /
    }
    bool CheckDirection(int col, int row, int colDir, int rowDir, int player)
    {
        int count = 1; // Include the placed chip

        // Check in the positive direction
        for (int i = 1; i < 4; i++)
        {
            int checkCol = col + i * colDir;
            int checkRow = row + i * rowDir;
            if (checkCol < 0 || checkCol >= 7 || checkRow < 0 || checkRow >= 6) break;
            if (gridState[checkCol, checkRow] == player) count++;
            else break;
        }

        // Check in the negative direction
        for (int i = 1; i < 4; i++)
        {
            int checkCol = col - i * colDir;
            int checkRow = row - i * rowDir;
            if (checkCol < 0 || checkCol >= 7 || checkRow < 0 || checkRow >= 6) break;
            if (gridState[checkCol, checkRow] == player) count++;
            else break;
        }

        return count >= 4;
    }

}