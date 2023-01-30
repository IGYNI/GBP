using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIItemInfo : MonoBehaviour
{
   public TMP_Text header;
   public TMP_Text description;
   public Button takeButton;
   public Image icon;

   private Item _item;

   private void Awake()
   {
      takeButton.onClick.AddListener(OnTakeButton);
   }

   public void Init(Item item)
   {
      _item = item;
      if (_item != null)
      {
         header.text = item.itemName;
         description.text = item.description;
         icon.sprite = item.icon;
      }
   }

   public void ClearInfo(Item item)
   {
      if (_item == item)
      {
         header.text = "";
         description.text = "";
         icon.sprite = null;
         _item = null;
      }
   }

   private void Update()
   {
      takeButton.interactable = _item != null;
   }

   private void OnTakeButton()
   {
      // move item to inventory;
      ClearInfo(_item);
      Destroy(_item.gameObject);
   }
}
