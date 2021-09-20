using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

        public GameObject tipsPost;

        // 加载子弹预制体
        public GameObject[] Bullet;

        // 当前的携带的子弹
        private GameObject _hitBullet = null;

        public float BulletForce = 10f;


        // 激光子弹承载器
        public GameObject[] leaserShotDot;
        public GameObject show;

        // 激光武器是否正在发射
        private bool _isLeaserFired = false;

        // 接受到的激光武器
        private GameObject _getLeaserWeapon;

        void Start()
        {
            maxHeight = Screen.height;
            maxWidth = Screen.width;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _isCameraNotNull = _camera != null;
            tipsPost.GetComponent<Text>().text = "";
            _hitBullet = Bullet[0];
        }

        // 角色获取到的子弹


        // 控制角色移动
        private void PlayerMove()
        {
            _newPosition = new Vector2(_h * moveSpeed, _v * moveSpeed);
            // 同时其实只有两个方向上的触发
            _pos = _camera.WorldToScreenPoint(transform.position);
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

        private void FixedUpdate()
        {
            _h = Input.GetAxis("Horizontal");
            _v = Input.GetAxis("Vertical");
            PlayerMove();
            // 理想中要移动的速度
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
            {
                shotMusic();
                if (_hitBullet != null && !_hitBullet.CompareTag("leaser"))
                {
                    var bullet = _hitBullet;
                    var transform1 = transform;
                    var position = transform1.position;
                    bullet.transform.position =
                        new Vector3(position.x, (float)(position.y + 0.5), position.z);
                    Instantiate(bullet);
                }

                // 有激光武器的操作
                // 只能发射移一次
                if (_getLeaserWeapon != null && _getLeaserWeapon.CompareTag("leaser") && _isLeaserFired == false)
                {
                    CreateLeaserAndCountdown();
                    _isLeaserFired = true;
                }
            }

            tipsPost.transform.position = transform.position + new Vector3(0.7f, -0.3f);
        }

        // 生成激光武器并倒计时
        private void CreateLeaserAndCountdown()
        {
            _hitBullet = Instantiate(_getLeaserWeapon, transform.GetChild(0), false);
            StartCoroutine(StartLeaserShowTime());
        }

        // 用来碰撞 从而获取该子弹
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_getLeaserWeapon == null && !other.CompareTag("bullet"))
            {
                Debug.Log(other.GetComponent<EnemyBlueController>().weaponName);
                foreach (var variable in leaserShotDot)
                {
                    // 获取激光类型
                    if (other.GetComponent<EnemyBlueController>().weaponName == variable.name)
                    {
                        _getLeaserWeapon = variable;
                        _hitBullet = variable;
                        tipsPost.GetComponent<Text>().text = "吃到了" + variable.name;
                        return;
                    }
                }
            }

            // Debug.Log(other.GetComponent<EnemyBlueController>().weaponName);
        }

        public float leaserStayTime = 5f;

        // 控制激光武器的持续时间
        IEnumerator StartLeaserShowTime()
        {
            Debug.Log("start leaser time");
            yield return new WaitForSeconds(leaserStayTime);
            if (_getLeaserWeapon != null && _getLeaserWeapon.CompareTag("leaser"))
            {
                // 持续的将lease 缩小
                StartCoroutine(DestoryTheLeaser(_hitBullet));
            }

            Debug.Log("end leaser time");
        }

        // 移除激光武器
        IEnumerator DestoryTheLeaser(GameObject leaser)
        {
            var x = leaser.GetComponent<LineRenderer>();
            var i = 1f;
            while (x.GetPosition(1).y > 0)
            {
                yield return new WaitForFixedUpdate();
                x.SetPosition(1, new Vector2(x.GetPosition(1).x, x.GetPosition(1).y - (0.1f * i)));
                i += 0.3f;
            }

            Destroy(leaser);
            _isLeaserFired = false;
            _getLeaserWeapon = null;
            _hitBullet = null;
            tipsPost.GetComponent<Text>().text = "";
        }

        private void shotMusic()
        {
            if (_hitBullet!=null && !_hitBullet.CompareTag("leaser"))
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}