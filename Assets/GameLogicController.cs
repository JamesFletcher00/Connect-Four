using UnityEngine;

public class GameLogicController : MonoBehaviour
{

    public Rigidbody c4Red;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Rigidbody clone;
            clone = Instanstiate(c4Red, transform.position, transform.rotation);
        }
    }
}