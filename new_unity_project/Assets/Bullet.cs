using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxWidth;
    public float maxHeight;
    private Camera _camera;
    private bool _isCameraNotNull;
    public float maxSpeed = 10f;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        maxHeight = Screen.height;
        maxWidth = Screen.width;
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

        _rigidbody2D.velocity = new Vector2(0,maxSpeed);
    }
}
