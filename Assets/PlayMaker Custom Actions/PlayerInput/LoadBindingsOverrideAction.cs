using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.InputSystem;

[ActionCategory("PlayerInput")]
public class LoadBindingsOverrideAction : FsmStateAction
{
	[ObjectType(typeof(InputActionAsset))]
	public FsmObject inputActionAsset;

	public FsmString bindingsDataInJson;

	public override void OnEnter()
	{
		InputActionAsset obj = inputActionAsset.Value as InputActionAsset;
		if ((Object)(object)obj == (Object)null)
		{
			Debug.LogWarning((object)"No Input Action Asset Assigned, skipping");
			((FsmStateAction)this).Finish();
		}
		obj.LoadBindingOverridesFromJson(bindingsDataInJson.Value);
		((FsmStateAction)this).Finish();
	}
}