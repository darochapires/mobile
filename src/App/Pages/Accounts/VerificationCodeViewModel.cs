﻿using System;
using Bit.App.Abstractions;
using Bit.App.Resources;
using Bit.Core.Abstractions;
using Bit.Core.Utilities;
using System.Threading.Tasks;
using Bit.Core.Exceptions;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using Bit.App.Utilities;
#if !FDROID
using Microsoft.AppCenter.Crashes;
#endif

namespace Bit.App.Pages
{
    public class VerificationCodeViewModel : BaseViewModel
    {
        private readonly IDeviceActionService _deviceActionService;
        private readonly IPlatformUtilsService _platformUtilsService;
        private readonly IUserVerificationService _userVerificationService;
        private readonly IApiService _apiService;
        private readonly IVerificationActionsFlowHelper _verificationActionsFlowHelper;

        private bool _showPassword;
        private string _secret;
        private string _secretName;

        public VerificationCodeViewModel()
        {
            _deviceActionService = ServiceContainer.Resolve<IDeviceActionService>("deviceActionService");
            _platformUtilsService = ServiceContainer.Resolve<IPlatformUtilsService>("platformUtilsService");
            _userVerificationService = ServiceContainer.Resolve<IUserVerificationService>("userVerificationService");
            _apiService = ServiceContainer.Resolve<IApiService>("apiService");
            _verificationActionsFlowHelper = ServiceContainer.Resolve<IVerificationActionsFlowHelper>("verificationActionsFlowHelper");

            PageTitle = AppResources.VerificationCode;

            TogglePasswordCommand = new Command(TogglePassword);
            MainActionCommand = new AsyncCommand(MainActionAsync, allowsMultipleExecutions: false);
        }

        public bool ShowPassword
        {
            get => _showPassword;
            set => SetProperty(ref _showPassword, value,
                additionalPropertyNames: new string[] { nameof(ShowPasswordIcon) });
        }

        public string Secret
        {
            get => _secret;
            set => SetProperty(ref _secret, value);
        }

        public string SecretName
        {
            get => _secretName;
            set => SetProperty(ref _secretName, value);
        }

        public ICommand TogglePasswordCommand { get; }

        public ICommand MainActionCommand { get; }

        public string ShowPasswordIcon => ShowPassword ? "" : "";

        public void TogglePassword() => ShowPassword = !ShowPassword;

        public async Task InitAsync()
        {
            await RequestOTPAsync();
        }

        public async Task RequestOTPAsync()
        {
            try
            {
                await _deviceActionService.ShowLoadingAsync(AppResources.Sending);
                await _apiService.PostAccountRequestOTP();
                await _deviceActionService.HideLoadingAsync();
                _platformUtilsService.ShowToast(null, null, AppResources.CodeSent);
            }
            catch (ApiException e)
            {
                await _deviceActionService.HideLoadingAsync();
                if (e?.Error != null)
                {
                    await _platformUtilsService.ShowDialogAsync(e.Error.GetSingleMessage(),
                        AppResources.AnErrorHasOccurred);
                }
            }
            catch (Exception ex)
            {
#if !FDROID
                Crashes.TrackError(ex);
#endif
                await _deviceActionService.HideLoadingAsync();
            }
        }

        private async Task MainActionAsync()
        {
            try
            {
                await _deviceActionService.ShowLoadingAsync(AppResources.Verifying);

                if (!await _userVerificationService.VerifyUser(Secret, Core.Enums.VerificationType.OTP))
                {
                    return;
                }

                await _deviceActionService.HideLoadingAsync();

                var parameters = _verificationActionsFlowHelper.GetParameters();
                parameters.Secret = Secret;
                await _verificationActionsFlowHelper.ExecuteAsync(parameters);

                Secret = string.Empty;
            }
            catch (ApiException e)
            {
                await _deviceActionService.HideLoadingAsync();
                if (e?.Error != null)
                {
                    await _platformUtilsService.ShowDialogAsync(e.Error.GetSingleMessage(),
                        AppResources.AnErrorHasOccurred);
                }
            }
            catch (Exception ex)
            {
#if !FDROID
                Crashes.TrackError(ex);
#endif
                await _deviceActionService.HideLoadingAsync();
            }
        }
    }
}
