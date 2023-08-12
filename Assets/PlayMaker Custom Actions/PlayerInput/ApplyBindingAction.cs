#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerInput")]
	public class ApplyBindingAction : BindingActionBase
	{
		public FsmBool manualBinding = false;
		public FsmString rebindKey;
		public FsmString overridePath;
		public FsmEvent onApplied;

		public override void OnEnter()
		{
			ApplyAction();
		}

		private void ApplyAction()
		{
			var actionRef = actionToRebind.Value as InputActionReference;

			if (actionRef == null || string.IsNullOrEmpty(overridePath.Value))
			{
				Finish();
			}

			actionRef.action.ApplyBindingOverride(bindingIndex.Value, overridePath.Value);
			rebindKey.Value = InputControlPath.ToHumanReadableString(overridePath.Value, InputControlPath.HumanReadableStringOptions.OmitDevice);
			Fsm.Event(onApplied);
		}

		public override void Reset()
		{
			base.Reset();
			bindingIndex = 0;
		}
	}
}
#endif