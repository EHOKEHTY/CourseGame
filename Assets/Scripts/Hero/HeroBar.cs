using UnityEngine;
using UnityEngine.UI;

public class HeroBar : MonoBehaviour
{

    [Header("Hero")]
    [SerializeField] private Hero _hero;

    [Header("Health")]
    [SerializeField] private Sprite _emptyHealth;
    [SerializeField] private Sprite _fullHealth;

    [Header("Stamina")]
    [SerializeField] private Sprite _emptyStamina;
    [SerializeField] private Sprite _fullStamina;

    [Header("HeroIcon")]
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite[] _iconStates;

    [Header("Scales")]
    [SerializeField] private Image[] _lives;
    [SerializeField] private Image[] _stamina;

    public void StaminaUpdate()
    {
        for (int i = 0, j = 0; i < _hero.MaxStamina; i++)
        {
            if (i < _hero.Stamina)
            {
                _stamina[j].sprite = _fullStamina;

            }
            else
            {
                _stamina[j].sprite = _emptyStamina;
            }
            if ((i + 1) * 10 >= (_hero.MaxStamina * (j + 1)))
            {
                j++;
            }
        }
    }

    public void HealthUpdate()
    {
        for (int i = 0, j = 0; i < _hero.MaxHP; i++)
        {
            if (i < _hero.HP)
            {
                _lives[j].sprite = _fullHealth;

            }
            else
            {
                _lives[j].sprite = _emptyHealth;
            }
            if ((i + 1) * 10 >= (_hero.MaxHP * (j + 1)))
            {
                j++;
            }
        }
        UpdateHealthIcon();
    }

     private void UpdateHealthIcon()
    {
        if (_hero.HP <= 0)
        {
            _icon.sprite = _iconStates[0];
        }
        else
        {
            float step = _hero.MaxHP / (_iconStates.Length - 1);

            int iconIndex = Mathf.CeilToInt(_hero.HP / step);

            _icon.sprite = _iconStates[iconIndex];
        }
    }


}
