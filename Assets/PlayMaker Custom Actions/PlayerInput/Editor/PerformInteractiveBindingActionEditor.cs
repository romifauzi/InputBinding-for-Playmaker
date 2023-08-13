using HutongGames.PlayMakerEditor;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[CustomActionEditor(typeof(PerformInteractiveBindingAction))]
	public class PerformInteractiveBindingActionEditor : CustomActionEditor
	{
		private GUIContent[] _bindingOptions;
		private string[] _bindingOptionValues;

		public override bool OnGUI()
		{
			var source = target as PerformInteractiveBindingAction;

			EditField(nameof(source.actionToRebind));
			EditField(nameof(source.matchPath));
			EditField(nameof(source.manualBinding));
			
			var isDirty = false;
			if (source.manualBinding.Value)
			{
				EditField(nameof(source.bindingIndex));
			}
			else if (!source.manualBinding.Value && source.actionToRebind != null)
			{
				isDirty = GenerateBindingListHelper.GenerateBindingList(source, _bindingOptions, _bindingOptionValues);
			}
			EditField(nameof(source.bindingIndexCache));
			EditField(nameof(source.newRebindedKey));
			EditField(nameof(source.newRebindedPath));
			EditField(nameof(source.excludedPath));
			EditField(nameof(source.OnRebind));
			EditField(nameof(source.OnError));
			
			return isDirty || GUI.changed;
		}
	}
}

