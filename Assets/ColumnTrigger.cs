using UnityEngine;
using System.Collections.Generic;

public class ColumnTrigger : MonoBehaviour
{
    public string columnLetter; // Example: "A", "B", etc.
    public List<Transform> rowPositions = new List<Transform>(); // Assign row positions in order (A1 to A7)
    private HashSet<Transform> occupiedSlots = new HashSet<Transform>(); // Track filled slots

private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Chip"))
    {
        Transform targetPosition = GetAvailableSlot();
        if (targetPosition != null)
        {
            StopChip(other.gameObject, targetPosition);
        }
    }
}


    Transform GetAvailableSlot()
    {
        for (int i = 0; i < rowPositions.Count; i++)
        {
            if (!occupiedSlots.Contains(rowPositions[i])) // Check if slot is free
            {
                return rowPositions[i]; // Return first available slot
            }
        }
        return null; // No available slots
    }

    void StopChip(GameObject chip, Transform targetSlot)
    {
        Rigidbody rb = chip.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;  // Stop falling immediately
            rb.useGravity = false;       // Disable gravity so it stays in place
            rb.isKinematic = true;       // Disable physics movement
        }

        chip.transform.position = targetSlot.position; // Snap chip to final position
        occupiedSlots.Add(targetSlot); // Mark slot as occupied

        // Disable the collider to prevent re-triggering
        Collider chipCollider = chip.GetComponent<Collider>();
        if (chipCollider != null)
        {
            chipCollider.enabled = false;
        }
    }
}
