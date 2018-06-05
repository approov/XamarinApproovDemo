using System;
using System.Threading.Tasks;

namespace XamarinApproov
{
    public interface IApproover
    {
        void init();

        string fetchToken(string uri);

        Task<string> fetchTokenAsync(string uri);
    }
}
