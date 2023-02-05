using UnityEngine;
using UnityEngine.UI;

public class SelectReceipt : MonoBehaviour
{
	[SerializeField] private UIReceipt receiptView;
	[SerializeField] private Button selectButton;
	[SerializeField] private Image selection;
	private Fabricator _fabricator;
	private Receipt _receipt;

	private void Awake()
	{
		selectButton.onClick.AddListener(LoadReceipt);
	}

	public void Setup(Fabricator fabricator, Receipt receipt)
	{
		_receipt = receipt;
		_fabricator = fabricator;
		receiptView.SetReceipt(receipt);
	}
	
	public void UpdateView(Receipt receipt)
	{
		selection.enabled = _receipt == receipt;
		selectButton.interactable = !_fabricator.IsProcessing;
	}
	
	private void LoadReceipt()
	{
		_fabricator.LoadReceipt(_receipt);
	}
}