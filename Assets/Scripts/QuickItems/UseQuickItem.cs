using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseQuickItem : MonoBehaviour
{
    public InputReader ir;
    public TMP_Text itemText;
    public Image itemImage;
    public List<QuickItem> QuickItems = new List<QuickItem>();

    private void OnEnable()
    {
        ir.SwitchQuickItemEvent += SwapItem;
    }

    private void Start()
    {
        ResetQuickItems();
    }

    private void Update()
    {
        itemText.text = GetActive().Number.ToString();
        itemImage.sprite = GetActive().Icon;
        CheckMax();
    }

    private void CheckMax()
    {
        foreach (QuickItem quickItem in QuickItems)
        {
            if (quickItem.Number > 99) quickItem.Number = 99;
        }
    }

    public void ResetQuickItems()
    {
        foreach (QuickItem quickItem in QuickItems)
        {
            if (quickItem.Number > quickItem.DefaultNumber) return;
            quickItem.Number = quickItem.DefaultNumber;
        }
    }

    private void SwapItem()
    {
        int newActiveID = -1;
        if (ir.ScrollWheelValue == Vector2.up)
        {
            foreach (QuickItem quickItem in QuickItems)
            {
                if (quickItem.Active)
                {
                    quickItem.Active = false;
                    newActiveID = quickItem.ID + 1;
                    if (newActiveID >= QuickItems.Count) newActiveID = 0;
                    break;
                }
            }
            QuickItems[newActiveID].Active = true;
        }
        else
        {
            foreach (QuickItem quickItem in QuickItems)
            {
                if (quickItem.Active)
                {
                    quickItem.Active = false;
                    newActiveID = quickItem.ID - 1;
                    if (newActiveID < 0) newActiveID = QuickItems.Count - 1;
                    break;
                }
            }
            QuickItems[newActiveID].Active = true;
        }
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
                case 1: EssenceOfStamina(_stateMachine, active); break;
            }
        }
    }

    private void CelestrialNectarFunc(PlayerStateMachine _stateMachine, QuickItem active)
    {
        active.Number--;
        _stateMachine.SwitchState(new PlayerDrinkState(_stateMachine));
        _stateMachine.Health.AddHealth(30);
    }

    private void EssenceOfStamina(PlayerStateMachine _stateMachine, QuickItem active)
    {
        active.Number--;
        _stateMachine.SwitchState(new PlayerDrinkState(_stateMachine));
        _stateMachine.Stamina.AddStamina(25);
    }

    private void OnDisable()
    {
        ir.SwitchQuickItemEvent -= SwapItem;
    }
}