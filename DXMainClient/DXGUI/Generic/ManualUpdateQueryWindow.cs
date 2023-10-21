using System;
using ClientCore;
using ClientCore.Extensions;
using ClientGUI;
using ClientCore.Extensions;
using Microsoft.Xna.Framework;
using Rampastring.XNAUI;
using Rampastring.XNAUI.XNAControls;

namespace DTAClient.DXGUI.Generic
{
    /// <summary>
    /// A window that redirects users to manually download an update.
    /// </summary>
    public class ManualUpdateQueryWindow : INItializableWindow
    {
        public delegate void ClosedEventHandler(object sender, EventArgs e);
        public event ClosedEventHandler Closed;

        public ManualUpdateQueryWindow(WindowManager windowManager) : base(windowManager) { }

        private XNALabel lblDescription;

        private string downloadUrl;

        public override void Initialize()
        {
            Name = "ManualUpdateQueryWindow";
            ClientRectangle = new Rectangle(0, 0, 251, 140);
            BackgroundTexture = AssetLoader.LoadTexture("updatequerybg.png");

            base.Initialize();

            lblDescription = FindChild<XNALabel>(nameof(lblDescription));

            XNAClientButton btnDownload;
            btnDownload = FindChild<XNAClientButton>(nameof(btnDownload));
            btnDownload.LeftClick += BtnDownload_LeftClick;

            XNAClientButton btnClose;
            btnClose = FindChild<XNAClientButton>(nameof(btnClose));
            btnClose.LeftClick += BtnClose_LeftClick;

            CenterOnParent();
        }

        private void BtnDownload_LeftClick(object sender, EventArgs e)
            => ProcessLauncher.StartShellProcess(downloadUrl);

        private void BtnClose_LeftClick(object sender, EventArgs e)
            => Closed?.Invoke(this, e);

        public void SetInfo(string version, string downloadUrl)
        {
            this.downloadUrl = downloadUrl;
            lblDescription.Text = string.Format(lblDescription.Text, version);
        }
    }
}