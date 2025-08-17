using UnityEngine;

public class Stick : MonoBehaviour
{
    private const string PLATEFORM = "PlateForm";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLATEFORM))
        {
            Debug.Log("Yes");
        }
    }

}
