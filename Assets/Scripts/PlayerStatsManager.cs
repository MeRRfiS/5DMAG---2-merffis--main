using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private PlayerController _player;

    private int Price(int level) => 10 + 5 * (level - 1);

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    public void BuyUpgradeHealth()
    {
        if (_player.Statistics.Money < Price(_player.Statistics.HealthLevel)) return;

        _player.Statistics.Money -= Price(_player.Statistics.HealthLevel);
        _player.Statistics.UpgradeHealth();

        UIController.Instance.UpdateCoinText(_player.Statistics.Money);
        UIController.Instance.ChangeHealthStat(_player.Statistics.HealthLevel, _player.Statistics.MaxHealth);
    }

    public void BuyUpgradeDamage()
    {
        if (_player.Statistics.Money < Price(_player.Statistics.DamageLevel)) return;

        _player.Statistics.Money -= Price(_player.Statistics.DamageLevel);
        _player.Statistics.UpgradeDamage();

        UIController.Instance.UpdateCoinText(_player.Statistics.Money);
        UIController.Instance.ChangeDamageStat(_player.Statistics.DamageLevel, _player.Statistics.AdditionalDamage);
    }

    public void BuyUpgradeSpeed()
    {
        if (_player.Statistics.Money < Price(_player.Statistics.SpeedLevel)) return;

        _player.Statistics.Money -= Price(_player.Statistics.SpeedLevel);
        _player.Statistics.UpgradeSpeed();

        UIController.Instance.UpdateCoinText(_player.Statistics.Money);
        UIController.Instance.ChangeSpeedStat(_player.Statistics.SpeedLevel, _player.Statistics.Speed);
    }

    public void BuyUpgradeMoney() 
    {
        if (_player.Statistics.Money < Price(_player.Statistics.MoneyLevel)) return;

        _player.Statistics.Money -= Price(_player.Statistics.MoneyLevel);
        _player.Statistics.UpgradeMoneyMultiplier();

        UIController.Instance.UpdateCoinText(_player.Statistics.Money);
        UIController.Instance.ChangeMoneyStat(_player.Statistics.MoneyLevel, _player.Statistics.MoneyMultipler);
    }
}
