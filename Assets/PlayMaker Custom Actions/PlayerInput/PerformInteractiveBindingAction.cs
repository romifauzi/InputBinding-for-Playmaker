using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerInput")]
	public class PerformInteractiveBindingAction : BindingActionBase
	{
		[ObjectType(typeof(MatchPath))]
		public FsmEnum matchPath;
		public FsmBool manualBinding;
		public FsmInt bindingIndexCache;
		public FsmString newRebindedKey;
		public FsmString newRebindedPath;
		[Tooltip("For multiple path, add , between path")]
		public FsmString excludedPath;
		public FsmEvent OnRebind;
		public FsmEvent OnError;

		public override void OnEnter()
		{
			InputActionReference action = actionToRebind.Value as InputActionReference;
			RemapAction(action, bindingIndex.Value);
		}

		private void RemapAction(InputActionReference action, int bindingIndex = -1)
		{
			action.action.Disable();
			bindingIndexCache.Value = bindingIndex;
			var rebind = action.action.PerformInteractiveRebinding(bindingIndex).WithControlsHavingToMatchPath($"<{matchPath}>").WithExpectedControlType<ButtonControl>()
				.OnMatchWaitForAnother(0.1f)
				.WithCancelingThrough($"<{MatchPath.Keyboard}>/escape")
				.OnComplete(RebindEnd)
				.OnCancel(RebindError);

			if (excludedPath.Value.Length > 0)
			{ 
				var excludes = excludedPath.Value.Split(",");

				foreach (var ex in excludes)
				{
					rebind.WithControlsExcluding(ex);
				}
			}

			rebind.Start();

			void RebindEnd(InputActionRebindingExtensions.RebindingOperation operation)
			{
				string effectivePath = operation.action.bindings[base.bindingIndex.Value].effectivePath;
				newRebindedPath.Value = effectivePath;
				newRebindedKey.Value = InputControlPath.ToHumanReadableString(effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
				operation.Dispose();
				action.action.Enable();
				Fsm.Event(OnRebind);
			}
			void RebindError(InputActionRebindingExtensions.RebindingOperation operation)
			{
				newRebindedKey.Value = InputControlPath.ToHumanReadableString(operation.action.bindings[base.bindingIndex.Value].path, InputControlPath.HumanReadableStringOptions.OmitDevice);
				operation.Dispose();
				action.action.Enable();
				Fsm.Event(OnError);
			}
		}

		public override void Reset()
		{
			base.Reset();
			bindingIndex = 0;
		}

		public override string ErrorCheck()
		{
			return null;
		}
	}

	public enum MatchPath
    {
		Keyboard,
		Gamepad,
		Mouse
    }
}
#endif