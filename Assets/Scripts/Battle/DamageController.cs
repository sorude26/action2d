using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour, IDamageApplicable
{
    [SerializeField]
    private int _maxHP = 100;
    [SerializeField]
    StateController _stateController = default;
    private int _currentHP = default;
    public int CurrentHP 
    {
        get => _currentHP;
        private set
        {
            _currentHP = value;
            if(_currentHP <= 0)
            {
                Dead();
            }
        }
    }
    private void OnEnable()
    {
        _currentHP = _maxHP;
    }
    protected virtual void Dead()
    {
        _stateController.ChangeState(StateType.Down);
    }
    public virtual void AddDamage(DamageBundle damage)
    {
        CurrentHP -= damage.damage;
        _stateController.ChangeState(StateType.Damage);
    }
}
