using System.Windows.Forms;

namespace ComTarget
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var web = new WebBrowser();
            web.Dock = DockStyle.Fill;
            Controls.Add(web);
        }
    }
}
