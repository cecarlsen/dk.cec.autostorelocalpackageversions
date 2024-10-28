
# Auto Store Local Package Versions

Unity's *Packages/manifest.json* does not contain versioning for local packages. In the case of projects that use multiple local packages each belonging to seperate repos, that are also updated outside the project, it is crucial to keep track their versions. When this package is present in your project it will store a list of local packages versions in use in *Packages/localpackageversions.json*.

## Installation

Install "Auto Store Local Package Versions" via Unity's Package Manager. Select "Install package from git URL..." and paste in:

	https://github.com/cecarlsen/dk.cec.autostorelocalpackageversions.git

If it doesn't work, check your error message and consult [this page](https://docs.unity3d.com/6000.0/Documentation/Manual/upm-ui-giturl.html).