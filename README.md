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

Below I have listed each step I've made to create this project.  This is the recipe I use whenever I start something new.  I'll continue to update this periodically because I am sure that I will be referring back to this often, myself!

Too long to read, you say?  Too much to type?  Just show you the project and binary?  You're not going to get very far in GameDev.  But...you can get the latest build under the [Releases](https://github.com/Corysia/Unity-Oculus-Example/releases) tab.

Having trouble cloning my project and getting it running?  Skip to the bottom and I'll tell you the steps.

## Project Setup
Initially, this was intended to be an example for the Oculus Quest, but except for some specific Android options, the instructions are identical for the Rift and Rift S.

What follows is a step-by-step guide to re-create what I've done in this Unity project.

Start with a new Unity 3D Project.  I have not yet been able to get a VR Lightweight RP project to work on the Quest.

---
#### Rift / Rift S changes

* Import `Oculus Integraion 1.38`
* Accept the updates (Oculus Utilities, Spatializer)
* `File -> Build Settings -> Player Settings`
	* 	Click `Add Open Scenes` to add `Scenes/SampleScene`
	* 	`XR Settings`
		*  Check `Virtual Reality Supported`
		*  Verify that `Oculus` appears first in the list, `OpenVR` is second.
		*  Set `Stereo Rendering Mode` to `Single Pass`
* Close the build settings window.

---
#### Quest changes

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
	* Change `<category android:name="android.intent.category.INFO"/>` to `<category android:name="android.intent.category.LAUNCHER"/>`. This step isn't necessary, but it gets rid of that 'boop' noise, removes some error messages, and auto-launches your app when you choose `build and deploy`.

---
## Scene Setup
The following steps are the same, regardless of the headset.

* Create a Cube at `0,0,0`
	* Rename the cube to `Floor`
	* Scale the `Floor` to `10, .001, 10`
	* Mark it `Static` because it will never move.  This will cause Unity to generate a baked lightmap.
* Create another cube 
	* Rename it to `Pillar`
	* Place it at `0, 0.5, 0`
	* Scale it to `0.1, 1, 0.1`
	* Mark it `Static`
* Create a Sphere
	* Place it at `0, 1.05, 0`
	* Scale it to `0.1, 0.1, 0.1`
* Create a new folder in `Assets` called `Materials`
* Create three new `Material`s and name them `Red`, `Green` and `Pale Blue`
* Set the color of `Green` to `0, 255, 0`
* Set the color of `Red` to `255, 0, 0`
* Set the color of `Pale Blue` to `0, 239, 255`
* Drag and drop `Red` on to the `Sphere`
* Drag and drop `Green` on to the `Pillar`
* Drag and drop `Pale Blue` on to the `Floor`

## Oculus Integration
Now that we have the basic scene set up, we can start adding the Oculus things.

---
#### Set up the Player Controller
* Delete the main camera.
* Find the `OVRPlayerController` prefab.  It's easiest to type `ovrplayer` in to the search field.
	* Drag and drop it in to your hiearchy.
	* Set the position to `2.5, 1, 0`
	* Set the rotation to `0, -90, 0`

> ***QUEST ONLY*** 
>
>  In your hierarchy, expand the `OVRPlayerController` and find the `OVRCameraRig`.  Locate the `OVRManager` panel and change the value of `Element 0` in `Target Devices` from `GearVR or Go` to `Quest`.  Also, check `Use Recommended MSAA Level`

At this point, you should be able to build and run.  You should be able to:

* Smoothly move with the left thumbstick.
* Turn with the right thumbstick.

> ***QUEST ONLY*** 
> 
> If you can't move or you just take a small step forward, check your AndroidManifest.xml file.  You may need to regenerate it.

---
#### Adding Collision Detection
* Click on the `OVRPlayerController` in your Hierarchy
	* In the `Character Controller` section, change the radius to `0.2`
	* Scroll to the bottom and click `Add Component`
		* Add a `Character Camera Constraint`
		* Check `Enable Collision`
		* Check `Dynamic Height`
		* In your hierarchy, expand the `OVRPlayerController` and find the `OVRCameraRig`.  Drag and drop it in to the `Camera Rig` field of the `Character Camera Constraints` script.


Now, you cannot walk through the `Pillar`.

---
#### Adding Hands with LocalAvatar
*TODO*

---
#### Making the Sphere Grabbable
*TODO*

## Building from this Source
* Checkout the project to your local system.
* Open it in Unity -- the same version of Unity I said at the beginning.
* Download and import the right version of Oculus Integration.
* Go through the upgrades Oculus will ask for.
* Shut down Unity.
* On the command line, type: `git reset --hard`.  If you don't know how to use the command line, there are other tutorials.
* Re-open the project in Unity
* Open up `Scenes/SampleScene` by double-clicking on it. 
* That's it.  You should be set to build a Rift / Rift S app.  If you're wanting to make a Quest app, you'll have to go in to the build settings and switch the platform to Android.  ...and then wait while Oculus Integration re-imports.  Even worse, texture compression is probably not set right.  Ah...the joys of being a Quest developer!  Also, ensure that the `OVRManager` is still set to `Quest`.  Oh, you also need to Remove and re-add the AndroidManifest.  And then change it to LAUNCHER.  

I'm going to have to clean this section up...

