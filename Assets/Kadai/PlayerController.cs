using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed = 0.5f;

    [SerializeField]
    private SpriteRenderer _prefabBody = null;

    private List<SpriteRenderer> _listSprBody = null;

    List<Vector2> _lastPositions = new List<Vector2>();

    private Camera _mainCamera = null;

    [SerializeField]
    private Transform _eyeLeft;

    [SerializeField]
    private Transform _eyeRight;

    [SerializeField]
    MyCirCol2D _col2D;

    private int _point = 1;
    private void GrowUp(MyCirCol2D col)
    {
        var f = col.GetComponent<FoodController>();
        if (f)
        {
            _point += (int)f.FoodWeight;
            Destroy(f.gameObject);

            var spr = Instantiate<SpriteRenderer>(_prefabBody, this.transform);
            _listSprBody.Add(spr);
            
            spr.transform.localScale = _prefabBody.transform.localScale;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        this.transform.localScale = Vector3.one * 0.5f;

        for (int i = 0; i < 10; i++)
        {

        }
        _point = 1;

        _col2D.SetCallback(GrowUp);
        _lastPositions.Add(this.transform.position);

        _listSprBody.Add(_prefabBody);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mouseScreenPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mauseDiff = mouseScreenPos - this.transform.position;

        float angle = Mathf.Atan2(mauseDiff.y, mauseDiff.x) * Mathf.Rad2Deg;
        Quaternion qTarget = Quaternion.Euler(0f, 0f, angle);
        _eyeLeft.transform.rotation = qTarget;
        _eyeRight.transform.rotation = qTarget;
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, qTarget, 0.05f);
        //Z軸回転量を取得
        float moveAngleDegree = transform.rotation.eulerAngles.z;
        //radに変換して計算
        float moveAngleRadius = Mathf.Deg2Rad * moveAngleDegree;
        float normalX = Mathf.Cos(moveAngleRadius);
        float normalY = Mathf.Sin(moveAngleRadius);

        // 移動
        Vector3 move = new Vector3(normalX, normalY, 0) * _moveSpeed * Time.deltaTime;
        transform.position += move;

        for(int i = _lastPositions.Count - 1; i > 0; i--)
        {
            if(i == 0)
            {
                _lastPositions[0] = transform.position;
            }
            else
            {
                _lastPositions[i] = _lastPositions[_lastPositions.Count - 1];
            }
        }
        for(int i = 0;i < _listSprBody.Count; i++)
        {
            var sqr = _listSprBody[i];
            ////
        }

    }
    private void LateUpdate()
    {
        _mainCamera.transform.position = this.transform.position;
    }
}
