using NAPS2_Alfresco.utils;
using System;
using System.Windows.Forms;

namespace NAPS2_Alfresco
{
    public partial class FLogin : Form
    {
        private Action<string> setTextAlfLogout;

        public FLogin(AuthInfo authInfo, Action<string> setTextAlfLogout)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.tbServiceUrl.Text = authInfo.serviceUrl;
            this.tbUsername.Text = authInfo.username;
            this.tbPassword.Text = authInfo.password;
            this.setTextAlfLogout = setTextAlfLogout;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.btnLogin.Enabled = false;
            string serviceUrl = this.tbServiceUrl.Text;
            string userName = this.tbUsername.Text;
            string password = this.tbPassword.Text;
            SessionUtils.Session = SessionUtils.CreateSession(serviceUrl, userName, password);
            if (SessionUtils.Session != null)
            {
                this.setTextAlfLogout(AlfDefs.TEXT_LOGOUT);
                this.Close();
                RegistryUtils.SetValue(AlfDefs.KEY_NAME_SERVICE_URL, serviceUrl);
                RegistryUtils.SetValue(AlfDefs.KEY_NAME_USERNAME, userName);
                RegistryUtils.SetValue(AlfDefs.KEY_NAME_PASSWORD, password);
                MessageBox.Show("Login Success", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Wrong username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.btnLogin.Enabled = true;
        }
    }
}
