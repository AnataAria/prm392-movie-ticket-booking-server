using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFEventOperation
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private readonly IAccountService _accountRepository;
        private bool _isCreateMode;

        public Window2(IAccountService accountRepository)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            _accountRepository = accountRepository;
            _ = LoadData();
        }

        private async Task LoadData()
        {
            this.dgData.ItemsSource = null;
            var accounts = await _accountRepository.GetAll();
            var accounts2 = accounts.Where(a => a.RoleId == 1 || a.RoleId == 2).Select(a => new { a.Id, a.Name, a.Address, a.Phone, a.RoleId, a.Status, a.Password, a.Email, a.Wallet });
            dgData.ItemsSource = accounts2;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnCcreateButton(object sender, RoutedEventArgs e)
        {
            if (_isCreateMode)
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                _isCreateMode = false;
                LoadData();
            }
            else
            {
                btnCreate.Visibility = Visibility.Visible;
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                _isCreateMode = true;
                dgData.ItemsSource = null;
            }
        }

        private async Task btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if(txtName.Text.Length  > 100)
            {
                MessageBox.Show("equal or over 100 charactics? too long!");
                return;
            }
            if (txtAdress.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtAdress.Text.Length > 255)
            {
                MessageBox.Show("equal or over 255 charactics? too long!");
                return;
            }
            if (txtPhone.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtPassword.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtPassword.Text.Length > 255)
            {
                MessageBox.Show("equal or over 255 charactics? too long!");
                return;
            }
            if (txtEmail.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                MessageBox.Show("must email!");
                return;
            }
            var phonePattern = @"^09\d{8}$";
            if (!Regex.IsMatch(txtPhone.Text, phonePattern))
            {
                MessageBox.Show("must phone!");
                return;
            }


            var id = await _accountRepository.GetAll();
            var id2 = id.Count();
            Account newAccount = new Account()
            {
                Id = id2 + 1,
                Name = txtName.Text,
                Address = txtAdress.Text,
                Phone = txtPhone.Text,
                RoleId = 2,
                Status = 1,
                Password = txtPassword.Text,
                Email = txtEmail.Text,
                Wallet = 0
            };
            var checkemail = await _accountRepository.GetAll();
            var checkemail2 = checkemail.FirstOrDefault(x => x.Email.Equals(txtEmail.Text));
            var checkphone = await _accountRepository.GetAll();
            var checkphone2 = checkphone.FirstOrDefault(x => x.Phone.Equals(txtPhone.Text));
            if ((checkemail2 == null) && (checkphone2 == null))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Do you want create new sponsor account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    await _accountRepository.Add(newAccount);
                    MessageBox.Show("create completed!", "Exit", MessageBoxButton.OK);
                    LoadData();
                }
                else
                {
                    MessageBox.Show($"create failed!");
                }
            }
            else
            {
                MessageBox.Show($" Email or Phone be used!");
            }
        }

        private async Task btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtWallet.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            var pattern = @"^\d+$";
            if (!Regex.IsMatch(txtWallet.Text, pattern))
            {
                MessageBox.Show("must number!");
                return;
            }
            var wallet =int.Parse(txtWallet.Text);
            var acc = await _accountRepository.GetById(int.Parse(txtId.Text));
            if ((wallet < 10000000) && (acc.Status == 1))
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Do you want update ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    var updateWallet = await _accountRepository.GetById(int.Parse(txtId.Text));
                    if (updateWallet == null)
                    {
                        MessageBox.Show($"Account Not Found!");
                        return;
                    }
                    updateWallet.Wallet = double.Parse(txtWallet.Text);
                    await _accountRepository.Update(updateWallet);
                    MessageBox.Show("update completed!", "Exit", MessageBoxButton.OK);
                    LoadData();
                }
                else
                {
                    MessageBox.Show($"update failed!");
                }
            }
            else
            {
                MessageBox.Show($"Account's wallet must be under 10 milion! Or Account banned!!!");
            }
        }

        private async Task btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var UpdateAccount = await _accountRepository.GetById(int.Parse(txtId.Text));
            if (UpdateAccount == null)
            {
                MessageBox.Show($"Account Not Found!");
                return;
            }
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want to ban this account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                UpdateAccount.Status = 0;
                UpdateAccount.Wallet = 0;
                _accountRepository.Update(UpdateAccount);
                MessageBox.Show("ban completed!", "Exit", MessageBoxButton.OK);
                LoadData();
            }
            else
            {
                MessageBox.Show($"ban failed!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            return;
        }
    }
}
