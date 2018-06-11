using System;
using System.Threading.Tasks;

using XamarinApproov;

namespace XamarinApproov.iOS
{
    public class IosApproover : IApproover
    {
        public IosApproover()
        {
            // nothing here
        }

        public void init()
        {
            // nothing here
        }

        public string fetchToken(string uri)
        {
            // always fail for now

            return Approover.TOKEN_FAILURE;
        }

        public Task<string> fetchTokenAsync(string uri)
        {
            var tcs = new TaskCompletionSource<string>();

            // always fail for now

            string token = Approover.TOKEN_FAILURE;

            try
            {
                tcs.SetResult(token);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }

            return tcs.Task;
        }
    }
}
