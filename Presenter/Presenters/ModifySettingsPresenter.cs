﻿using System;
using Model;
using Presenter.Common;
using Presenter.Views;
using Presenter.Presenters;

namespace Presenter.Presenters
{
    class ModifySettingsPresenter : BasePresenterControl<IModifySettingsView>
    {
        private AppSettings _appSettings = null;

        public ModifySettingsPresenter(IApplicationController controller, IModifySettingsView view)
            : base(controller, view)
        {
            View.CommandLineHelpClick += View.ShowCommandLineHelp;
            View.GitHubSiteClick += View.OpenGitHubSite;
            View.ApplyMatrixSizeClick += ModifyMatrixSize;
            View.ModifyNameViewClick += ModifyNameView;
            View.AlertSetupClick += AlertSetup;
        }

        public void SetSettings(AppSettings appSettings)
        {
            _appSettings = appSettings;
            View.MatrixX = appSettings.matrix.cntX;
            View.MatrixY = appSettings.matrix.cntY;
        }
        public event Action MatrixSizeChanged;
        public event Action NameViewChanged;
        public event Action AlertSettingsChanged;

        private void ModifyMatrixSize()
        {
            _appSettings.matrix.cntX = View.MatrixX;
            _appSettings.matrix.cntY = View.MatrixY;
            MatrixSizeChanged?.Invoke();
        }

        private void ModifyNameView()
        {
            NameView nv = new NameView();
            _appSettings.nameView.SaveTo(nv);
            Controller.Run<NameViewSetupPresenter, NameView>(nv);
            if(!_appSettings.nameView.Equals(nv))
            {
                _appSettings.nameView = nv;
                NameViewChanged?.Invoke();
            }
        }

        private void AlertSetup()
        {
            Alert alert = new Alert();
            _appSettings.alert.SaveTo(alert);
            Controller.Run<AlertSetupPresenter, Alert>(alert);
            if (!_appSettings.alert.Equals(alert))
            {
                _appSettings.alert = alert;
                AlertSettingsChanged?.Invoke();
            }
        }

    }
}
