using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace HutongGames.PlayMaker.Actions
{
	public class GenerateBindingListHelper
	{
		public static bool GenerateBindingList(BindingActionBase source, GUIContent[] bindingOptions, string[] bindingOptionValues)
		{
			var action = source.actionToRebind.Value as InputActionReference;

			if (action == null)
			{
				Debug.LogWarning((object)"Action reference not found");
				bindingOptions = (GUIContent[])(object)new GUIContent[0];
				bindingOptionValues = new string[0];
				return false;
			}

			var bindings = action.action.bindings;
			var bindingCount = bindings.Count;
			bindingOptions = new GUIContent[bindingCount];
			bindingOptionValues = new string[bindingCount];

			string currentBindingId = bindings[source.bindingIndex.Value].id.ToString();

			for (int i = 0; i < bindingCount; i++)
			{
				var binding = bindings[i];
				var bindingId = binding.id.ToString();
				var num = !string.IsNullOrEmpty(binding.groups);
				var displayOptions = InputBinding.DisplayStringOptions.DontUseShortDisplayNames | InputBinding.DisplayStringOptions.IgnoreBindingOverrides;
				
				if (!num)
				{
					displayOptions |= InputBinding.DisplayStringOptions.DontOmitDevice;
				}

				var displayString = action.action.GetBindingDisplayString(i, displayOptions);
				
				if (binding.isPartOfComposite)
				{
					displayString = ObjectNames.NicifyVariableName(binding.name) + ": " + displayString;
				}
				displayString = displayString.Replace('/', '\\');
				
				if (num)
				{
					var asset = action.action.actionMap?.asset;
					if (asset != null)
					{
						string controlSchemes = string.Join(", ", from x in binding.groups.Split(';', StringSplitOptions.None)
																  select asset.controlSchemes.FirstOrDefault((InputControlScheme c) => c.bindingGroup == x).name);
						displayString = displayString + " (" + controlSchemes + ")";
					}
				}

				bindingOptions[i] = new GUIContent(displayString);
				bindingOptionValues[i] = bindingId;
				
				if (currentBindingId.Equals(bindingId))
				{
					source.bindingIndex = i;
				}
			}

			EditorGUILayout.BeginHorizontal();
			var newSelectedBinding = EditorGUILayout.Popup(new GUIContent("Binding"), source.bindingIndex.Value, bindingOptions, (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.ExpandWidth(true) });
			var result = false;
			if (source.bindingIndex.Value != newSelectedBinding)
			{
				currentBindingId = bindingOptionValues[newSelectedBinding];
				source.bindingIndex.Value = newSelectedBinding;
				result = true;
			}
			EditorGUILayout.EndHorizontal();
			return result;
		}
	}
}
#endif