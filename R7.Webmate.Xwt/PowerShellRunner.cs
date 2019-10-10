//
//  PowerShellRunner.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2019 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace R7.Webmate.Xwt
{
    public class PowerShellRunner
    {
        public string TestScript (string text)
        {
            using (PowerShell psInstance = PowerShell.Create ()) {
                psInstance.AddScript (
                   "param($Text); Write-Output $Text;"
                );

                psInstance.AddParameter ("Text", text);
                Collection<PSObject> psOutput = psInstance.Invoke ();

                foreach (PSObject outItem in psOutput) {
                    Console.WriteLine (outItem.BaseObject.ToString ());
                }

                return psInstance.Streams.Error.Count.ToString ();
            }

            //return "No PS instance!";
        }

        public string RunScript (string script, string text)
        {
            using (PowerShell psInstance = PowerShell.Create ()) {
                psInstance.AddScript (
                    "param($Text)\n"
                    + "cd Scripts\n"
                    + "Import-Module ./TextToHtml.ps1\n"
                    + "Invoke-TextToText -Text $Text\n"
                );

                psInstance.AddParameter ("Text", text);

                Collection<PSObject> psOutput = psInstance.Invoke ();
                if (psInstance.Streams.Error.Count == 0) {
                    foreach (PSObject outItem in psOutput) {
                        if (outItem != null) {
                            return outItem.BaseObject.GetType ().ToString ();
                        }
                    }
                }
                else {
                    return psInstance.Streams.Error.Count.ToString ();
                }


            }
            return null;
        }
    }
}
