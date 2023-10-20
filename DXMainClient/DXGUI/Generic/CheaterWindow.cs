using ClientGUI;
using System;
using Rampastring.XNAUI;
using Rampastring.XNAUI.XNAControls;
using Microsoft.Xna.Framework;
using ClientCore.Extensions;

namespace DTAClient.DXGUI.Generic
{
    public class CheaterWindow : INItializableWindow
    {
        public CheaterWindow(WindowManager windowManager) : base(windowManager)
        {
        }

        public event EventHandler YesClicked;

        public override void Initialize()
        {
            Name = "CheaterScreen";
            ClientRectangle = new Rectangle(0, 0, 334, 453);
            BackgroundTexture = AssetLoader.LoadTexture("cheaterbg.png");

            base.Initialize();

            XNAClientButton btnCancel;
            btnCancel = FindChild<XNAClientButton>(nameof(btnCancel));
            btnCancel.LeftClick += BtnCancel_LeftClick;

            XNAClientButton btnYes;
            btnYes = FindChild<XNAClientButton>(nameof(btnYes));
            btnYes.LeftClick += BtnYes_LeftClick;
        }

        private void BtnCancel_LeftClick(object sender, EventArgs e)
        {
            Disable();
        }

        private void BtnYes_LeftClick(object sender, EventArgs e)
        {
            Disable();
            YesClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
