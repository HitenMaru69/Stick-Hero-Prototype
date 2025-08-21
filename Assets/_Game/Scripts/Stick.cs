using UnityEngine;

public class Stick : MonoBehaviour
{
    private const string PLATEFORM = "PlateForm";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(PLATEFORM))
        {
            GameManager.Instance._isPlayeeCanMove = true;

        }

    }

}
