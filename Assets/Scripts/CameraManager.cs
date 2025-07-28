using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _camDistance = 1.0f;
    [SerializeField]  PlayerWormManager _playerWormManager;
    private void Start()
    {
        _playerWormManager = FindAnyObjectByType<PlayerWormManager>();
    }
    void Update()
    {
        Vector3 vdebug = Camera.main.transform.position;
        Vector3 vdebug2 = _playerWormManager.GetPlayerPos + new Vector3(0, 0, -_camDistance); 
        Camera.main.transform.position = _playerWormManager.GetPlayerPos + new Vector3(0, 0, -_camDistance);
    }
}
