using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float _speed;
    private float _endPosX;

    public void StartFloating(float speed, float endPosX)
    {
        _speed = speed;
        _endPosX = endPosX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (Time.deltaTime * _speed));

        if(transform.position.x < _endPosX)
        {
            Destroy(gameObject);
        }
    }
}