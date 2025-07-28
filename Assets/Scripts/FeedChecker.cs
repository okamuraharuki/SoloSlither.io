using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FeedChecker : MonoBehaviour
{
    [SerializeField] Vector2 _offset = Vector2.zero;
    [SerializeField] float _radius = 0.5f;
    public event Func<Vector3, bool> CheckEatFeed;
    public Action<int> EatFeed;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
