using HutongGames.PlayMakerEditor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[CustomActionEditor(typeof(GetCurrentBindingAction))]
	public class GetCurrentBindingActionEditor : CustomActionEditor
	{
		private GUIContent[] _bindingOptions;
		private string[] _bindingOptionValues;

		public override bool OnGUI()
		{
			var source = base.target as GetCurrentBindingAction;
			
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

			EditField(nameof(source.currentRebindedKey));
			
			return isDirty || GUI.changed;
		}
	}
}