using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _stickPrefeb;

    private bool _isHold = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_isHold == false)
            {
                SpawnStick();
            }
            _isHold = true;
            
        }

        if (Input.GetMouseButton(0) && _isHold)
        {
           GrowStick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isHold = false;
            
        }
    }

    private void SpawnStick()
    {
        Vector3 playerPosition = transform.position;
        playerPosition.x = transform.position.x + 0.8f;
        playerPosition.y = transform.position.y - 0.5f;

        Instantiate(_stickPrefeb,playerPosition,Quaternion.identity);
    }
    
    private void GrowStick()
    {

    }
}
