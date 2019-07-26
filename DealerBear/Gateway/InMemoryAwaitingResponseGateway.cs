using System.Collections.Generic;
using DealerBear.Gateway.Interface;

namespace DealerBear.Gateway
{
    public class InMemoryAwaitingResponseGateway : IAwaitingResponseGateway
    {
        HashSet<string> _ids = new HashSet<string>();
        public bool HasID(string uid)
        {
            return _ids.Contains(uid);
        }

        public void PopID(string uid)
        {
            if (HasID(uid))
            {
                _ids.Remove(uid);
            }
        }

        public void SaveID(string uid)
        {
            _ids.Add(uid);
        }
    }
}