# UniDi
[Documentation](https://github.com/UniDi/unidi.github.io)

UniDi is a Dependency Injection container for Unity and a continuation of [Zenject/Extenject](https://github.com/Mathijs-Bakker/Extenject).
The UniDi is a project that seeks to refactor Zenject in order to:

* Simplify maintenance
* Encourage [contributions]()
* Split the work between multiple developers
* Maximize extensibility

## Table Of Contents
<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<details>
<summary>Details</summary>

- [What is a DI container?](#what-is-a-di-container)
- [Installation](#installation)
  - [Unity Package Manager](#unity-package-manager)
  - [Manual Installation](#manual-installation)
  - [Install from file](#install-from-file)
  - [OpenUPM](#openupm)
- [Usage](#usage)
  - [Injection](#injection)
  - [Code example: Hello, World!](#code-example-hello-world)
- [Contributing](#contributing)
- [Credits](#credits)
- [License](#license)

</details>
<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## What is a DI container?
A DI Container is a software library that provides DI functionality and automates many of the tasks involved in Object Composition, Interception, and Lifetime Management. It’s an engine that resolves and manages object graphs.

## Installation

You can choose posibilities to install UniDi into your Unity project:
1. [Unity Package Manger](#unity-package-manager)
Probably the easiest way. Just add the git URL and let the package manager install it for you.
1. [Manual Installation](#manual-installation)
Edit the project manifest file by hand.
1. [Instal from a File](#Install-from-file)
Download a tarball and install it as a package.
1. [OpenUPM](#openupm) 
OpenUPM is currently not yet supported.

### Unity Package Manager
Open the Package Manager (UPM) in Unity ``Windows -> Package Manager``.

Select ``+`` in the top-left of the UPM panel and select ``Add package from Git URL...``

Enter ``https://github.com/UniDi/UniDi.git`` in the text box and click add.

More info: [Unity Manual: Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

| Syntax: | URL example |  
|---|---|
| Latest default branch	| "https://github.com/UniDi/UniDi.git" |  
| Specific branch| "https://github.com/UniDi/UniDi.git/#branch-name" |
| Specific version (tag) | "https://github.com/UniDi/UniDi.git#v.0.0.1" |
| Commit hash |	"https://github.com//UniDi/UniDi.git#9e72f9d5a6a3da49..." |

### Manual Installation
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
  {
    {
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

### Injection
are also several ways of having these dependencies injected into your classes. These are:

#### Constructor Injection
```
public class Foo
{
    IBar _bar;

    public Foo(IBar bar)
    {
        _bar = bar;
    }
}
```

#### Field Injection
```
public class Foo
{
    [Inject]
    IBar _bar;
}
```
Field injection occurs immediately after the constructor is called. All fields that are marked with the [Inject] attribute are looked up in the container and given a value. Note that these fields can be private or public and injection will still occur.

#### Property Injection
```
public class Foo
{
    [Inject]
    public IBar Bar
    {
        get;
        private set;
    }
}
```
Property injection works the same as field injection except is applied to C# properties. Just like fields, the setter can be private or public in this case.

#### Method Injection
```
public class Foo
{
    IBar _bar;
    Qux _qux;

    [Inject]
    public void Init(IBar bar, Qux qux)
    {
        _bar = bar;
        _qux = qux;
    }
}
```
Method Inject injection works very similarly to constructor injection.

### Code example: Hello, World!

This code example logs a 'Hello, World!' in the console.

```
using UniDi;
using UnityEngine;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello, World!");
        Container.Bind<Greeter>().AsSingle().NonLazy();
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}
```
If you want to follow all the steps of this example, you can consult the docs TODO:[here]()

## Contributing
Contributing is welcome! Create a *draft* or *PR*.
[Contributing guidelines](https://github.com/UniDi/UniDi/CONTRIBUTING.md)

## Credits
TODO: Include a section for credits in order to highlight and link to the authors of your project.

## License
UniDi contributions are licensed under the Apache 2.0 license, except for contributions copied from Extenject. See [LICENSE](https://github.com/UniDi/UniDi/blob/master/LICENSE.md) for details.
