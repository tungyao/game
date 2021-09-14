using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;
    public float intervalTime = 2f;
    private Transform one;
    public float cycleTime = 0f;
    void Start()
    {
        one = enemyOne.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        cycleTime += Time.deltaTime;
        // Debug.Log(cycleTime);
        if (cycleTime > intervalTime) {
            one.transform.position = new Vector3(Random.Range(-10, 10),Random.Range(-4,4));
            GameObject.Instantiate(one);
            cycleTime = 0;
        }
          
    }
}
