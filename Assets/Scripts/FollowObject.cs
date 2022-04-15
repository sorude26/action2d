using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ì¡íËÇÃÉIÉuÉWÉFÉNÉgÇ…í«è]Ç∑ÇÈÉNÉâÉX
/// </summary>
public class FollowObject : MonoBehaviour
{
    #region SerializeField
    /// <summary> í«è]ëŒè€ </summary>
    [SerializeField]
    Transform _followTarget = default;
    /// <summary> í«è]ë¨ìx </summary>
    [SerializeField]
    float _followSpeed = 1f;
    /// <summary> í«è]ï˚å¸ëŒè€ </summary>
    [SerializeField]
    Transform _rotationTarget = default;
    /// <summary> âÒì]ë¨ìx </summary>
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
    /// í«è]à⁄ìÆ
    /// </summary>
    private void FollowMove()
    {
        _followPos = _followTarget.position;
        float speed = (transform.position - _followPos).sqrMagnitude;
        transform.position = Vector3.Lerp(transform.position, _followPos, speed * _followSpeed * Time.deltaTime);
    }
    /// <summary>
    /// í«â¡âÒì]
    /// </summary>
    private void FollowLook()
    {
        transform.forward = Vector3.Lerp(transform.forward, _rotationTarget.forward, _rotationSpeed * Time.deltaTime);
    }
    /// <summary>
    /// í«è]í‚é~
    /// </summary>
    public void StopFollow()
    {
        _isFollow = false;
    }
    /// <summary>
    /// í«è]äJén
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
