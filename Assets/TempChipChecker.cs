using UnityEngine;

public class TempChipChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chip entered trigger: " + gameObject.name);
    }

}
