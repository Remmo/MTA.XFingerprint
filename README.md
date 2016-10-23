# MTA.XFingerprint

## Fingerprint Plugin for Xamarin (PCL/Android/iOS)

A simple and fast PCL Fingerprint Reader for Xamarin - Created by Mind the app http://www.mindtheapp.it/

### Android required permissions
```
	<uses-permission android:name="android.permission.USE_FINGERPRINT" />
```

### Setup
* Coming soon on Nuget 
* Install into your Xamarin Forms project and Xamarin Android/iOS projects.


**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|---|iOS 6+|
|Xamarin.iOS Unified|---|iOS 6+|
|Xamarin.Android|Yes|API 14+|
|Windows Phone Silverlight|---|---|
|Windows Phone RT|---|---|
|Windows Store RT|---|---|
|Windows 10 UWP|---|---|
|Xamarin.Mac|No||


### API Usage

Call **XFingerprint.Current** from any project or PCL to gain access to APIs.




**Library Creators**
Simply set this nuget as a dependency of your project to gain access to the current activity. This can be achieved by setting the following in your nuspec:

```
<dependencies>
  <group targetFramework="MonoAndroid10">
    <dependency id="MTA.CurrentActivity" version="1.0.1"/>
  </group>
</dependencies>
```

Daje!

#### License
Licensed under main repo license
