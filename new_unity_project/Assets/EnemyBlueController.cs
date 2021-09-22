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

    public string weaponName = "leaser";
    private static readonly int IsDead = Animator.StringToHash("isDead");

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
            if (pos.y > maxHeight * 2 || pos.x > maxWidth * 2 || pos.y < -maxHeight / 3 || pos.x < -maxWidth / 3)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool _isLeaserTrigger = false;
    private float _leaserStayTime;
    public bool isDead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator animator = new Animator();
            if (TryGetComponent<Animator>(out animator))
            {
                Debug.Log("接触到了人物 --- Blue");
                animator.SetBool(id: IsDead, value: true);
            }
        }

        if (collision.CompareTag("leaser") && _isLeaserTrigger == false && isDead == false)
        {
            _isLeaserTrigger = true;
            _leaserStayTime = Time.fixedTime;
            Debug.Log("接触到了激光");
        }
    }

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        yield return new WaitForSeconds(0.1f);
        if (other.CompareTag("leaser") && Time.fixedTime - _leaserStayTime > 0.5f && isDead == false)
        {
            Debug.Log("是不是接触了2秒");
            Animator animator = new Animator();
            if (TryGetComponent<Animator>(out animator))
            {
                Debug.Log("接触到了人物 --- Blue");
                animator.SetBool( "isDead", value: true);
                isDead = true;
            }
        }
    }
}