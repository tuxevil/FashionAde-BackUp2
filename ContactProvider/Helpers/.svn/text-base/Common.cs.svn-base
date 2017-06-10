using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactProvider.Helpers
{
    class Common
    {
        public static int GetTimestamp()
        {
            DateTime refTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = DateTime.UtcNow - refTime;
            return (int)ts.TotalSeconds;
        }
    }
}
