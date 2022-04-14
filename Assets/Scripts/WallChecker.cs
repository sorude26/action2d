using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    #region SerializeField
    /// <summary> Linecast�̎n�_ </summary>
    [SerializeField]
    private Transform[] _checkPoints = default;
    /// <summary> Linecast�̕��� </summary>
    [SerializeField]
    private Vector2 _checkDir = Vector2.down;
    /// <summary> Linecast�̒��� </summary>
    [SerializeField]
    private float _checkRange = 0.2f;
    /// <summary> �ǂ̃��C���[ </summary>
    [SerializeField]
    private LayerMask _wallLayer = default;
    /// <summary> �Œ�ڒn���萔 </summary>
    [SerializeField]
    private int _minHitCount = 1;
    #endregion

    /// <summary>
    /// �ǔ��肪�����true��Ԃ�
    /// </summary>
    /// <returns></returns>
    public bool IsWalled()
    {
        int count = 0;
        foreach (Transform point in _checkPoints)
        {
            Vector2 start = point.position;
            Vector2 end = start + _checkDir.normalized * _checkRange;
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
