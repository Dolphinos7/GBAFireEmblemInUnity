using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnItemHover : EventTrigger
{
    public GameObject currentStatBox;
    public Item thisItem;
    public bool showStats = false;
    public bool showing = false;
    public bool statsUpdated = false;
    public override void OnPointerEnter(PointerEventData data){
        showStats = true;
    }

    public override void OnPointerExit(PointerEventData data){
        showStats = false;
    }

    void Update(){
        if (currentStatBox != null && !statsUpdated && thisItem.isWeapon){
            Weapon currentWeapon = (Weapon)thisItem;
            GameObject.Find("AttackValue").GetComponent<TextMeshProUGUI>().text = (currentWeapon.might + TurnManager.playerSelected.GetComponent<PlayerCharacter>().getStats().strength).ToString();
            GameObject.Find("CritValue").GetComponent<TextMeshProUGUI>().text = (currentWeapon.crit + TurnManager.playerSelected.GetComponent<PlayerCharacter>().getStats().skill / 2).ToString();
            GameObject.Find("HitValue").GetComponent<TextMeshProUGUI>().text = (currentWeapon.hit + TurnManager.playerSelected.GetComponent<PlayerCharacter>().getStats().skill * 2 + TurnManager.playerSelected.GetComponent<PlayerCharacter>().getStats().luck / 2).ToString();
            GameObject.Find("AttackValue").GetComponent<TextMeshProUGUI>().text = (currentWeapon.might + TurnManager.playerSelected.GetComponent<PlayerCharacter>().getStats().strength).ToString();
            GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>().text = currentWeapon.name;
            GameObject.Find("AffiImage").GetComponent<Image>().sprite = null;
            statsUpdated = true;
        }
        else if (currentStatBox != null && !statsUpdated && thisItem.isUsableItem){
            UsableItem usable = (UsableItem)thisItem;
            GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = usable.description;
            GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>().text = usable.name;
            statsUpdated = true;
        }
        if (showStats && !showing){
            ItemMenuFunctions.currentItem = thisItem;
            if (thisItem.isWeapon){
                currentStatBox = Instantiate(Resources.Load<GameObject>("ItemStatBoxWeapon"), new Vector3(5, 5, 0), Quaternion.identity);
            }
            else{
                currentStatBox = Instantiate(Resources.Load<GameObject>("ItemDescriptionBox"), new Vector3(5, 5, 0), Quaternion.identity);
            }
            
            currentStatBox.transform.SetParent(GameObject.Find("Canvas").transform);
            currentStatBox.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            showing = true;
        }
        else if(!showStats && showing){
            GameObject.Destroy(currentStatBox);
            showing = false;
            currentStatBox = null;
            statsUpdated = false;
        }
    }
}
