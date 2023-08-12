using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.InputSystem;

[ActionCategory("PlayerInput")]
public class SaveBindingsOverrideAction : FsmStateAction
{
	[ObjectType(typeof(InputActionAsset))]
	public FsmObject inputActionAsset;

	public FsmString bindingsDataInJson;

	public override void OnEnter()
	{
		InputActionAsset asset = inputActionAsset.Value as InputActionAsset;
		if ((Object)(object)asset == (Object)null)
		{
			Debug.LogWarning((object)"No Input Action Asset Assigned, skipping");
			((FsmStateAction)this).Finish();
		}
		bindingsDataInJson.Value = asset.SaveBindingOverridesAsJson();
		((FsmStateAction)this).Finish();
	}
}