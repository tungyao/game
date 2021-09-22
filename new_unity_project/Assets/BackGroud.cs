using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BackGroud : MonoBehaviour
{
    public float maxWidth;
    public float maxHeight;
    public GameObject groundFre;
    public GameObject Ground;

    public GameObject StarSmall;

    public GameObject StarBig;

    private bool _isCloned = false;
    public float groundSpeed = 0.1f;
    private void Start()
    {
        maxHeight = Screen.height;
        maxWidth = Screen.width;
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        var transform1 = transform;
        var pos = transform1.position;
        transform1.position = pos - new Vector3(0, groundSpeed);
        if (pos.y < -41f * 3)
        {
            Destroy(gameObject);
        }
        else if (pos.y < -12f && _isCloned == false)
        {
            CreateNewGround(pos);
        }
    }

    // 创建一个新的在后面叠加
    private void CreateNewGround(Vector3 pos)
    {
        var g = Instantiate(groundFre);
        g.transform.position = new Vector3(0, 28.8f, 0);

        _isCloned = true;
    }
}