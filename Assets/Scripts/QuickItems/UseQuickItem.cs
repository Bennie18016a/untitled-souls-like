using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseQuickItem : MonoBehaviour
{
    public TMP_Text itemText;
    public List<QuickItem> QuickItems = new List<QuickItem>();

    private void Start()
    {
        foreach (QuickItem quickItem in QuickItems)
        {
            quickItem.Number = quickItem.DefaultNumber;
        }
    }

    private void Update()
    {
        itemText.text = string.Format("{0} - {1}", GetActive().ItemName, GetActive().Number);
    }

    private QuickItem GetActive()
    {
        foreach (QuickItem quickItem in QuickItems)
        {
            if (quickItem.Active)
            {
                return quickItem;
            }
        }
        return null;
    }

    public void UseItem(PlayerStateMachine _stateMachine)
    {
        QuickItem active = GetActive();

        if (active.Number > 0)
        {
            switch (active.ID)
            {
                case 0: CelestrialNectarFunc(_stateMachine, active); break;
            }
        }
    }

    private void CelestrialNectarFunc(PlayerStateMachine _stateMachine, QuickItem active)
    {
        active.Number--;
        _stateMachine.SwitchState(new PlayerDrinkState(_stateMachine));
        _stateMachine.Health.AddHealth(10);
    }
}