#!/usr/bin/pwsh

param ([string] $Text)

Import-Module ../Modules/Text/Text.psm1

"$Text" | Invoke-TextToText | Invoke-TextToHtml
