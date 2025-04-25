using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;
    public Transform itemListGO;
    public GameObject itemSlotPrefab;
    public int selectedItemIndex = 0;
    public int maxItemAllowed = 10;
    public TextMeshProUGUI itemNameTMP;
    public TextMeshProUGUI itemDescriptionTMP;
    public InputActionReference inputItemLeft;
    public InputActionReference inputItemRight;

    void SelectItemLeft(InputAction.CallbackContext obj)
    {
        if (itemList.Count > 0) {
            this.moveToLeftItem();
            UpdateUI();
        }
    }

    void SelectItemRight(InputAction.CallbackContext obj)
    {
        if (itemList.Count > 0) {
            this.moveToRightItem();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (itemList.Count > 0)
        {
            this.itemNameTMP.text = itemList[selectedItemIndex].name;
            this.itemDescriptionTMP.text = itemList[selectedItemIndex].description;
        }
        else
        {
            this.itemNameTMP.text = "";
            this.itemDescriptionTMP.text = "";
        }
    }

    public void addItem(Item item) {
        if (itemList.Count < this.maxItemAllowed) {
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemListGO);
            ItemSlot itemSlotComponent = itemSlot.GetComponent<ItemSlot>();

            itemSlotComponent.addItem(item);
            itemList.Add(item);

            if (itemList.Count == 1) {
                selectedItemIndex = 0;
                this.setItemHilight(selectedItemIndex, true);
            }

            GameManager.instance.showMessageBox("Você encontrou " + item.name);
        }
    }
    void OnEnable()
    {
        inputItemLeft.action.started += SelectItemLeft;
        inputItemRight.action.started += SelectItemRight;
        Time.timeScale = 0;
        UpdateUI();
    }

    void OnDisable()
    {
        inputItemLeft.action.started -= SelectItemLeft;
        inputItemRight.action.started -= SelectItemRight;
        Time.timeScale = 1;
    }

    private void moveToLeftItem() {
        this.setItemHilight(selectedItemIndex, false);
        this.selectedItemIndex = Mathf.Max(0, selectedItemIndex - 1);
        this.setItemHilight(selectedItemIndex, true);
    }

    private void moveToRightItem() {
        this.setItemHilight(selectedItemIndex, false);
        this.selectedItemIndex = Mathf.Min(itemList.Count - 1, selectedItemIndex + 1);
        this.setItemHilight(selectedItemIndex, true);
    }

    private void setItemHilight(int index, bool value) {
        this.itemList[index].slot.transform.Find("Hilight").gameObject.SetActive(value);
    }
}
