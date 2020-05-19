using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenRedding.Client.Store.Common
{
    public static class StandardActionFactory
    {
        public static StandardAction FromType(string type) =>
            new StandardAction(type);

        public static StandardAction FromPayload(string type, object payload) =>
            new StandardAction(type)
    }
}
