using HutongGames.PlayMaker;
using UnityEngine.InputSystem;

public abstract class BindingActionBase : FsmStateAction
{
	[ObjectType(typeof(InputActionReference))]
	public FsmObject actionToRebind;

	public FsmInt bindingIndex;
}
