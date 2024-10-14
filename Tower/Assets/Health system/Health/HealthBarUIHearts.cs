using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthBarUIHearts : HealthBar
{
    [SerializeField] List<GameObject> _hearts;
    [SerializeField] int _maxValue;
    [SerializeField] int _currentValue;
    public override void ReduceHP(int value)
    {
        _currentValue -= value;
        UpdateDisplay();
    }

    public override void SetHealth(int hp)
    {
        _currentValue = math.clamp(hp, 0, _maxValue);
        UpdateDisplay();
    }

    public override void SetMaxHealth(int value)
    {
        _maxValue = value;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        for(int i= _hearts.Count-1; i>= 0; i--) 
        {
            if (i > _currentValue-1) _hearts[i].SetActive(false);
            else _hearts[i].SetActive(true);

        }
    }
}
