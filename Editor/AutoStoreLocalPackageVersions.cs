/*
	This script will update /Packages/localpackageversions.json with names
	and versions of your local packages, assuming the packages stored in
	<ProjectPath>/../PackageRepos/

	You could make this fancier by finding the local repo paths in the
	manifest.json file but then you can't use JsonUtility, and this really
	should stay simple.
*/

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

[InitializeOnLoad]
public class AutoStoreLocalPackageVersions
{

	static AutoStoreLocalPackageVersions()
	{
		AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
	}

	static void OnAfterAssemblyReload()
	{
		var projectDirectory = Directory.GetParent( Application.dataPath );
		string packageReposPath = projectDirectory.Parent + "/PackageRepos";
		string[] packagePaths = Directory.GetDirectories( packageReposPath );
		List<PackageJson> packages = new List<PackageJson>();
		foreach( var p in packagePaths ){
			string packageJsonFilePath = p + "/package.json";
			if( File.Exists( packageJsonFilePath ) ) {
				try {
					var packageJson = JsonUtility.FromJson<PackageJson>( File.ReadAllText( packageJsonFilePath ) );
					packages.Add( packageJson );
				} catch {}
			}
		}

		var localPackageVersions = new LocalPackageVersions(){ packages = packages.ToArray() };
		string localPackageVersionsJson = JsonUtility.ToJson( localPackageVersions, prettyPrint: true );
		string packagesDirectoryPath = projectDirectory + "/Packages";
		string localPackageVersionsFilePath = packagesDirectoryPath + "/localpackageversions.json";
		File.WriteAllText( localPackageVersionsFilePath, localPackageVersionsJson );
	}

	[System.Serializable]
	public class PackageJson
	{
		public string name;
		public string version;
	}

	[System.Serializable]
	public class LocalPackageVersions
	{
		public PackageJson[] packages;
	}
}