#!/usr/bin/pwsh

param (
	[string] $OutputPath,
	[string] $XwtPackageVersion
)

$PackagesPath = "../packages"

Copy-Item -Path "$PackagesPath/Xwt.Gtk3.$XwtPackageVersion/lib/net472/Xwt.Gtk3.dll" -Destination $OutputPath -PassThru
Copy-Item -Path "$PackagesPath/Xwt.Gtk3.$XwtPackageVersion/build/Xwt.Gtk3.dll.config" -Destination $OutputPath -PassThru
Copy-Item -Path "$PackagesPath/Xwt.Gtk.$XwtPackageVersion/lib/net472/Xwt.Gtk.dll" -Destination $OutputPath -PassThru
Copy-Item -Path "$PackagesPath/Xwt.Gtk.Windows.$XwtPackageVersion/lib/net472/Xwt.Gtk.Windows.dll" -Destination $OutputPath -PassThru
Copy-Item -Path "$PackagesPath/Xwt.WPF.$XwtPackageVersion/lib/net472/Xwt.WPF.dll" -Destination $OutputPath -PassThru
