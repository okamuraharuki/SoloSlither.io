using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _camDistance = 1.0f;
    PlayerWormManager _playerWormManager;
    private void Start()
    {
        _playerWormManager = FindAnyObjectByType<PlayerWormManager>().GetComponent<PlayerWormManager>();
    }
    void Update()
    {
        Camera.main.transform.position = _playerWormManager.GetPlayerPos + new Vector3(0, 0, -_camDistance);
    }
}
