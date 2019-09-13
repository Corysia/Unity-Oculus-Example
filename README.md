# Unity-Oculus-Example
An example of how to use Oculus Integration 1.40 in Unity

## Purpose
There have been a lot of changes to the VR landscape in the last few months.  As of this writing, there are many tutorials out there today that show how to get started, but each one is slightly out of date.  This project pulls together all the things I know about getting a starter project up and running with:

* Animated hands
* Thumbstick locomotion
* Geometry collision
* Simple grabbing of objects

Below I have listed each step I've made to create this project.  This is the recipe I use whenever I start something new.  I'll continue to update this periodically because I am sure that I will be referring back to this often, myself!

## Requirements
* Unity 2019.1.14f1 - This will probably work with other versions, but this is the version I am working with.
* Oculus Integration 1.40 - **Verify this is the version you're getting from the Asset Store.**   If there is a newer one out, these instructions may not work.  You can find older versions at the [Oculus Unity Integration Archive](https://developer.oculus.com/downloads/package/unity-integration-archive/"Title").  At the top of the page, where the version number is, you'll find a drop-down arrow.  Use that to select the version.

## Project Setup
Initially, this was intended to be an example for the Oculus Quest, but except for some specific Android options, the instructions are identical for the Rift and Rift S.

What follows is a step-by-step guide to re-create what I've done in this Unity project.

Start with a new Unity 3D Project.  I have not yet been able to get a VR Lightweight RP project to work on the Quest.

---
#### Rift / Rift S changes

* Import `Oculus Integraion 1.40`
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
* Import `Oculus Integraion 1.40`
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
* Open the Lighting tab
	* `Window -> General -> Lighthing Settings`
	* Under `Environment Lighting` change `Ambient Mode` to `Baked`.

## Oculus Integration
Now that we have the basic scene set up, we can start adding the Oculus things.

---
### Set up the Player Controller
* Delete the main camera.
* Find the `OVRPlayerController` prefab.  It's easiest to type `ovrplayer` in to the search field.
	* Drag and drop it in to your hiearchy.
	* Set the position to `2.5, 1, 0`
	* Set the rotation to `0, -90, 0`

> ***QUEST ONLY*** 
>
>  In your hierarchy, expand the `OVRPlayerController` and find the `OVRCameraRig`.  
> Locate the `OVRManager` panel 
> 
> 	* 	Change the value of `Element 0` in `Target Devices` from `GearVR or Go` to `Quest`.  
> 	*	Ensure `Use Recommended MSAA Level` is checked.
>  * 	Change `Tracking` to `Floor Level`
>  *	Ensure all of the following are checked:
> 		*	`Use Position Tracking`
> 		*	`Use IPD in Position Tracking`
> 		* 	`Reset Tracker On Load`
> 		*	`Allow Recenter`
> 		* 	`Reorient HMD on Controller Recenter`

At this point, build and run your project.  You should be able to:

* Smoothly move with the left thumbstick.
* Turn with the right thumbstick.

---
### Adding Collision Detection
* Click on the `OVRPlayerController` in your Hierarchy
	* In the `Character Controller` section, change the radius to `0.2`
	* Scroll to the bottom and click `Add Component`
		* Add a `Character Camera Constraint`
		* Check `Enable Collision`
		* Check `Dynamic Height`
		* In your hierarchy, expand the `OVRPlayerController` and find the `OVRCameraRig`.  Drag and drop it in to the `Camera Rig` field of the `Character Camera Constraints` script.


Build and run.  Now, you should not be able to walk through the `Pillar`.

---
### Adding Hands with LocalAvatar
I was hoping to be able to use the `LocalAvatarWithGrab` prefab, but the hands simply do not track correctly for me.  As a result, it's neccessary to modify the prefab for the `OVRPlayerController`.

As of Oculus Integration 1.39, it is necessary to have an App ID[^AppID] for your project in order to display your Oculus Avatar.  You can register your app with Oculus on [your Oculus Dashboard](https://dashboard.oculus.com).  Once you have an App ID for your project, you register it under the Oculus menu in `Oculus -> Avatars -> Edit Settings` and `Oculus -> Platform -> Edit Settings`

[^AppID]: You don't have to make a real project you intend to publish.  I've registered a `QuestTest` application to get an ID to experiment with.  Once I'm at the point of making a real app, I'll get a unique one for it.


This will then create three files:

* Assets/Resources/OvrAvatarSettings.asset
* Assets/Resources/OculusPlatformSettings.asset
* Assets/Resources/OculusPlatformSettings.asset.meta

The first two files will contain your App's ID, so take that in to consideration if you add your project to Source Control.  These should not be public!  I chose to add them to my `.gitignore` file.

* In your Heirarchy, expand your `OVRPlayerController` out until you can see the `TrackingSpace` underneath the `OVRCamperaRig`.  
* In your Assets folder, find the `LocalAvatar` prefab.
* Drag and drop `LocalAvatar` on top of the `TrackingSpace`.  Do not place it underneath it.  If you get a pop-up about modifying the prefab, you've done it wrong.  `TrackingSpace` should expand and you should see a `+LocalAvatar` at the bottom of its list.
* Find the `Ovr Avatar (Script)` component within `+LocalAvatar`
	* Un-check `Show Third Person`
	* Optionally, un-check `Can Own Microphone` because we won't be using the mic in this tutorial.

That's it!  Build and run.  You should have animated hands.

---
### Making the Sphere Grabbable
* Find your `Sphere` in your hierarchy
	* Add a `Rigidbody` component
	* Add an `OVRGrabbable` component

That's it for the sphere.  You can do this now with any other object you'd like to pick up (that isn't marked static).

### Allow Your Hands to Grab	
* Find `LeftHandAnchor` and `RightHandAnchor` under the `OVRPlayerController` in your heirarchy.
	* Select them both and add a `Sphere Collider`
		* Set the radius to `0.05`
		* Check the `Is Trigger` box
	* Add an `OVR Grabber` script to both
* Expand the `RightHandAnchor` and find the `RightControllerAnchor` under it.
	*  On the `RightHandAnchor`, drag and drop the `RightControllerAnchor` to the `Grip Transform` field
	*  Under `Grip Volumes` on the `RightHandAnchor`
		*  Set the size to `1`
		*  Drag the `Sphere Collider` of the `RightHandAnchor` in to this field.
	* In the `Controller` dropdown, select `R Touch`
* Do the same thing for the `LeftHandController` for the left hand.

When you start up your project now, you should be able to pick up the sphere.

---
### Colliding with Grabbed Objects
You may or may not have noticed that if you pick up the grabbed object and hug it to yourself, or place it under you, you'll be pushed around in the world.  To fix this, we need to adjust the collision matrix.

* Create a new layer called `Player`
* Create a new layer called `Grabbable`
* Under `Player Settings`, go to the `Physics` tab.
	* Scroll to the bottom of the panel until you see the matrix
	* Uncheck the intersection of `Player` and `Grabbable`

By doing this, these two layers won't trigger a collision event.

* Set the layer of the `Sphere` in your hierarchy as `Grabbable`
* Set the layer of the `OVRPlayerController` as `Player`. Do not recursively mark all child objects.  We only want the top object to be on the `Player` layer.

