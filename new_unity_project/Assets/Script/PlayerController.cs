using System;
using UnityEngine;

namespace Script
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        public float maxWidth;
        public float maxHeight;
        public float moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private float _h;
        private float _v;
        private Camera _camera;
        private bool _isCameraNotNull;
        private Vector2 _pos;
        private Vector2 _newPosition = new Vector2();

        void Start()
        {
            maxHeight = Screen.height;
            maxWidth = Screen.width;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _isCameraNotNull = _camera != null;
        }

        private void FixedUpdate()
        {
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");

            // 理想中要移动的速度
            _newPosition = new Vector2(_h * moveSpeed, _v * moveSpeed);
            // 同时其实只有两个方向上的触发
            _pos = _camera.WorldToScreenPoint(transform.position);
            Debug.Log(_pos);

            //  这是在往下持续移动
            if (_pos.y <= 0 && _v <= 0)
            {
                _newPosition.y = 0;
            }

            if (_pos.y >= maxHeight && _v >= 0)
            {
                _newPosition.y = 0;
            }


            if (_pos.x <= 0 && _h <= 0)
            {
                _newPosition.x = 0;
            }

            if (_pos.x >= maxWidth && _h >= 0)
            {
                _newPosition.x = 0;
            }

            _rigidbody2D.velocity = _newPosition;
        }
    }
}