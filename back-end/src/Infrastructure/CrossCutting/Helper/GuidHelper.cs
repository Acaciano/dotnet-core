using System;

namespace Infrastructure.CrossCutting.Helper
{
    public static class GuidHelper
    {
        public static Guid TryParseGuid(this string value)
        {
            try
            {
                Guid result;
                Guid parseGuid = Guid.TryParse(value, out result) ? result : Guid.Empty;

                return parseGuid;
            }
            catch
            {
                return Guid.Empty;
            }
        }
    }
}
