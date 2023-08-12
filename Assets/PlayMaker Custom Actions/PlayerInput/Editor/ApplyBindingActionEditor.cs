using HutongGames.PlayMakerEditor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[CustomActionEditor(typeof(ApplyBindingAction))]
	public class ApplyBindingActionEditor : CustomActionEditor
	{
		private GUIContent[] _bindingOptions;
		private string[] _bindingOptionValues;

		public override bool OnGUI()
		{
			var source = target as ApplyBindingAction;
			
			EditField(nameof(source.actionToRebind));
			
			var isDirty = false;

			EditField(nameof(source.manualBinding));
			
			if (source.manualBinding.Value)
			{
				EditField(nameof(source.bindingIndex));
			}
			else if (!source.manualBinding.Value && source.actionToRebind != null)
			{
				isDirty = GenerateBindingListHelper.GenerateBindingList(source, _bindingOptions, _bindingOptionValues);
			}

			EditField(nameof(source.overridePath));
			EditField(nameof(source.rebindKey));
			EditField(nameof(source.onApplied));

			return isDirty || GUI.changed;
		}
	}
}

