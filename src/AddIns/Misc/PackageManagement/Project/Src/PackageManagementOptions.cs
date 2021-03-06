﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ICSharpCode.Core;
using NuGet;

namespace ICSharpCode.PackageManagement
{
	public class PackageManagementOptions
	{
		const string PackageDirectoryPropertyName = "PackagesDirectory";
		const string RecentPackagesPropertyName = "RecentPackages";

		RegisteredPackageSourceSettings registeredPackageSourceSettings;
		Properties properties;
		ObservableCollection<RecentPackageInfo> recentPackages;
		PackageRestoreConsent packageRestoreConsent;
		
		public PackageManagementOptions(Properties properties, ISettings settings)
		{
			this.properties = properties;
			registeredPackageSourceSettings = new RegisteredPackageSourceSettings(settings);
			packageRestoreConsent = new PackageRestoreConsent(settings);
		}
		
		public PackageManagementOptions(Properties properties)
			: this(properties, Settings.LoadDefaultSettings(null, null, null))
		{
		}
		
		public PackageManagementOptions()
			: this(PropertyService.NestedProperties("PackageManagementSettings"))
		{
		}
		
		public bool IsPackageRestoreEnabled {
			get { return packageRestoreConsent.IsGrantedInSettings; }
			set { packageRestoreConsent.IsGrantedInSettings = value; }
		}
		
		public RegisteredPackageSources PackageSources {
			get { return registeredPackageSourceSettings.PackageSources; }
		}
		
		public string PackagesDirectory {
			get { return properties.Get(PackageDirectoryPropertyName, "packages"); }
			set { properties.Set(PackageDirectoryPropertyName, value); }
		}
		
		public PackageSource ActivePackageSource {
			get { return registeredPackageSourceSettings.ActivePackageSource; }
			set { registeredPackageSourceSettings.ActivePackageSource = value; }
		}
		
		public IList<RecentPackageInfo> RecentPackages {
			get {
				if (recentPackages == null) {
					ReadRecentPackages();
				}
				return recentPackages;
			}
		}
		
		void ReadRecentPackages()
		{
			IReadOnlyList<RecentPackageInfo> savedRecentPackages = properties.GetList<RecentPackageInfo>(RecentPackagesPropertyName);
			
			recentPackages = new ObservableCollection<RecentPackageInfo>();
			recentPackages.AddRange(savedRecentPackages);
			recentPackages.CollectionChanged += RecentPackagesCollectionChanged;
		}

		void RecentPackagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			properties.SetList(RecentPackagesPropertyName, recentPackages);
		}
	}
}
