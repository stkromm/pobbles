# Repository of Pobbles

# Getting Started

## Contribution Guide

### Github Desktop
If you don't want to fiddle around in the console use GitHub Desktop as the
preferred Git GUI
https://desktop.github.com/

### Console Guide
Clone the project
```
git clone https://github.com/stkromm/pobbles.git
```
or if you already cloned the project
```
git checkout master
git pull
```

Create your own development branch
```
git checkout -b dev-yourname
```

Commit and publish (do this for every change please, you only need to push if you want it on the master though)
```
git add --all
git commit -m "prefix: What have you changed?" # prefix should be feat, fix or refac depending on the change
git push
```

Update your branch, if someone else changed the master
```
git fetch
git pull origin master
```
Resolve the merge conflicts via IDE or GitHub desktop

Create a merge request to apply your changes on the master (do it via github)

## Development Environment

### Install Unity
https://store.unity.com/download?ref=personal

Visual Studio is the standard Unity IDE and has the best supported workflow with the unity editor. 
It's available on macOS and windows:
https://visualstudio.microsoft.com/de/thank-you-downloading-visual-studio/?sku=Community&rel=15

You're free to also use what ever IDE you like, just be sure to add local files
to the gitignore file if neccesary.

### Setting up local Firebase
Save *googe-service.json* for Android and/or *GoogleService-Info.plist* for iOS under Assets. You get them from the Apps in the Firebase Console
https://console.firebase.google.com/u/0/project/pobbles-dev/overview

You have to download the Firebase Unity SDK from 
https://firebase.google.com/download/unity

Go in the Unity Editor and import the Firebase SDK packages under dotnet4 in the Firebase SDK folder you downloaded.
You import a package through 
Assets > Import Package > Custom Package and select a package file (must be repeated for each Firebase SDK package)

### Selecting a Build
Open the project in Unity. The Unity Editor should be open.
Select File > Build Settings...

In the build settings you can select Platform.
Pobbles currently only supports Android or iOS.

### Build for Android
You need the Android SDK on your machine.
Best way to download it is by downloading Android Studio. This also installs
Android Emulator, which can be useful if you don't have an physical device available.
https://developer.android.com/studio/#downloads

Use Build in the Build Settings to create a APK.
If you run Build first time you may be prompted to point to the Android SDK folder.

### Build for iOS
Not tested yet.

For building the iOS client you need xCode.
https://developer.apple.com/xcode/
