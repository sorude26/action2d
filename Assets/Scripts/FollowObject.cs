using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特定のオブジェクトに追従するクラス
/// </summary>
public class FollowObject : MonoBehaviour
{
    #region SerializeField
    /// <summary> 追従対象 </summary>
    [SerializeField]
    Transform _followTarget = default;
    /// <summary> 追従速度 </summary>
    [SerializeField]
    float _followSpeed = 1f;
    /// <summary> 追従方向対象 </summary>
    [SerializeField]
    Transform _rotationTarget = default;
    /// <summary> 回転速度 </summary>
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
    /// 追従移動
    /// </summary>
    private void FollowMove()
    {
        _followPos = _followTarget.position;
        float speed = (transform.position - _followPos).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followPos, speed * _followSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 追加回転
    /// </summary>
    private void FollowLook()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 追従停止
    /// </summary>
    public void StopFollow()
    {
        _isFollow = false;
    }
    /// <summary>
    /// 追従開始
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
