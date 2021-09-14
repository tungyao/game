using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxWidth;
    public float maxHeight;
    public float moveSpeed;
    void Start()
    {
        maxHeight = Screen.height;
        maxWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        if (H != 0 || V != 0)
        {
            transform.Translate(new Vector3(H, V, 0) * Time.deltaTime * moveSpeed, Space.World);
        }
    }
}
