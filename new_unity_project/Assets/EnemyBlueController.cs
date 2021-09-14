using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlueController : MonoBehaviour
{
    public float maxWidth;
    public float maxHeight;
    private Camera _camera;
    private bool _isCameraNotNull;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
        maxHeight = Screen.height;
        maxWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCameraNotNull)
        {
            var pos = _camera.WorldToScreenPoint(transform.position);
            if (pos.y > maxHeight * 2 || pos.x > maxWidth * 2 || pos.y < -maxHeight/3 || pos.x < -maxWidth/3)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("接触到了人物");
    }
}