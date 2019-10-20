------------------------------------------------------------------
2D Sky FREE 1.1.5
------------------------------------------------------------------

	2D Sky FREE contains low-res sprites and a demo scene, it is free version of 2D Sky.

	This pacakge is very easy to use, just copy sky from the demo scene and paste on your scene.

	Features:

		• Low-res hand-painted sunny sky sprites.
		• Components to control cloud.
		• Use Unity 2D Sprite features, no required for thirdparty 2d frameworks.
		• Realtime self strength background and self auto-tile large cloud for any screen resolution.

		• Support all build player platforms.
		
	Compatible:

		• Unity 5.5.1 or higher.

	Note:

		• This package is free version of 2D Sky. See below links for full version.
		• This package contains low quality textures (good enough for mobiles games).
		• This package uses dll to store the components.

	Product page:
	
		https://www.assetstore.unity3d.com/en/#!/content/21555

	Full Version:	
	
		https://www.assetstore.unity3d.com/en/#!/content/58833

	Please direct any bugs/comments/suggestions to geteamdev@gmail.com.

	Thank you for your support,

	Gold Experience Team
	E-mail: geteamdev@gmail.com
	Website: http://www.ge-team.com

------------------------------------------------------------------
Use demo scene
------------------------------------------------------------------

	1. Open Demo in "2D Sky FREE/Demo/Scenes/2D Sky FREE Demo (960x600px)".
	2. In Hierarchy tab, look for NearCloud, MidCloud, FarCloud objects.
	3. Select any of them, you will see GE2DSkyFREE_CloudFlow component in Inspector tab. GE2DSkyFREE_CloudFlow component does update position of children objects.

			Parameters:

				Camera:			An orthographic camera that renders clouds and background
				Tile:				Enable tile for large cloud
				Behavior:	
					- Flow Mixed Left Right	:	Randomly left/right direction for children
					- Flow to Left:				Children objects move to left, they will repeat from right edge when they get off from screen.
					- Flow to Right:			Children objects move to right, they will repeat from left edge when they get off from screen.
				Min Speed:		Minimum speed of children
				Max Speed:		Maximum speed of children
				Speed Multiplier:	Current speed multiplier

	4. Select an object names Sunny_01_sky.
	5. Look for GE2DSkyFREE_SkyBG component in Inspector tab, this component does resize Sunny_01_sky sprite to strength  fit on screen.

			Parameters:

				Camera:			An orthographic camera that renders clouds and background

------------------------------------------------------------------
Use cloud on your scene
------------------------------------------------------------------
	
	1. Open Demo in "2D Sky FREE/Demo/Scenes/2D Sky FREE Demo (960x600px)".
	2. Look for Sky object, copy it.
	3. Open your scene then paste it into your scene.
	4. Press play, you shoud see Sky and its children active same as in the 2D Sky FREE Demo (960x600px) scene.

------------------------------------------------------------------
Release notes
------------------------------------------------------------------

	Version 1.1.5

		• Update GUI Animator FREE to version 1.1.5.
		• Unity 5.5.1 or higher compatible.

	Version 1.1.0

		• Update GUI Animator FREE to version 1.1.0.
		• Unity 5.4.0 and higher compatible.

	Version 1.0.6

		• Update GUI Animator FREE to version 1.0.1.
		• Unity 4.7.1 and higher compatible.
		• Unity 5.3.4 and higher compatible.

	Version 1.0.5

		• Add real-time strength capability for background when screen resolution is changed.
		• Add real-time tile capability for large cloud when screen resolution is changed.
		• Add cloud Speed multiplier.
		• Add Full Screen toggle button.
		• Add Settings button and details panels.
		• Fixed GUID conflict with other packages.
		• Rearrange folders.
		• Update Demo scene.
		• Use component in dll instead of scripts.
		• Unity 4.6.9 and higher compatible.
		• Unity 5.3.2 and higher compatible.

	version 1.0 (Initial version)

		• Sunny sky sprites.
		• Scripts to control cloud.
		• Use Unity 2D features, doesn't other 2d frameworks.
		• Supports all player platforms.
		• Unity and Unity Pro compatible.
