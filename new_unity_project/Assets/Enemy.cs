using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyOne;
    public float intervalTime = 2f;
    private GameObject _one;
    public float cycleTime = 0f;
    public float maxWidth;
    public float maxHeight;
    private Camera _camera;
    private bool _isCameraNotNull;
    public float minGravityScale;
    public float maxGravityScale;
    public int flow = 0;


    public string[] enemys = new string[]
    {
        "enemyBlue", "enemyGreen", "enemyRed",
        "leaser", "leaser1"
    };

    void Start()
    {
        _camera = Camera.main;
        _isCameraNotNull = _camera != null;
        maxHeight = Screen.height + 50;
        maxWidth = Screen.width;
        StartCoroutine(UpFLow());
    }

    // Update is called once per frame

    IEnumerator UpFLow()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            flow += 1;
            Debug.LogFormat("get number {0}", flow);
        }
    }

    private void LateUpdate()
    {
        cycleTime += Time.deltaTime;
        if (cycleTime > intervalTime)
        {
            // Debug.Log(Random.Range(0, 2));
            _one = enemyOne[Mathf.FloorToInt(Random.Range(0, 3))];
            _one.GetComponent<Transform>().transform.position =
                _camera.ScreenToWorldPoint(new Vector3(Random.Range(0, maxWidth), maxHeight, 2));
            _one.GetComponent<Rigidbody2D>().gravityScale = Random.Range(minGravityScale, maxGravityScale);
            if (Mathf.FloorToInt(Random.Range(0, 100f)) % 2 == 0)
            {
                var one = Instantiate(_one);
                one.GetComponent<EnemyBlueController>().weaponName = "Line";
            }
            else
            {
                var one = Instantiate(_one);
            }

            cycleTime = 0;
        }
    }
}