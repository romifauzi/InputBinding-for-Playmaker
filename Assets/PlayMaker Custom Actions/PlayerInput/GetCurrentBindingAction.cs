#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("PlayerInput")]
    public class GetCurrentBindingAction : BindingActionBase
    {
        public FsmBool manualBinding = false;

        public FsmString currentRebindedKey;

        public override void OnEnter()
        {
            var effectivePath = (actionToRebind.Value as InputActionReference).action.bindings[bindingIndex.Value].effectivePath;
            currentRebindedKey.Value = InputControlPath.ToHumanReadableString(effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            Finish();
        }

        public override void Reset()
        {
            base.Reset();
            bindingIndex = 0;
        }
    }
}
#endif