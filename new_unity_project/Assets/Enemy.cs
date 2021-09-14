using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyOne;
    public float intervalTime = 2f;
    private Transform _one;
    public float cycleTime = 0f;
    public float maxWidth;
    public float maxHeight;
    private Camera _camera;
    private bool _isCameraNotNull;

    void Start()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
        maxHeight = Screen.height + 50;
        maxWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        cycleTime += Time.deltaTime;
        // Debug.Log(cycleTime);
        if (cycleTime > intervalTime)
        {
            Debug.Log(Random.Range(0, 2));
            _one = enemyOne[Mathf.FloorToInt(Random.Range(0, 3))].GetComponent<Transform>();
            _one.transform.position = _camera.ScreenToWorldPoint(new Vector3(Random.Range(0, maxWidth), maxHeight, 2));
            Instantiate(_one);
            cycleTime = 0;
        }
    }
}