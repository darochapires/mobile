[![Github Workflow build on master](https://github.com/bitwarden/mobile/actions/workflows/build.yml/badge.svg?branch=master)](https://github.com/bitwarden/mobile/actions/workflows/build.yml?query=branch:master)
[![Crowdin](https://d322cqt584bo4o.cloudfront.net/bitwarden-mobile/localized.svg)](https://crowdin.com/project/bitwarden-mobile)
[![Join the chat at https://gitter.im/bitwarden/Lobby](https://badges.gitter.im/bitwarden/Lobby.svg)](https://gitter.im/bitwarden/Lobby)

# Bitwarden Mobile Application

<a href="https://play.google.com/store/apps/details?id=com.x8bit.bitwarden" target="_blank"><img alt="Get it on Google Play" src="https://imgur.com/YQzmZi9.png" width="153" height="46"></a> <a href="https://mobileapp.bitwarden.com/fdroid/" target="_blank"><img alt="Get it on Google Play" src="https://i.imgur.com/HDicnzz.png" width="154" height="46"></a> <a href="https://itunes.apple.com/us/app/bitwarden-free-password-manager/id1137397744?mt=8" target="_blank"><img src="https://imgur.com/GdGqPMY.png" width="135" height="40"></a>

The Bitwarden mobile application is written in C# with Xamarin Android, Xamarin iOS, and Xamarin Forms.

<img src="https://raw.githubusercontent.com/bitwarden/brand/master/screenshots/mobile-android-myvault.png" alt="" width="325" height="650" /> <img src="https://raw.githubusercontent.com/bitwarden/brand/master/screenshots/mobile-ios-myvault.png" alt="" width="300" height="650" />

# Build/Run

**Requirements**

- [Visual Studio](https://visualstudio.microsoft.com/)
- [Xamarin](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows)

**Run the app**

- Open the solution file in Visual Studio.
- Restore the nuget packages.
- Build and run the app.

# We're Hiring!

Interested in contributing in a big way? Consider joining our team! We're hiring for many positions. Please take a look at our [Careers page](https://bitwarden.com/careers/) to see what opportunities are currently open as well as what it's like to work at Bitwarden.

# Contribute

Code contributions are welcome! Visual Studio with Xamarin is required to work on this project. Please commit any pull requests against the `master` branch.
Learn more about how to contribute by reading the [`CONTRIBUTING.md`](CONTRIBUTING.md) file.

Security audits and feedback are welcome. Please open an issue or email us privately if the report is sensitive in nature. You can read our security policy in the [`SECURITY.md`](SECURITY.md) file.

### Dotnet-format

We recently migrated to using dotnet-format as code formatter. All previous branches will need to updated to avoid large merge conflicts using the following steps:

1. Check out your local Branch
2. Run `git merge e0efcfbe45b2a27c73e9593bfd7a71fad2aa7a35`
3. Resolve any merge conflicts, commit.
4. Run `dotnet tool run dotnet-format`
5. Commit
6. Run `git merge -Xours 04539af2a66668b6e85476d5cf318c9150ec4357`
7. Push

#### Git blame

We also recommend that you configure git to ignore the prettier revision using:

```bash
git config blame.ignoreRevsFile .git-blame-ignore-revs
```
