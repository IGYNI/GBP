using System;
using System.Collections;
using UnityEngine;

public class FabricatorInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.Interact;
	public override bool Interactable { get; protected set; }
	[SerializeField] private string overviewInfo;
	[SerializeField] private Fabricator fabricator;
	private VariableSystem _variableSystem;

	private void Awake()
	{
		Interactable = true;
	}

	public override bool Interact(VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
		StartCoroutine(ShowFabricator());
		return true;
	}

	private IEnumerator ShowFabricator()
	{
		yield return new WaitForSeconds(0.5f);
		fabricator.Show();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overviewInfo;
	}
}