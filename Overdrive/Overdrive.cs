using System;
using System.Collections.Generic;
using UnityEngine;
using KSP.UI.Screens;
using KSP.IO;
using UICore;

namespace Overdrive
{
	[KSPAddon (KSPAddon.Startup.Instantly, true)]
	public class Overdrive: MonoBehaviour
	{
		static private Overdrive _instance = null;
		UICore.UICore UI;

		private Dictionary<string, string> skins;

		private ApplicationLauncherButton appLauncherButton;

		private int mainWindowId;
		private Rect mainWindowRect;
		private bool mainWindowVisible = false;
		private Vector2 mainWindowScrollPosition;

		private int testWindowId;
		private Rect testWindowRect;
		private bool testWindowVisible = false;
		private Vector2 testWindowScrollPosition;

		private string defaultSkinName;

		private bool ovedriven = false;
		private PluginConfiguration config;

		public void Awake ()
		{
			if (_instance != null) {
				Destroy (this);
				return;
			}
			_instance = this;
		}

		public void Start ()
		{
			DontDestroyOnLoad (this);
			UI = new UICore.UICore ();
			skins = new Dictionary<string, string> ();

			config = PluginConfiguration.CreateForType<Overdrive> ();
			config.load ();

			mainWindowRect = config.GetValue<Rect> ("mainWindowRect", new Rect ((Screen.width - 400) / 2, Screen.height / 4, 400, 0));
			defaultSkinName = config.GetValue<string> ("defaultSkinName", "Overdrive");

			mainWindowId = GUIUtility.GetControlID (FocusType.Passive);
			testWindowId = GUIUtility.GetControlID (FocusType.Passive);

			mainWindowScrollPosition.Set (0, 0);

			testWindowScrollPosition.Set (0, 0);
			testWindowRect = new Rect ();
			testWindowRect.width = 400;
			testWindowRect.height = 300;
			testWindowRect.center = new Vector2 (Screen.width / 2, Screen.height / 2);

			GameEvents.onGUIApplicationLauncherReady.Add (OnAppLauncherReady);
			GameEvents.onGameSceneSwitchRequested.Add (OnSwitchRequested);

			//ConfigNode [] configs = GameDatabase.Instance.GetConfigNodes ("UICoreSkin");
			//foreach (ConfigNode conf in configs) {
			//	skins.Add (conf.GetValue ("Name"), conf);
			//}
			UrlDir.UrlConfig [] configs = GameDatabase.Instance.GetConfigs ("UICoreSkin");
			foreach (UrlDir.UrlConfig url in configs) {
				skins.Add (url.config.GetValue ("Name"), url.url);
				//Debug.Log ("over -");
				//Debug.Log (url.config);
				//Debug.Log (url.name);
				//Debug.Log (url.parent);
				//Debug.Log (url.ToString());
				//Debug.Log (url.type);
				//Debug.Log (url.url);
			}
			//Debug.Log ("over -");
			//Debug.Log (skins ["Overdrive"]);
			//Debug.Log (skins ["Blues"]);
		}

		public void OnDestroy ()
		{
			config.SetValue ("mainWindowRect", mainWindowRect);
			config.SetValue ("defaultSkinName", defaultSkinName);
			config.save ();

			if (_instance == this)
				_instance = null;
			
			GameEvents.onGUIApplicationLauncherReady.Remove (OnAppLauncherReady);
			GameEvents.onGameSceneSwitchRequested.Remove (OnSwitchRequested);
		}

		private void OnAppLauncherReady ()
		{
			if (appLauncherButton == null) {
				appLauncherButton = ApplicationLauncher.Instance.AddModApplication (
					ShowMainWindow,
					HideMainWindow,
					null,
					null,
					null,
					null,
					ApplicationLauncher.AppScenes.SPACECENTER |
					ApplicationLauncher.AppScenes.FLIGHT |
					ApplicationLauncher.AppScenes.MAPVIEW |
					ApplicationLauncher.AppScenes.TRACKSTATION,
					GameDatabase.Instance.GetTexture ("Overdrive/Textures/overdrive-off-icon", false)
				);
			}
		}

		private void OnSwitchRequested (GameEvents.FromToAction<GameScenes, GameScenes> ev) {
			if (appLauncherButton != null) {
				ApplicationLauncher.Instance.RemoveModApplication (appLauncherButton);
				appLauncherButton = null;
			}
			mainWindowVisible = false;
		}


		private void ShowMainWindow ()
		{
			mainWindowVisible = true;
			appLauncherButton.SetTexture (GameDatabase.Instance.GetTexture ("Overdrive/Textures/overdrive-icon", false));
		}

		private void HideMainWindow ()
		{
			mainWindowVisible = false;
			appLauncherButton.SetTexture (GameDatabase.Instance.GetTexture ("Overdrive/Textures/overdrive-off-icon", false));
		}

		private void RenderMainWindow (int windowId)
		{
			GUILayout.BeginVertical ();
			UI.Layout.LabelCentered ("Presets");
			foreach (string skinName in skins.Keys) {
				if (UI.Layout.Button (skinName, GUILayout.Height(25))) {
					defaultSkinName = skinName;
					ovedriven = false;
				}
			}
			UI.Layout.HR ();
			if (UI.Layout.Button ("Retune Preset", GUILayout.Height (25)))
				ovedriven = false;

			if (UI.Layout.Button ("Open Test Window", GUILayout.Height (25)))
				testWindowVisible = true;

			if (UI.Layout.Button ("Close", GUILayout.Height (25)))
				appLauncherButton.SetFalse ();
			GUILayout.EndVertical ();
			GUI.DragWindow ();
		}

		private void RenderTestWindow (int windowId) {
			GUILayout.BeginVertical ();

			GUILayout.Box ("This is box");
			GUILayout.Label ("This is label");
			GUILayout.TextField ("This is textField");
			GUILayout.TextArea ("This is textArea\nWith Some text inside\nBlah blah blah");
			GUILayout.Button ("This is button");
			GUILayout.Toggle (true, "This is toggle");
			GUILayout.BeginScrollView (Vector2.zero, GUILayout.Height (100));
			GUILayout.Label ("This is scrollView");
			GUILayout.Label ("With a set of labels");
			GUILayout.Label ("To fill it");
			GUILayout.Label ("With some text");
			GUILayout.Button ("And also a button");
			GUILayout.Toggle (true, "And a toggle");
			GUILayout.EndScrollView ();
			UI.Layout.HR ();
			if (UI.Layout.Button ("Close", GUILayout.Height (25)))
				testWindowVisible = false;
			GUILayout.EndVertical ();
			GUI.DragWindow ();
		}

		public void OnGUI ()
		{
			if (mainWindowVisible) {
				mainWindowRect = UI.Layout.Window (
					mainWindowId,
					mainWindowRect,
					RenderMainWindow,
					"Overdrive",
					GUILayout.ExpandWidth (true),
					GUILayout.ExpandHeight (true)
				);
			}

			if (testWindowVisible) {
				testWindowRect = UI.Layout.Window (
					testWindowId,
					testWindowRect,
					RenderTestWindow,
					"Overdrive Test Window",
					GUILayout.ExpandWidth (false),
					GUILayout.ExpandHeight (false)
				);
			}

			if (!ovedriven)
			{
				UI.LoadConfig ("GameData/" + skins [defaultSkinName].Replace ("/UICoreSkin", ".cfg"));
				OverdriveUnity (UI.Skin);
				OverdriveKSP (UI.Skin);

				ovedriven = true;
			}
			//Destroy (this); // Quit after initialized
		}

		internal static void OverdriveUnity (Skin skin)
		{
			GUI.skin.box = skin.Styles ["box"];
			GUI.skin.label = skin.Styles ["label"];
			GUI.skin.textField = skin.Styles ["textField"];
			GUI.skin.textArea = skin.Styles ["textArea"];
			GUI.skin.button = skin.Styles ["button"];
			GUI.skin.toggle = skin.Styles ["toggle"];
			GUI.skin.window = skin.Styles ["window"];
			GUI.skin.horizontalSlider = skin.Styles ["horizontalSlider"];
			GUI.skin.horizontalSliderThumb = skin.Styles ["horizontalSliderThumb"];
			GUI.skin.verticalSlider = skin.Styles ["verticalSlider"];
			GUI.skin.verticalSliderThumb = skin.Styles ["verticalSliderThumb"];
			GUI.skin.horizontalScrollbar = skin.Styles ["horizontalScrollbar"];
			GUI.skin.horizontalScrollbarThumb = skin.Styles ["horizontalScrollbarThumb"];
			GUI.skin.horizontalScrollbarLeftButton = skin.Styles ["horizontalScrollbarLeftButton"];
			GUI.skin.horizontalScrollbarRightButton = skin.Styles ["horizontalScrollbarRightButton"];
			GUI.skin.verticalScrollbar = skin.Styles ["verticalScrollbar"];
			GUI.skin.verticalScrollbarThumb = skin.Styles ["verticalScrollbarThumb"];
			GUI.skin.verticalScrollbarUpButton = skin.Styles ["verticalScrollbarUpButton"];
			GUI.skin.verticalScrollbarDownButton = skin.Styles ["verticalScrollbarDownButton"];
			GUI.skin.scrollView = skin.Styles ["scrollView"];
		}

		/// <summary>
		/// Overrides the KSP skin.
		/// </summary>
		internal static void OverdriveKSP (Skin skin)
		{
			HighLogic.Skin.box = skin.Styles ["box"];
			HighLogic.Skin.label = skin.Styles ["label"];
			HighLogic.Skin.textField = skin.Styles ["textField"];
			HighLogic.Skin.textArea = skin.Styles ["textArea"];
			HighLogic.Skin.button = skin.Styles ["button"];
			HighLogic.Skin.toggle = skin.Styles ["toggle"];
			HighLogic.Skin.window = skin.Styles ["window"];
			HighLogic.Skin.horizontalSlider = skin.Styles ["horizontalSlider"];
			HighLogic.Skin.horizontalSliderThumb = skin.Styles ["horizontalSliderThumb"];
			HighLogic.Skin.verticalSlider = skin.Styles ["verticalSlider"];
			HighLogic.Skin.verticalSliderThumb = skin.Styles ["verticalSliderThumb"];
			HighLogic.Skin.horizontalScrollbar = skin.Styles ["horizontalScrollbar"];
			HighLogic.Skin.horizontalScrollbarThumb = skin.Styles ["horizontalScrollbarThumb"];
			HighLogic.Skin.horizontalScrollbarLeftButton = skin.Styles ["horizontalScrollbarLeftButton"];
			HighLogic.Skin.horizontalScrollbarRightButton = skin.Styles ["horizontalScrollbarRightButton"];
			HighLogic.Skin.verticalScrollbar = skin.Styles ["verticalScrollbar"];
			HighLogic.Skin.verticalScrollbarThumb = skin.Styles ["verticalScrollbarThumb"];
			HighLogic.Skin.verticalScrollbarUpButton = skin.Styles ["verticalScrollbarUpButton"];
			HighLogic.Skin.verticalScrollbarDownButton = skin.Styles ["verticalScrollbarDownButton"];
			HighLogic.Skin.scrollView = skin.Styles ["scrollView"];
		}
	}
}

