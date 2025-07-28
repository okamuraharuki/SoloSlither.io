using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [ReadOnly] protected bool _isActive = false;

    [SerializeField, Range(0, 1)] private float _rotationCoffecient = 0.5f;

    [SerializeField] private float _minBodyDistanceSqr = 0.5f;

    [SerializeField] private float _distancebody = 0.1f;

    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private List<Transform> _bodyPositions = new List<Transform>();

    [SerializeField] private GameObject _rightEye;
    [SerializeField] private GameObject _leftEye;

    [SerializeField] private SpriteRenderer _bodySpriteRenderer;
    [SerializeField] private Color _bodyColor;

    virtual protected void Start()
    {
        _bodySpriteRenderer.color = _bodyColor;
    }

    virtual protected void Update()
    {

    }
    //ボディの追従処理
    protected void BodyMove()
    {
        if (_bodyPositions.Count > 0)
        {
            for (int i = _bodyPositions.Count - 1; i > 0; i--)
            {
                if ((_bodyPositions[i - 1].position - _bodyPositions[i].position).sqrMagnitude > _minBodyDistanceSqr)
                {
                    _bodyPositions[i].position += (_bodyPositions[i - 1].position - _bodyPositions[i].position) * _distancebody;
                }
            }
            _bodyPositions[0].position += (this.transform.position - _bodyPositions[0].position) * _distancebody;
        }
    }
    //頭の直接の移動
    // 2ベクトルの角度差を0~1に変換して代入 1/180＝0.005555...f
    protected void HeadMove(Vector3 moveVec)
    {
        transform.up = Vector3.Lerp(transform.up, moveVec, _rotationCoffecient);
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    protected void EyeMove(Vector3 moveVec)
    {
        if (!_leftEye || !_rightEye)
        {
            Debug.LogWarning("Eyes not registered");
            return;
        }
        _rightEye.transform.up = moveVec;
        _leftEye.transform.up = moveVec;
    }
    protected void BodyGenerate()
    {
        if (!_bodyPrefab)
        {
            Debug.LogWarning("Body not registered");
            return;
        }
        GameObject newBody = Instantiate(_bodyPrefab,
            _bodyPositions.Count > 0 ? _bodyPositions[_bodyPositions.Count - 1].position : this.transform.position,
            Quaternion.identity);
        newBody.GetComponent<SpriteRenderer>().color = _bodyColor;
        _bodyPositions.Add(newBody.transform);
    }
}
