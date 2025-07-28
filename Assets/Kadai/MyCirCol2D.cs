using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCirCol2D : MonoBehaviour
{
    private static List<MyCirCol2D> _allCirCol2D = new List<MyCirCol2D>(1024);
    [SerializeField] Vector2 _offset = Vector2.zero;
    [SerializeField] float _rad = 0.5f;

    bool _isColed = false;
    public bool IsColed => _isColed;
    bool _needCheckCol = true;
    public bool NeedCheckCol => _needCheckCol;

    public float Rad => _rad * this.transform.lossyScale.x;
    public Vector2 Offset => _offset;

    private Action<MyCirCol2D> _coledAction = null;
    public void SetCallback(Action<MyCirCol2D> coledAction)
    {
        _coledAction = coledAction;
    }
    void Awake()
    {
        _allCirCol2D.Add(this);
    }
    void OnDestroy()
    {
        _allCirCol2D.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(this._isColed)
            Destroy(this.gameObject);

        this._isColed = false;
        if (!_needCheckCol)
            return;
        var a = this;
        foreach( var b  in _allCirCol2D )
        {
            if(a == b)
                continue;
            Vector2 positiona = (Vector2)a.transform.position + a.Offset;
            Vector2 positionb = (Vector2)a.transform.position + b.Offset;

            float sqrtMagnitude = (positiona - positionb).sqrMagnitude;
            float additiveRad = a.Rad+ b.Rad;
            
            if(sqrtMagnitude <= additiveRad * additiveRad)
            {
                _isColed |= true;
                _coledAction.Invoke(b);
                b.SetColed();
            }
        }
    }
    private void LateUpdate()
    {
        _needCheckCol = true;
    }

    public void SetColed()
    {
        _isColed = true;
        _needCheckCol = false;
    }
}
