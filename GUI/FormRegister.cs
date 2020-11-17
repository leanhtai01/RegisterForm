using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class FormRegister : Form
    {
        private UserBUS userBUS;
        private ErrorProvider errorProvider;

        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            userBUS = new UserBUS();
            textBoxPassword.Text = "";
            textBoxPassword.PasswordChar = '*';
            errorProvider = new ErrorProvider();
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            string pattern = @"^([a-z]+|[0-9]+)+([a-z]+|[0-9]+)+([a-z]+|[0-9]+)+$";

            if (!validation.IsValid(textBoxPassword.Text, pattern))
            {
                //MessageBox.Show("Password chỉ gồm các ký tự: a-z, 0-9 và tối thiểu 3 ký tự.",
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider.SetError(textBoxPassword, "Password chỉ gồm các ký tự: a-z, 0-9 và tối thiểu 3 ký tự.");
                textBoxPassword.Focus();
            }
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            string pattern = @"^([a-z]+(([a-z]|[0-9]|[-_.])+)?)$";

            if (!validation.IsValid(textBoxUsername.Text, pattern) ||
                userBUS.IsExistUsername(textBoxUsername.Text))
            {
                //MessageBox.Show("Username phải duy nhất, ko có ký tự HOA, bắt đầu bởi ký tự a-z," +
                //    " không khoảng trắng và các ký tự đặc biệt(chỉ gồm a - z, 0 - 9, -, _, .)",
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider.SetError(textBoxUsername, "Username phải duy nhất, ko có ký tự HOA, bắt đầu bởi ký tự a-z," +
                    " không khoảng trắng và các ký tự đặc biệt(chỉ gồm a - z, 0 - 9, -, _, .)");
                textBoxUsername.Focus();
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            string pattern = @"^([A-Za-z]+.*?)$";

            if (!validation.IsValid(textBoxName.Text, pattern))
            {
                //MessageBox.Show("Name không được bắt đầu bởi ký số hay ký tự đặc biệt.",
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider.SetError(textBoxName, "Name không được bắt đầu bởi ký số hay ký tự đặc biệt.");
                textBoxName.Focus();
            }
        }

        private void textBoxEmail_Leave(object sender, EventArgs e)
        {
            Validation validation = new Validation();
            string pattern = @"^(([a-z]+(([a-z]|[0-9]|[.])+)?)[@][a-z]+[.][a-z]+)$";

            if (!validation.IsValid(textBoxEmail.Text, pattern))
            {
                //MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.",
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider.SetError(textBoxEmail, "Email không hợp lệ. Vui lòng nhập lại.");
                textBoxEmail.Focus();
            }
        }

        private void textBoxBirthDate_Leave(object sender, EventArgs e)
        {
            DateTime dateTime;

            if (!DateTime.TryParseExact(textBoxBirthDate.Text, "dd/mm/yyyy", 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, out dateTime))
            {
                //MessageBox.Show("Ngày không hợp lệ!",
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorProvider.SetError(textBoxBirthDate, "Ngày không hợp lệ!");
                textBoxBirthDate.Focus();
            }
        }

        private UserDTO GetUser()
        {
            return new UserDTO
            {
                ID = userBUS.CreateID(),
                Username = textBoxUsername.Text,
                Password = textBoxPassword.Text,
                Name = textBoxName.Text,
                Email = textBoxEmail.Text,
                BirthDate = DateTime.ParseExact(textBoxBirthDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Permission = 0
            };
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (userBUS.AddUser(GetUser()))
            {
                MessageBox.Show("Registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
