using ClientCore;
using ClientGUI;
using ClientCore.Extensions;
using Microsoft.Xna.Framework;
using Rampastring.XNAUI;
using Rampastring.XNAUI.XNAControls;

namespace DTAClient.DXGUI.Generic
{
    /// <summary>
    /// A notification that asks the user to accept the CnCNet privacy policy.
    /// </summary>
    class PrivacyNotification : INItializableWindow
    {
        public PrivacyNotification(WindowManager windowManager) : base(windowManager)
        {
            // DrawMode = ControlDrawMode.UNIQUE_RENDER_TARGET;
        }

        public override void Initialize()
        {
            Name = nameof(PrivacyNotification);
            Width = WindowManager.RenderResolutionX;

            base.Initialize();

            XNALabel lblDescription;
            lblDescription = FindChild<XNALabel>(nameof(lblDescription));

            XNALinkLabel lblTermsAndConditions;
            lblTermsAndConditions = FindChild<XNALinkLabel>(nameof(lblTermsAndConditions));
            lblTermsAndConditions.LeftClick += (s, e) => ProcessLauncher.StartShellProcess(lblTermsAndConditions.Text);

            XNALinkLabel lblPrivacyPolicy;
            lblPrivacyPolicy = FindChild<XNALinkLabel>(nameof(lblPrivacyPolicy));
            lblPrivacyPolicy.LeftClick += (s, e) => ProcessLauncher.StartShellProcess(lblPrivacyPolicy.Text);

            XNALabel lblExplanation;
            lblExplanation = FindChild<XNALabel>(nameof(lblExplanation));

            XNAClientButton btnOK;
            btnOK = FindChild<XNAClientButton>(nameof(btnOK));
            btnOK.LeftClick += (s, e) =>
            {
                UserINISettings.Instance.PrivacyPolicyAccepted.Value = true;
                UserINISettings.Instance.SaveSettings();
                // AlphaRate = -0.2f;
                Disable();
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Alpha <= 0.0)
                Disable();
        }
    }
}
