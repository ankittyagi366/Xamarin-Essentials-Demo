using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace XamarinEssentials_FullDemo.ViewModels
{
    public class ConnectivityInformationPageViewModel : BindableBase
    {

        //Add Permission in android manifest - "Access Network State" not required in iOS and UWP
        // <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />

        //Commands
        public DelegateCommand CheckConnectivityCommand { get; set; }

        //Properties
        IEnumerable<ConnectionProfile> profiles;
        NetworkAccess objNetworkAccess;

        //Services
        private IPageDialogService _dialogService;

        public ConnectivityInformationPageViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            CheckConnectivityCommand = new DelegateCommand(CheckInternetConnectivity);
            profiles = Connectivity.ConnectionProfiles;
            objNetworkAccess = Connectivity.NetworkAccess;
        }

        private void CheckInternetConnectivity()
        {
            Tuple<string,string> result = CheckConnectivity();
            _dialogService.DisplayAlertAsync(result.Item1, result.Item2, "Ok");
        }

        public Tuple<string, string> CheckConnectivity()
        {

            if (objNetworkAccess == NetworkAccess.Internet)
            {
                return Tuple.Create("Internet", Constants.NetworkConnectivityConstants.Internet);

            }
            else if (objNetworkAccess == NetworkAccess.ConstrainedInternet)
            {
                return Tuple.Create("Constrained Internet", Constants.NetworkConnectivityConstants.ConstrainedInternet);

            }
            else if (objNetworkAccess == NetworkAccess.Local)
            {
                return Tuple.Create("Local", Constants.NetworkConnectivityConstants.Local);

            }
            else if (objNetworkAccess == NetworkAccess.None)
            {
                return Tuple.Create("None", Constants.NetworkConnectivityConstants.None);

            }
            else
            {
                return Tuple.Create("Unknown", Constants.NetworkConnectivityConstants.Unknown);

            }
        }


    }
}
