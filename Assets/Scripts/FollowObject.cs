using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����̃I�u�W�F�N�g�ɒǏ]����N���X
/// </summary>
public class FollowObject : MonoBehaviour
{
    #region SerializeField
    /// <summary> �Ǐ]�Ώ� </summary>
    [SerializeField]
    Transform _followTarget = default;
    /// <summary> �Ǐ]���x </summary>
    [SerializeField]
    float _followSpeed = 1f;
    /// <summary> �Ǐ]�����Ώ� </summary>
    [SerializeField]
    Transform _rotationTarget = default;
    /// <summary> ��]���x </summary>
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
    private void Start()
    {
        StartFollow();
    }
    private void FixedUpdate()
    {
        if (!_isFollow) { return; }
        FollowMove();
    }
    /// <summary>
    /// �Ǐ]�ړ�
    /// </summary>
    private void FollowMove()
    {
        _followPos = _followTarget.position;
        float speed = (transform.position - _followPos).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followPos, speed * _followSpeed * Time.deltaTime);
    }
    /// <summary>
    /// �ǉ���]
    /// </summary>
    private void FollowLook()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// �Ǐ]��~
    /// </summary>
    public void StopFollow()
    {
        _isFollow = false;
    }
    /// <summary>
    /// �Ǐ]�J�n
    /// </summary>
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
