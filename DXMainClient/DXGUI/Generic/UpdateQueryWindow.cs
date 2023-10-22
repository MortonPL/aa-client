using ClientCore;
using ClientGUI;
using ClientCore.Extensions;
using Microsoft.Xna.Framework;
using Rampastring.XNAUI;
using Rampastring.XNAUI.XNAControls;
using System;

namespace DTAClient.DXGUI.Generic
{
    /// <summary>
    /// A window that asks the user whether they want to update their game.
    /// </summary>
    public class UpdateQueryWindow : INItializableWindow
    {
        public delegate void UpdateAcceptedEventHandler(object sender, EventArgs e);
        public event UpdateAcceptedEventHandler UpdateAccepted;

        public delegate void UpdateDeclinedEventHandler(object sender, EventArgs e);
        public event UpdateDeclinedEventHandler UpdateDeclined;

        public UpdateQueryWindow(WindowManager windowManager) : base(windowManager) { }

        private XNALabel lblDescription;
        private XNALabel lblUpdateSize;

        private string changelogUrl;

        public override void Initialize()
        {
            changelogUrl = ClientConfiguration.Instance.ChangelogURL;

            Name = "UpdateQueryWindow";
            ClientRectangle = new Rectangle(0, 0, 251, 140);
            BackgroundTexture = AssetLoader.LoadTexture("updatequerybg.png");

            base.Initialize();

            lblDescription = FindChild<XNALabel>(nameof(lblDescription));

            XNALinkLabel lblChangelogLink;
            lblChangelogLink = FindChild<XNALinkLabel>(nameof(lblChangelogLink));
            lblChangelogLink.LeftClick += LblChangelogLink_LeftClick;

            lblUpdateSize = FindChild<XNALabel>(nameof(lblUpdateSize));

            XNAClientButton btnYes;
            btnYes = FindChild<XNAClientButton>(nameof(btnYes));
            btnYes.ClientRectangle = new Rectangle(12, 110, 75, 23);
            btnYes.LeftClick += BtnYes_LeftClick;

            XNAClientButton btnNo;
            btnNo = FindChild<XNAClientButton>(nameof(btnNo));
            btnNo.LeftClick += BtnNo_LeftClick;

            CenterOnParent();
        }

        private void LblChangelogLink_LeftClick(object sender, EventArgs e)
        {
            ProcessLauncher.StartShellProcess(changelogUrl);
        }

        private void BtnYes_LeftClick(object sender, EventArgs e)
        {
            UpdateAccepted?.Invoke(this, e);
        }

        private void BtnNo_LeftClick(object sender, EventArgs e)
        {
            UpdateDeclined?.Invoke(this, e);
        }

        public void SetInfo(string version, int updateSize)
        {
            lblDescription.Text = string.Format(("Version {0} is available for download.\nDo you wish to install it?").L10N("Client:Main:VersionAvailable"), version);
            if (updateSize >= 1000)
                lblUpdateSize.Text = string.Format("The size of the update is {0} MB.".L10N("Client:Main:UpdateSizeMB"), updateSize / 1000);
            else
                lblUpdateSize.Text = string.Format("The size of the update is {0} KB.".L10N("Client:Main:UpdateSizeKB"), updateSize);
        }
    }
}
