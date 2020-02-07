using System;

namespace R7.Webmate.Xwt
{
    public class UuidGeneratorModel
    {
        public bool Uppercase { get; set; }

        public string GenerateUuid ()
        {
            var uuid = Guid.NewGuid ().ToString ();
            if (Uppercase) {
                uuid = uuid.ToUpperInvariant ();
            }

            return uuid;
        }
    }
}
