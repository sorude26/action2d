using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    Transform _followTarget = default;
    [SerializeField]
    float _followSpeed = 1f;
    [SerializeField]
    Transform _rotationTarget = default;
    [SerializeField]
    float _rotationSpeed = 1f;
    #endregion

    #region PrivateField
    bool _isFollow = false;
    Vector3 _followPos = default;
    float _maxY = default;
    float _minY = default;
    float _maxX = default;
    float _minX = default;
    #endregion
    private void FixedUpdate()
    {
        if (!_isFollow) { return; }
        FollowMove();
    }
    private void FollowMove()
    {
        _followPos = _followTarget.position;
        float speed = (transform.position - _followPos).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followPos, speed * _followSpeed * Time.deltaTime);
    }
    private void FollowLook()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
    }

    public void StopFollow()
    {
        _isFollow = false;
    }
    public void StartFollow()
    {
        if (_followTarget != null)
        {
            _isFollow = true;
        }
    }
    public void SetHight(float max,float min) { _maxY = max; _minY = min;}
    public void SetWide(float max, float min) { _maxX = max; _minX = min;}
}
