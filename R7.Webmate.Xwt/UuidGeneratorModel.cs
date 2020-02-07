using System;

namespace R7.Webmate.Xwt
{
    public class UuidGeneratorModel
    {
        public bool Uppercase { get; set; }

        public bool NoDashes { get; set; }

        public string GenerateUuid ()
        {
            var uuid = Guid.NewGuid ().ToString ();

            if (Uppercase) {
                uuid = uuid.ToUpperInvariant ();
            }

            if (NoDashes) {
                uuid = uuid.Replace ("-", string.Empty);
            }

            return uuid;
        }
    }
}
