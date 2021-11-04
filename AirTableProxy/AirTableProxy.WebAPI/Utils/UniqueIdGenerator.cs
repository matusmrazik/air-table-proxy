using System;

namespace AirTableProxy.WebAPI.Utils
{
    public static class UniqueIdGenerator
    {
        public static string Last { get; private set; } = null;

        public static string Generate()
        {
            Last = Guid.NewGuid().ToString("N");
            return Last;
        }
    }
}
