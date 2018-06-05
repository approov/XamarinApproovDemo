using System;
using System.Threading.Tasks;

using Android.Content;
using Android.Util;

using XamarinApproov;
using Com.Criticalblue.Attestationlibrary;

namespace XamarinApproov.Droid
{

    public class TokenCB : Java.Lang.Object, ITokenInterface
    {
        TaskCompletionSource<string> tcs;

        public TokenCB(TaskCompletionSource<string> tcs)
        {
            this.tcs = tcs;
        }

        public void ApproovTokenFetchResult(TokenInterfaceApproovResults results)
        {
            string token = Approover.TOKEN_FAILURE;

            ApproovAttestation.AttestationResult result = results.Result;

            if (results.Result == ApproovAttestation.AttestationResult.Success)
            {
                token = results.Token;
            }

            try
            {
                tcs.SetResult(token);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }
        }
    }

    public class AndroidApproover : IApproover
    {

        private const string TAG = "APPRROOV_INIT";

        private Context context;

        public AndroidApproover(Context context)
        {

            this.context = context;

            // Initialize the Approov SDK
            try
            {
                // Creates the configuration object for the Approov SDK based
                // on the Android application context
                ApproovConfig config = ApproovConfig.GetDefaultConfig(context);
                ApproovAttestation.Initialize(config);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
            }

        }

        public void init()
        {
            // nothing here
        }

        public string fetchToken(string uri)
        {
            TokenInterfaceApproovResults
                              results = ApproovAttestation.Shared().FetchApproovTokenAndWait(uri);

            if (results.Result == ApproovAttestation.AttestationResult.Success)
            {
                return results.Token;
            }
            else
            {
                return Approover.TOKEN_FAILURE;
            }
        }

        public Task<string> fetchTokenAsync(string uri)
        {
            var tcs = new TaskCompletionSource<string>();
            var cb = new TokenCB(tcs);

            ApproovAttestation.Shared().FetchApproovToken(cb, uri);

            return tcs.Task;
        }
    }
}
