using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.ExceptionCatcher
{
    /// <summary>
    /// Окно показа ошибки
    /// </summary>
    public class FormException : Form
    {
        private Panel pnHeader;
        private Button bttnContinue;
        private TextBox txtMessage;
        private Panel pnDetails;
        private Button bttnDetails;
        private HelpProvider helpProvider;
        private Button bttnQuit;
        private TextBox txtDetails;
        private Label lblHint2;
        private Label lblHint1;
        private ToolTip toolTipProvider;
        private IContainer components;

        public FormException(Exception ex)
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            txtMessage.Text = ex.Message;
            txtDetails.Text = ex.ToString();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.bttnDetails = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.bttnQuit = new System.Windows.Forms.Button();
            this.bttnContinue = new System.Windows.Forms.Button();
            this.lblHint2 = new System.Windows.Forms.Label();
            this.lblHint1 = new System.Windows.Forms.Label();
            this.pnDetails = new System.Windows.Forms.Panel();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.toolTipProvider = new System.Windows.Forms.ToolTip(this.components);
            this.pnHeader.SuspendLayout();
            this.pnDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnHeader
            // 
            this.pnHeader.Controls.Add(this.bttnDetails);
            this.pnHeader.Controls.Add(this.txtMessage);
            this.pnHeader.Controls.Add(this.bttnQuit);
            this.pnHeader.Controls.Add(this.bttnContinue);
            this.pnHeader.Controls.Add(this.lblHint2);
            this.pnHeader.Controls.Add(this.lblHint1);
            this.pnHeader.Dock = DockStyle.Top;
            this.pnHeader.Location = new Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(512, 200);
            this.pnHeader.TabIndex = 0;
            // 
            // bttnDetails
            // 
            this.bttnDetails.FlatStyle = FlatStyle.System;
            this.helpProvider.SetHelpString(this.bttnDetails, "Показывает или скрывает детальную информацию о нештатной ситуации");
            this.bttnDetails.Location = new Point(104, 168);
            this.bttnDetails.Name = "bttnDetails";
            this.helpProvider.SetShowHelp(this.bttnDetails, true);
            this.bttnDetails.Size = new System.Drawing.Size(112, 24);
            this.bttnDetails.TabIndex = 4;
            this.bttnDetails.Text = "Детальнее";
            this.toolTipProvider.SetToolTip(this.bttnDetails, "Показать детальное описание ошибки");
            this.bttnDetails.Click += new System.EventHandler(this._btnDetails_Click);
            // 
            // txtMessage
            // 
            this.helpProvider.SetHelpString(this.txtMessage, "Заголовок сообщения о нештатной ситуации");
            this.txtMessage.Location = new System.Drawing.Point(8, 16);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.helpProvider.SetShowHelp(this.txtMessage, true);
            this.txtMessage.Size = new System.Drawing.Size(488, 96);
            this.txtMessage.TabIndex = 1;
            this.txtMessage.Text = "";
            // 
            // bttnQuit
            // 
            this.bttnQuit.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.bttnQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.helpProvider.SetHelpString(this.bttnQuit, "Заканчивает работу клиентского приложения");
            this.bttnQuit.Location = new System.Drawing.Point(376, 168);
            this.bttnQuit.Name = "bttnQuit";
            this.helpProvider.SetShowHelp(this.bttnQuit, true);
            this.bttnQuit.Size = new System.Drawing.Size(112, 24);
            this.bttnQuit.TabIndex = 6;
            this.bttnQuit.Text = "Выйти";
            this.toolTipProvider.SetToolTip(this.bttnQuit, "Закончить работу приложения");
            // 
            // bttnContinue
            // 
            this.bttnContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.bttnContinue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.helpProvider.SetHelpString(this.bttnContinue, "Продолжает работу клиентского приложения");
            this.bttnContinue.Location = new System.Drawing.Point(240, 168);
            this.bttnContinue.Name = "bttnContinue";
            this.helpProvider.SetShowHelp(this.bttnContinue, true);
            this.bttnContinue.Size = new System.Drawing.Size(112, 24);
            this.bttnContinue.TabIndex = 5;
            this.bttnContinue.Text = "Продолжить";
            this.toolTipProvider.SetToolTip(this.bttnContinue, "Продолжить работу приложения");
            // 
            // lblHint2
            // 
            this.lblHint2.Location = new System.Drawing.Point(24, 144);
            this.lblHint2.Name = "lblHint2";
            this.lblHint2.Size = new System.Drawing.Size(464, 16);
            this.lblHint2.TabIndex = 3;
            this.lblHint2.Text = "Если Вы выберете \"Выйти\", то приложение закончит свою работу.";
            // 
            // lblHint1
            // 
            this.lblHint1.Location = new System.Drawing.Point(24, 128);
            this.lblHint1.Name = "lblHint1";
            this.lblHint1.Size = new System.Drawing.Size(464, 16);
            this.lblHint1.TabIndex = 2;
            this.lblHint1.Text = "Если Вы выберете \"Продолжить\", то приложение продолжит свою работу.";
            // 
            // pnDetails
            // 
            this.pnDetails.Controls.Add(this.txtDetails);
            this.pnDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDetails.DockPadding.All = 8;
            this.pnDetails.Location = new System.Drawing.Point(0, 200);
            this.pnDetails.Name = "pnDetails";
            this.pnDetails.Size = new System.Drawing.Size(512, 168);
            this.pnDetails.TabIndex = 1;
            // 
            // txtDetails
            // 
            this.txtDetails.AutoSize = false;
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpProvider.SetHelpString(this.txtDetails, "Детализованная информация о нештатной ситуации");
            this.txtDetails.Location = new System.Drawing.Point(8, 8);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.helpProvider.SetShowHelp(this.txtDetails, true);
            this.txtDetails.Size = new System.Drawing.Size(496, 152);
            this.txtDetails.TabIndex = 0;
            this.txtDetails.Text = "";
            // 
            // FormException
            // 
            this.AcceptButton = this.bttnContinue;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.bttnContinue;
            this.ClientSize = new System.Drawing.Size(512, 368);
            this.Controls.Add(this.pnDetails);
            this.Controls.Add(this.pnHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormException";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нештатная ситуация";
            this.Load += new System.EventHandler(this.FormException_Load);
            this.Activated += new System.EventHandler(this.FormException_Activated);
            this.pnHeader.ResumeLayout(false);
            this.pnDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public static bool Execute(Exception ex)
        {
            return Execute(ex, true);
        }

        public static bool Execute(Exception ex, bool canResume)
        {
            FormException form = new FormException(ex);
            form.bttnContinue.Enabled = canResume;
            return (form.ShowDialog() != DialogResult.Abort);
        }
        /// <summary>
        /// Показать детальное описание ошибки
        /// </summary>
        private void ShowDetails()
        {
            if(pnDetails.Visible)
            {
                pnDetails.Hide();
                Height -= pnDetails.Height;
            }
            else
            {
                Height += pnDetails.Height;
                pnDetails.Show();
            }
        }

        private void _btnDetails_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void FormException_Load(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void FormException_Activated(object sender, EventArgs e)
        {
            bttnContinue.Focus();
        }

    }
}