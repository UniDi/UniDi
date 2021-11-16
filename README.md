# UniDi
UniDi is a Dependency Injection container for Unity based on Zenject.

## Table Of Contents
<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<details>
<summary>Details</summary>

- [What is a DI container?](#what-is-a-di-container)
- [Installation](#installation)
  - [Install via Git URL](#install-via-git-url)
  - [Install from file](#install-from-file)
  - [OpenUPM](#openupm)
- [Usage](#usage)
- [Contributing](#contributing)
- [Credits](#credits)
- [License](#license)

</details>
<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## What is a DI container?
A DI Container is a software library that provides DI functionality and automates many of the tasks involved in Object Composition, Interception, and Lifetime Management. It’s an engine that resolves and manages object graphs.

## Installation

### Install with Unity Package Manager
Open the Package Manager (UPM) in Unity ``Windows -> Package Manager``.

Select ``+`` in the top-left of the UPM panel and select ``Add package from Git URL...``

Enter ``https://github.com/UniDi/UniDi.git`` in the text box and click add.

More info: [Unity Manual: Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

|---|---|
| Syntax: | URL example |  
| Latest default branch	| "https://github.com/UniDi/UniDi.git" |  
| Specific branch| "https://github.com/UniDi/UniDi.git/#branch-name" |
| Specific version (tag) | "https://github.com/UniDi/UniDi.git#v.0.0.1" |
| Commit hash |	"https://github.example.com/myuser/myrepository.git#9e72f9d5a6a3dadc38d813d8399e1b0e86781a49" |

### Git URL Manual Installation
Open ``Packages/manifest.json`` with your favorite text editor. Add the following line to the dependencies block.
```json
{
    "dependencies": {
        "com.unidi.unidi": "https://github.com/unidi/unidi.git",
    },
    "testables": "com.unidi.unidi"
}
```
**Notice:** Unity Package Manager records the current commit to a lock entry of the manifest.json. To update to the latest version, change the ``"hash"`` value manually or just remove the lock entry to resolve the package.
```json
{
  "dependencies": {
    "com.unidi.unidi": {
      "version": "https://github.com/unidi/unidi.git",
      "depth": 0,
      "source": "git",
      "dependencies": {},
      "hash": "dd5027426f3dfecf2212fa7abe26365387ad080f"
    },
  }
}
```
TODO: [Unity Manual about ](https://docs.unity3d.com/Manual/upm-git.html)

### Install from file 
Download and extract a [release](https://github.com/UniDi/UniDi/releases) to your machine. Press 'Add package from disk' in the Unity Package Manager and select the ``package.json`` file in the extracted folder.

### OpenUPM
TODO: --> WIP <--

## Usage 
TODO

## Contributing
TODO

## Credits
TODO: Include a section for credits in order to highlight and link to the authors of your project.

## License
UniDi contributions are licensed under the Apache 2.0 license, except for contributions copied from Extenject. See [LICENSE](https://github.com/UniDi/UniDi/blob/master/LICENSE.md) for details.
