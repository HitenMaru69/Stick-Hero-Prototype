using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _stickPrefeb;
    [SerializeField] float _growSpeed;
    [SerializeField] Transform _spawnpoint;
    [SerializeField] GameObject _groundPrefeb;
    [SerializeField] GameObject _player;

    private bool _isHold = false;
    private GameObject _newObj;
    private GameObject _lastSpawnObj;
    private bool _isReleasing = false;
    private float _rotationProgress = 0f;
    private bool _isMoveGround = false;
    private GameObject _moveObject;

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
            RealeaseStick();
            
        }

        RotateStick();

        if (_isMoveGround) { 
        
            MoveGround();
        }

        if (Input.GetKeyDown(KeyCode.Space)) { SpawnGround(); }
    }

    private void SpawnStick()
    {
        Vector3 playerPosition = _player.transform.position;
        playerPosition.x = _player.transform.position.x + 0.7f;
        playerPosition.y = _player.transform.position.y - 0.9f;

        _newObj =Instantiate(_stickPrefeb,playerPosition,Quaternion.identity);
    }
    
    private void GrowStick()
    {
        _newObj.transform.localScale = new Vector3(_newObj.transform.localScale.x,
            _newObj.transform.localScale.y + _growSpeed * Time.deltaTime,
            _newObj.transform.localScale.z);

    }

    private void SpawnGround()
    {
        _lastSpawnObj = Instantiate(_groundPrefeb,_spawnpoint.position, Quaternion.identity);
        //Vector3 newPos = _spawnpoint.position;
        //newPos.x = newPos.x + 3;
        //_spawnpoint.position = newPos;
        _isMoveGround = true;
    }

    private void RealeaseStick()
    {
        _isReleasing = true;
        _rotationProgress = 0f;
    }

    private void RotateStick()
    {
        if (_isReleasing && _newObj != null)
        {
            _rotationProgress += Time.deltaTime * 3f; 

            Quaternion targetRotation = Quaternion.Euler(0, 0, -90);
            _newObj.transform.rotation = Quaternion.Lerp(Quaternion.identity, targetRotation, _rotationProgress);

            if (_rotationProgress >= 1f)
            {
                _isReleasing = false;
                _rotationProgress = 1f;
            }

        }
    }

    private void MoveGround()
    {
        _moveObject = _lastSpawnObj;
        Vector3 groundpos = _moveObject.transform.position;
        groundpos.x = _player.transform.position.x;

        _moveObject.transform.position = Vector3.Lerp(_moveObject.transform.position,groundpos,0.1f);

        if (Mathf.Abs(_moveObject.transform.position.x - groundpos.x) < 0.01f)
        {
            _moveObject.transform.position = groundpos;
            _isMoveGround = false;
        }
    }
}
