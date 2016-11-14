/*
 * UI Framework licensed under BSD 3-clause license
 * https://github.com/Real-Gecko/Unity5-UIFramework/blob/master/LICENSE.md
*/

using System;
using UnityEngine;

namespace Overdrive
{
	internal struct Palette {
		// Colors

		internal static Color white = new Color (1.0f, 1.0f, 1.0f);
		internal static Color dimWhite = new Color (0.9f, 0.9f, 0.9f, 0.9f);
		internal static Color black = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		internal static Color red = new Color (1.0f, 0.8f, 0.8f);
		internal static Color darkRed = new Color (0.7f, 0.4f, 0.4f);
		internal static Color green = new Color (0.6f, 1.0f, 0.8f);
		internal static Color darkGreen = new Color (0.4f, 0.7f, 0.4f);
		internal static Color blue = new Color (0.7f, 0.7f, 1.0f);
		internal static Color yellow = new Color (1.0f, 1.0f, 0.5f);
		internal static Color gray60 = new Color (0.6f, 0.6f, 0.6f);
		internal static Color gray50 = new Color (0.5f, 0.5f, 0.5f);
		internal static Color gray40 = new Color (0.4f, 0.4f, 0.4f);
		internal static Color gray30 = new Color (0.3f, 0.3f, 0.3f);
		internal static Color gray20 = new Color (0.2f, 0.2f, 0.2f);
		internal static Color gray10 = new Color (0.1f, 0.1f, 0.1f);
		internal static Color transparent = new Color (0.0f, 0.0f, 0.0f, 0.0f);

		// Color filled textures

		internal static Texture2D tBlack = new Texture2D (1, 1);
		internal static Texture2D tRed = new Texture2D (1, 1);
		internal static Texture2D tDarkRed = new Texture2D (1, 1);
		internal static Texture2D tGreen = new Texture2D (1, 1);
		internal static Texture2D tDarkGreen = new Texture2D (1, 1);
		internal static Texture2D tBlue = new Texture2D (1, 1);
		internal static Texture2D tYellow = new Texture2D (1, 1);
		internal static Texture2D tGray60 = new Texture2D (1, 1);
		internal static Texture2D tGray50 = new Texture2D (1, 1);
		internal static Texture2D tGray40 = new Texture2D (1, 1);
		internal static Texture2D tGray30 = new Texture2D (1, 1);
		internal static Texture2D tGray20 = new Texture2D (1, 1);
		internal static Texture2D tGray10 = new Texture2D (1, 1);
		internal static Texture2D tTransparent = new Texture2D (1, 1);

		/// <summary>
		/// 30 inside 60 outside
		/// </summary>
		internal static Texture2D tBorder = new Texture2D(4, 4);

		// Use for debuging
		//internal static Color dGray = new Color(0.5f, 0.5f, 0.5f);
		//internal static Color dBlue = new Color(0.5f, 0.5f, 1.0f);
		//internal static Color dGreen = new Color(0.5f, 1.0f, 0.5f);
		//internal static Color debug3 = new Color(0.5f, 1.0f, 1.0f);
		//internal static Color dRed = new Color(1.0f, 0.5f, 0.5f);
		//internal static Color debug5 = new Color(1.0f, 0.5f, 1.0f);
		//internal static Color debug6 = new Color(1.0f, 1.0f, 0.5f);
		//internal static Color dWhite = new Color(1.0f, 1.0f, 1.0f);
		//
		//internal static Texture2D tdGray = new Texture2D(1, 1);
		//internal static Texture2D tdBlue = new Texture2D(1, 1);
		//internal static Texture2D tdGreen = new Texture2D(1, 1);
		//internal static Texture2D tDebug3 = new Texture2D(1, 1);
		//internal static Texture2D tdRed = new Texture2D(1, 1);
		//internal static Texture2D tDebug5 = new Texture2D(1, 1);
		//internal static Texture2D tDebug6 = new Texture2D(1, 1);
		//internal static Texture2D tdWhite = new Texture2D(1, 1);

		internal static void InitPalette() {
			tBlack.SetPixel (0, 0, black);
			tBlack.Apply ();

			tRed.SetPixel (0, 0, red);
			tRed.Apply ();

			tDarkRed.SetPixel (0, 0, darkRed);
			tDarkRed.Apply ();

			tGreen.SetPixel (0, 0, green);
			tGreen.Apply ();

			tDarkGreen.SetPixel (0, 0, darkGreen);
			tDarkGreen.Apply ();

			tBlue.SetPixel (0, 0, blue);
			tBlue.Apply ();

			tYellow.SetPixel (0, 0, yellow);
			tYellow.Apply ();

			tGray60.SetPixel(0, 0, gray60);
			tGray60.Apply();

			tGray50.SetPixel(0, 0, gray50);
			tGray50.Apply();

			tGray40.SetPixel (0, 0, gray40);
			tGray40.Apply ();

			tGray30.SetPixel(0, 0, gray30);
			tGray30.Apply();

			tGray40.SetPixel (0, 0, gray40);
			tGray40.Apply ();

			tGray20.SetPixel(0, 0, gray20);
			tGray20.Apply();

			tGray10.SetPixel(0, 0, gray10);
			tGray10.Apply();

			tTransparent.SetPixel (0, 0, transparent);
			tTransparent.Apply ();

			tBorder.SetPixel (0, 0, gray60);
			tBorder.SetPixel (0, 1, gray60);
			tBorder.SetPixel (0, 2, gray60);
			tBorder.SetPixel (0, 3, gray60);
			tBorder.SetPixel (1, 0, gray60);
			tBorder.SetPixel (1, 1, gray30);
			tBorder.SetPixel (1, 2, gray30);
			tBorder.SetPixel (1, 3, gray60);
			tBorder.SetPixel (2, 0, gray60);
			tBorder.SetPixel (2, 1, gray30);
			tBorder.SetPixel (2, 2, gray30);
			tBorder.SetPixel (2, 3, gray60);
			tBorder.SetPixel (3, 0, gray60);
			tBorder.SetPixel (3, 1, gray60);
			tBorder.SetPixel (3, 2, gray60);
			tBorder.SetPixel (3, 3, gray60);

			// User for debuging
			//tdGray.SetPixel (0, 0, dGray);
			//tdGray.Apply ();
			//
			//tdBlue.SetPixel (0, 0, dBlue);
			//tdBlue.Apply ();
			//
			//tdGreen.SetPixel (0, 0, dGreen);
			//tdGreen.Apply ();
			//
			//tDebug3.SetPixel (0, 0, debug3);
			//tDebug3.Apply ();
			//
			//tdRed.SetPixel (0, 0, dRed);
			//tdRed.Apply ();
			//
			//tDebug5.SetPixel (0, 0, debug5);
			//tDebug5.Apply ();
			//
			//tDebug6.SetPixel (0, 0, debug6);
			//tDebug6.Apply ();
			//
			//tdWhite.SetPixel (0, 0, dWhite);
			//tdWhite.Apply ();
		}
	}
}

