# UniDi
UniDi is a Dependency Injection container for Unity based on Zenject.

## Table Of Contents
<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<details>
<summary>Details</summary>

    - [UniDi](#unidi)
    - [Table Of Contents](#table-of-contents)
    - [What is a DI container?](#what-is-a-di-container)
    - [Installation](#installation)
    - [Usage](#usage)
    - [Contributing](#contributing)
    - [Credits](#credits)
- [License](#license)

    </details>
    <!-- END doctoc generated TOC please keep comment here to allow auto update -->

## What is a DI container?
A DI Container is a software library that provides DI function- ality and automates many of the tasks involved in Object Composition, Interception, and Lifetime Management. It’s an engine that resolves and manages object graphs.

## Installation

###Install via Git URL
Open ``Packages/manifest.json`` with your favorite text editor. Add the following line to the dependencies block.
```json
{
    "dependencies": {
        "com.unidi.unidi": "https://github.com/unidi/unidi.git",
    }
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
### Install from file 
Download a release package to your computer. Press 'Add package from disk' in the Unity Package Manager and select the ``package.json`` in the extracted folder.

### OpenUPM
TODO: --> WIP <--

## Usage 
TODO

## Contributing
TODO

## Credits
TODO: Include a section for credits in order to highlight and link to the authors of your project.

## License
UniDi contributions are licensed under the Apache 2.0 license, except for contributions copied from Extenject. See [LICENSE](https://github.com/UniDi/UniDi/blob/master/LICENSE) for details.
