#!/usr/bin/pwsh

function Invoke-TextToText
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[string] $Text
	)
	process {
		$t = $_
		
		# normalize endlines
		$t = $t -replace "\r\n", "\n"
		$t = $t -replace "\r", "\n"
		
		# remove excess empty lines (more than \n\n)
		#$t = $t -replace "\n{3,}", "\n\n"
		
		# replace tabs with spaces
		$t = $t -replace "\t", " "
		
		# replace excess composite puntuation like !!!! => !!!
		#$t = $t -replace "([\.!?]){3,}", "$1$1$1"
		
		# add spaces after punctuation
		#$t = $t -replace "[\.,;:\)\]\}\?!]+\s?", "$0 "
		
		# remove spaces before "closing" punctuation
		#$t = $t -replace "\s+([\.,;:\)\]\}\?!])", "$1"
		
		# remove extra punctuation before closing parenthesis
		$t = $t -replace "\.\)\.", ".)"
		$t = $t -replace "!\)!", "!)"
		$t = $t -replace "\?\)\?", "?)"
		
		# remove duplicate whitespace
		$t = $t -replace "\s+", " "
		
		# remove hyphens
		$t = $t -replace "¬", ""
		
		# replace figure quotes in text output
		$t = $t -replace "«", '"'
		$t = $t -replace "»", '"'
		
		# fix some common typos
		$t = $t -replace "г\.г\.", "гг."
		$t = $t -replace "с[\\/]х", "с.-х."
		$t = $t -replace "с\.х\.", "с.-х."
		
		$t = $t.Trim()
		
		Write-Output $t
	}
}

function Invoke-TextToHtml
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory=$true, ValueFromPipeline=$true)]
		[string] $Text
	)
	process {
		$t = $_
		
		# enclose all text in the para
		$t = "<p>" + $t + "</p>"
		
		# replace endlines with paras
		$t = $t -replace "\r\n", "</p><p>"
		$t = $t -replace "\n", "</p><p>"
		
		# remove spaces before and after para tags
		$t = $t -replace "<p> ", "<p>"
		$t = $t -replace " </p>", "</p>"
		
		# remove duplicate whitespace
		$t = $t -replace "\s+", " "
		
		# remove empty paras
		$t = $t -replace "<p></p>", ""
		#$t = $t -replace "<p>\u00A0</p>", ""
		$t = $t -replace "<p>&#160;</p>", ""
		$t = $t -replace "<p>&nbsp;</p>", ""
		
		# ...
		
		Write-Output $t
	}
}