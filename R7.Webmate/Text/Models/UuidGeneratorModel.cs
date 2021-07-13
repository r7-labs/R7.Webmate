using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R7.Webmate.Text.Models
{
    public class UuidGeneratorModel
    {
        public IList<TextResult> Results = new List<TextResult> ();

        public int NumOfEntries { get; set; }

        public IList<Guid> GenerateUuids (int numOfEntries)
        {
            var uuids = new List<Guid> ();
            for (int i = 0; i < numOfEntries; i++) {
                uuids.Add (Guid.NewGuid ());
            }

            return uuids;
        }

        public string UuidToString (Guid uuid, bool upperCase, bool noDashes)
        {
            var uuidStr = uuid.ToString ();

            if (upperCase) {
                uuidStr = uuidStr.ToUpperInvariant ();
            }

            if (noDashes) {
                uuidStr = uuidStr.Replace ("-", string.Empty);
            }

            return uuidStr;
        }

        public void Process ()
        {
            Results.Clear ();

            var uuids = GenerateUuids (NumOfEntries);

            Results.Add (new TextResult {
                Text = string.Join ("\n", uuids.Select (u => UuidToString (u, false, false))),
                Label = "Default"
            });

            Results.Add (new TextResult {
                Text = string.Join ("\n", uuids.Select (u => UuidToString (u, true, false))),
                Label = "UPPERCASE"
            });

            Results.Add (new TextResult {
                Text = string.Join ("\n", uuids.Select (u => UuidToString (u, false, true))),
                Label = "No Dashes"
            });

            Results.Add (new TextResult {
                Text = string.Join ("\n", uuids.Select (u => UuidToString (u, true, true))),
                Label = "UPPERCASE, No Dashes"
            });
        }
    }
}
