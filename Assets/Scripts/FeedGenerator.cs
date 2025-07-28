using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedGenerator : MonoBehaviour
{
    [SerializeField] bool _isActive = false;

    [SerializeField] GameObject _minFeed;

    [SerializeField] List<int> _feedPowers = new List<int>() { 2, 5, 12 };

    [SerializeField] float _maxSpawnDistance = 100;

    [SerializeField] int _maxSpawnableCnt = 1000;
    int _spawnCnt = 0;

    void Start()
    {
        
    }
    void Update()
    {
        if(_isActive)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            float spawnAangle = Random.Range(0, Mathf.PI * 2);
            float spawnDistance = Random.Range(0, _maxSpawnDistance);
            GameObject generatedFeed = Instantiate(_minFeed,
                new Vector3(_maxSpawnDistance * Mathf.Cos(spawnAangle), _maxSpawnDistance * Mathf.Sin(spawnAangle)) * spawnDistance,
                Quaternion.identity);
            _spawnCnt++;
            generatedFeed.transform.localScale *= _feedPowers[0];
            generatedFeed.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
