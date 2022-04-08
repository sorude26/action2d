using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField]
    private Transform[] _checkPoints = default;
    [SerializeField]
    private Vector2 _checkDir = Vector2.down;
    [SerializeField]
    private float _checkRange = 0.2f;
    [SerializeField]
    private LayerMask _layer = default;

    public bool IsWalled(int maxHitCount = 1) 
    {
        int count = 0;
        foreach (Transform point in _checkPoints)
        {
            Vector2 start = point.position;
            Vector2 end = start + _checkDir * _checkRange;
            bool isHit = Physics2D.Linecast(start, end, _layer);
            if (isHit)
            {
                count++;
                if (count >= maxHitCount)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
