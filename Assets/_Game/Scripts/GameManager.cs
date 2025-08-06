using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _stickPrefeb;
    [SerializeField] float _growSpeed;

    private bool _isHold = false;
    private GameObject _newObj;

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

        _newObj =Instantiate(_stickPrefeb,playerPosition,Quaternion.identity);
    }
    
    private void GrowStick()
    {
        _newObj.transform.localScale = new Vector3(_newObj.transform.localScale.x,
            _newObj.transform.localScale.y + _growSpeed * Time.deltaTime,
            _newObj.transform.localScale.z);

    }
}
