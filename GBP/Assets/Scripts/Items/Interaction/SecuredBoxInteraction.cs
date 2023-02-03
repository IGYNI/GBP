using System.Collections;
using UnityEngine;

public class SecuredBoxInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.Interact;
	public override bool Interactable { get; protected set; }
	[SerializeField] private string overviewInfo;
	[SerializeField] private ItemInfo hiddenItem;
	[SerializeField] private NoteInfo noteInfo;
	[SerializeField] private CodeLock codeLock;
	[SerializeField] private GameObject securedView;
	private VariableSystem _variableSystem;

	private void Awake()
	{
		Interactable = true;
		codeLock.State.OnValueChanged += HandleLockState;
	}

	private void HandleLockState(Puzzle.EState prev, Puzzle.EState current)
	{
		if (current == Puzzle.EState.Complete)
		{
			_variableSystem.SetVariable(item.info.itemName + Item.UnlockedSuffix, "true", true);
			if (hiddenItem != null)
			{
				_variableSystem.SetVariable(hiddenItem.itemName + Item.TakenSuffix, "true", true);
				_variableSystem.Inventory.AddItem(hiddenItem);
			}

			if (noteInfo != null)
			{
				_variableSystem.SetVariable(noteInfo.noteName + Item.TakenSuffix, "true", true);
				_variableSystem.NoteBook.Add(noteInfo);
			}

			if (securedView != null)
			{
				securedView.SetActive(false);
			}
			Interactable = false;
		}
	}

	public override bool Interact(VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
		StartCoroutine(ShowCodeLock());
		return true;
	}

	private IEnumerator ShowCodeLock()
	{
		yield return new WaitForSeconds(0.5f);
		codeLock.Show();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		GameVar variable = variableSystem.GetVariable(item.info.itemName + Item.UnlockedSuffix);
		if (variable != null)
		{
			Interactable = variable.Value != "true";
		}
		if (securedView != null)
		{
			securedView.SetActive(Interactable);
		}
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overviewInfo;
	}
}