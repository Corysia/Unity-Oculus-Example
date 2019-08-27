# Unity-Oculus-Example
An example of how to use Oculus Integration in VR

## Requirements
* Unity 2019.1.14f1 - This will probably work with other versions, but this is the version I am working with.
* Oculus Integration 1.38 - **DO NOT USE THE VERSION FROM THE UNITY ASSET STORE!**  Download from https://developer.oculus.com/downloads/package/unity-integration-archive/

## Purpose
There have been a lot of changes to the VR landscape in the last few months.  As of this writing, there are many tutorials out there today that show how to get started, but each one is slightly out of date.  This project pulls together all the things I know about getting a starter project up and running with:

* Animated hands
* Thumbstick locomotion
* Geometry collision
* Simple grabbing of objects

## Notes
Initially, this was intended to be an example for the Oculus Quest, but except for some specific Android options, the steps are identical.

What follows are the step-by-step instructions I did to make this project.

***Rift / Rift S changes***

* Import `Oculus Integraion 1.38`
* Accept the updates (Oculus Utilities, Spatializer)
* `File -> Build Settings -> Player Settings`
	* 	Click `Add Open Scenes` to add `Scenes/SampleScene`
	* 	`XR Settings`
		*  Check `Virtual Reality Supported`
		*  Verify that `Oculus` appears first in the list, `OpenVR` is second.
		*  Set `Stereo Rendering Mode` to `Single Pass`
* Close the build settings window.


***Quest changes***

* Switch the platform to `Android`
* Change Texture Compression to `ASTC`
* Import `Oculus Integraion 1.38`
* Accept the updates (Oculus Utilities, Spatializer)
* `File -> Build Settings -> Player Settings`
	* 	Click `Add Open Scenes` to add `Scenes/SampleScene`
	* 	XR Settings
		*  Check `Virtual Reality Supported`
		*  Add `Oculus` to the list.
		*  Set `Stereo Rendering Mode` to `Single Pass`
	*  Other Settings
		*  Color space `Linear`
		*  Remove `Vulkan` from `Graphics APIs`
		*  Set a package name.  This can be almost anything you want, but a domain name is the convention.  e.g., `com.example.demo`  It doesn't have to be real at this stage, but should be by the time you intend to publish.
		*  Set Minimum API level to `Android 7.1 'Nougat' (API level 25)`
		*  Change API Compatibility Level to `.Net 4.x`
	* `Quality`
		*	At the top, change the default for Android from `Medium` to `Low`.
	* `Graphics`
		* `Medium tier` - uncheck `Use Defaults` and select `Low`.
		* `High tier` - uncheck `Use Defaults` and select `Low`.
* Close the build settings window.
* Oculus -> Tools -> Remove AndroidManifest.xml
* Oculus -> Tools -> Create store-compatible AndroidManifest.xml
* Edit Assets/Android/AndroidManifest.xml
	* Change `<category android:name="android.intent.category.INFO"/>` to `<category android:name="android.intent.category.LAUNCHER"/>`

		
The following steps are the same, regardless of the headset.

* Delete the main camera.
* Create a Cube at `0,0,0`
	* Rename the cube to `Floor`
	* Scale the `Floor` to `10, .001, 10`
	
