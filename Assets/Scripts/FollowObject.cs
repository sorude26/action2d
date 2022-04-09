using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    Transform _followTarget = default;
    [SerializeField]
    float _followSpeed = 1f;
    /*
    [SerializeField]
    Transform _rotationTarget = default;
    [SerializeField]
    float _rotationSpeed = 1f;
    */

    Vector3 _followPos = default;
    float _maxY = default;
    float _minY = default;
    float _maxX = default;
    float _minX = default;
    private void FixedUpdate()
    {
        if (_followTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        //transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
        float speed = (transform.position - _followTarget.position).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followTarget.position, speed * _followSpeed * Time.deltaTime);
    }
}
