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
    private bool _isPlayeeCanMove = false;
    private bool _isOnetime = true;
    private GameObject _moveObject;
    private bool _shouldCameraFollow = false;


    private void Start()
    {
        SpawnGround();
    }

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
        PlayerMove();


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

            Vector3 newPos = _spawnpoint.position;
            newPos.x = newPos.x + 3;
            _spawnpoint.position = newPos;
     
        
        
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
                _isPlayeeCanMove = true;
                _rotationProgress = 1f;
            }

        }
    }

    private void PlayerMove()
    {
        if (_isPlayeeCanMove && _lastSpawnObj != null)
        {
            Vector3 targetPos = _lastSpawnObj.transform.position;
            targetPos.y = _player.transform.position.y;

            _player.transform.position = Vector3.Lerp(_player.transform.position, targetPos, 0.1f);

            if (Vector3.Distance(_player.transform.position, targetPos) < 0.01f)
            {
                _player.transform.position = targetPos;
                _isPlayeeCanMove = false;
                _shouldCameraFollow = true; 
                SpawnGround();
            }
        }

        if (_shouldCameraFollow)
        {

            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                Vector3 camPos = mainCam.transform.position;
                float targetX = _player.transform.position.x + 2.5f;
                camPos.x = Mathf.Lerp(camPos.x, targetX, 0.1f);
                mainCam.transform.position = camPos;

                if (Mathf.Abs(camPos.x - targetX) < 0.01f)
                {
                    camPos.x = targetX;
                    mainCam.transform.position = camPos;
                    _shouldCameraFollow = false;
                }
            }
        }
    }

}
