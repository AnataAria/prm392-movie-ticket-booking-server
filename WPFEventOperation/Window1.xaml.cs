using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;

namespace WPFEventOperation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly IAccountService _accountRepository;
        private readonly IEventService _eventCategory;
        private readonly ICategoryService _categoryRepository;
        private readonly ITicketService _ticketRepository;
        private bool _isCreateMode;

        public Window1(IAccountService accountRepository, IEventService eventCategory, ICategoryService categoryRepository, ITicketService ticketRepository)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            _accountRepository = accountRepository;
            _eventCategory = eventCategory;
            _categoryRepository = categoryRepository;
            _ = LoadData();
            _ = bindcombo();
            _ticketRepository = ticketRepository;
        }

        private async Task LoadData()
        {
            this.dgData.ItemsSource = null;
            var accounts = await _eventCategory.GetAllIncludeType();
            dgData.ItemsSource = accounts.Where(a => a.Status == 1).Select(a => new { a.Id, a.Name, a.CategoryId, a.Category!.Type, a.TicketQuantity, a.Location, a.DateStart, a.DateEnd, a.Image });
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private async Task btnCcreateButton(object sender, RoutedEventArgs e)
        {
            if (_isCreateMode)
            {
                btnCreate.Visibility = Visibility.Collapsed;
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                _isCreateMode = false;
                await LoadData();
            }
            else
            {
                btnCreate.Visibility = Visibility.Visible;
                btnCreate.IsEnabled = true;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                _isCreateMode = true;
                dgData.ItemsSource = null;
                txtCategpryType.Text = null;
            }
        }

        private async Task btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txt.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txt.Text.Length > 255)
            {
                MessageBox.Show("equal or over 255? too long!");
                return;
            }
            if (txtCategpryType.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtTicketQuantity.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtLocation.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtLocation.Text.Length > 188)
            {
                MessageBox.Show("your filling is not like a location or country the longest city name which is: Krung Thep Mahanakhon Amon Rattanakosin Mahinthara Yuthaya Mahadilok Phop Noppharat Ratchathani Burirom Udomratchaniwet Mahasathan Amon Piman Awatan Sathit Sakkathattiya Witsanukam Prasit which is BANGKOK!");
                return;
            }
            if (txtDateStart.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtDateEnd.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtImage.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (!txtImage.Text.StartsWith("https://"))
            {
                MessageBox.Show("must have link image");
                return;
            }
            if(txtImage.Text.Length > 255)
            {
                MessageBox.Show("link too long!");
                return;
            }
            var checkTime = @"^([1-9]|1[0-2])/([1-9]|[12][0-9]|3[01])/((19|20)\d\d)$";
            if (!Regex.IsMatch(txtDateStart.Text, checkTime) || !Regex.IsMatch(txtDateEnd.Text, checkTime))
            {
                MessageBox.Show("you must fill the format MM/DD/YYYY with the real time!");
                return;
            }
            if (!int.TryParse(txtTicketQuantity.Text, out int quantity))
            {
                MessageBox.Show("must number!");
                return;
            }
            if(int.Parse(txtTicketQuantity.Text) <= 0)
            {
                MessageBox.Show("must not smaller or equal 0!");
                return;
            }
            if (int.Parse(txtTicketQuantity.Text) > 9000000)
            {
                MessageBox.Show("quantity ticket cannot over 9 million!");
                return;
            }
            var checkExist = _categoryRepository.getByCateName(txtCategpryType.Text);
            if (checkExist == null)
            {
                MessageBox.Show("Type Not Exist!");
                return;
            }

            if (DateTime.Parse(txtDateStart.Text) < DateTime.Now || DateTime.Parse(txtDateEnd.Text) < DateTime.Now)
            {
                MessageBox.Show("Event must be in future!");
                return;
            }
            if(DateTime.Parse(txtDateStart.Text) > DateTime.Parse(txtDateEnd.Text))
            {
                MessageBox.Show("Date End invalid");
                return;
            }
            var id = await _eventCategory.GetAll();
            var id2= id.Count();
            Event newEvent = new Event()
            {
                Id = id2 + 1,
                Name=txt.Text,
                CategoryId=checkExist.Id,
                TicketQuantity = int.Parse(txtTicketQuantity.Text),
                Location=txtLocation.Text,
                DateStart= DateOnly.Parse(txtDateStart.Text),
                DateEnd = DateOnly.Parse(txtDateEnd.Text),
                Image = txtImage.Text,
                Status = 1,
            };

            var ticketId = await _ticketRepository.GetAll();
            var countTicketId = ticketId.Count();
            Ticket ticket = new Ticket()
            {
                Id = countTicketId + 1,
                EventId = id2 + 1,
                Price = 20000,
                Quantity = int.Parse(txtTicketQuantity.Text),
                Status = 1,
            };
            var exist = await _eventCategory.GetAll();
            var exist2 = exist.FirstOrDefault(x => x.Name == newEvent.Name);
            if (exist2 == null)
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Do you want create ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    await _eventCategory.Add(newEvent);
                    await _ticketRepository.Add(ticket);
                    MessageBox.Show("create completed!", "Exit", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show($"create failed!");
                }
            }
            else
            {
                MessageBox.Show($"Event existed!");
            }
        }

        private async Task btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txt.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtCategpryType.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtTicketQuantity.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtLocation.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtDateStart.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtDateEnd.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtImage.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (!txtImage.Text.StartsWith("https://"))
            {
                MessageBox.Show("must have link image");
                return;
            }
            var checkTime = @"^([1-9]|1[0-2])/([1-9]|[12][0-9]|3[01])/((19|20)\d\d)$";
            if (!Regex.IsMatch(txtDateStart.Text, checkTime) || !Regex.IsMatch(txtDateEnd.Text, checkTime))
            {
                MessageBox.Show("you must fill the format MM/DD/YYYY with the real time!");
                return;
            }
            if (!int.TryParse(txtTicketQuantity.Text, out int quantity))
            {
                MessageBox.Show("must number!");
                return;
            }
            if (int.Parse(txtTicketQuantity.Text) <= 0)
            {
                MessageBox.Show("must not smaller or equal 0!");
                return;
            }
            var checkExist = _categoryRepository.getByCateName(txtCategpryType.Text); 
            if (checkExist == null)
            {
                MessageBox.Show("Type Not Exist!");
                return;
            }

            if (DateTime.Parse(txtDateStart.Text) < DateTime.Now || DateTime.Parse(txtDateEnd.Text) < DateTime.Now)
            {
                MessageBox.Show("Event must be in future!");
                return;
            }
            if (DateTime.Parse(txtDateStart.Text) > DateTime.Parse(txtDateEnd.Text))
            {
                MessageBox.Show("Date End invalid");
                return;
            }
            if (txt.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtTicketQuantity.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (txtLocation.Text.IsNullOrEmpty())
            {
                MessageBox.Show("please filed in");
                return;
            }
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want update ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                var UpdateEvent = await _eventCategory.GetById(int.Parse(txtId.Text));
                if (UpdateEvent == null)
                {
                    MessageBox.Show($"Event Not Found!");
                    return;
                }
                var UpdateTicket = await _ticketRepository.GetByEventIdAsync(UpdateEvent.Id);
                if (UpdateTicket == null)
                {
                    MessageBox.Show($"Ticket Not Found!");
                    return;
                }
                UpdateEvent.Name = txt.Text;
                UpdateEvent.CategoryId = checkExist.Id;
                UpdateEvent.TicketQuantity = int.Parse(txtTicketQuantity.Text);
                UpdateEvent.Location = txtLocation.Text;
                await _eventCategory.Update(UpdateEvent);

                foreach (var newiUpdateTicket in UpdateTicket)
                {
                    newiUpdateTicket.Quantity = int.Parse(txtTicketQuantity.Text);
                    await _ticketRepository.UpdateNewTicket(newiUpdateTicket);
                }
                MessageBox.Show("update completed!", "Exit", MessageBoxButton.OK);
                await LoadData();
            }
            else
            {
                MessageBox.Show($"update failed!");
            }

        }

        private async Task btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var UpdateEvent = await _eventCategory.GetById(int.Parse(txtId.Text));
            if (UpdateEvent == null)
            {
                MessageBox.Show($"Event Not Found!");
                return;
            }
            if (UpdateEvent.SponsorId != null)
            {
                MessageBox.Show("Can't delete due to having Sponsor");
                return;
            }
            if (MessageBoxResult.Yes == MessageBox.Show("Do you want delete ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                UpdateEvent.Status = 0;
                await _eventCategory.Update(UpdateEvent);
                MessageBox.Show("delete completed!", "Exit", MessageBoxButton.OK);
                await LoadData();
            }
            else
            {
                MessageBox.Show($"delete failed!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            return;
        }

        public List<Category> cates { get; set; }
        private async Task bindcombo()
        {
            var cate = await _categoryRepository.GetAll();
            var cate2 = cate.ToList();
            cates = cate2;
            DataContext = cate2;
        }

        private void cbCategoryType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategoryType.SelectedItem is Category cate)
            {
                txtCategpryType.Text = cate.Type;
            }
        }
    }
}
