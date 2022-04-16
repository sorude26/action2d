using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 壁判定を行うクラス
/// </summary>
public class WallChecker : MonoBehaviour
{
    #region SerializeField
    /// <summary> Linecastの始点 </summary>
    [SerializeField]
    private Transform[] _checkPoints = default;
    /// <summary> Linecastの方向 </summary>
    [SerializeField]
    private Vector2 _checkDir = Vector2.down;
    /// <summary> Linecastの長さ </summary>
    [SerializeField]
    private float _checkRange = 0.2f;
    /// <summary> 壁のレイヤー </summary>
    [SerializeField]
    private LayerMask _wallLayer = default;
    /// <summary> 最低接地判定数 </summary>
    [SerializeField]
    private int _minHitCount = 1;
    #endregion

    /// <summary>
    /// 壁判定があればtrueを返す
    /// </summary>
    /// <returns></returns>
    public bool IsWalled()
    {
        return IsWalled(_checkDir);
    }
    /// <summary>
    /// キャラクターの向きに合わせた方向に壁判定があればtrueを返す
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool IsWalled(CharaDirection direction)
    {
        Vector2 checkDir = _checkDir;
        checkDir.x *= (int)direction;
        return IsWalled(checkDir);
    }
    /// <summary>
    /// 指定方向に壁判定があればtrueを返す
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public bool IsWalled(Vector2 dir)
    {
        int count = 0;
        foreach (Transform point in _checkPoints)
        {
            Vector2 start = point.position;
            Vector2 end = start + dir.normalized * _checkRange;
            bool isHit = Physics2D.Linecast(start, end, _wallLayer);
            if (isHit)
            {
                count++;
                if (count >= _minHitCount)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
