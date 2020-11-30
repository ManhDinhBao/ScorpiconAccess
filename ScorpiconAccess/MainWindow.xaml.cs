using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using Microsoft.Win32;
using ScorpiconAccess.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BUS_Card bus_Card;
        private BUS_CardHolder bus_CardHolder;
        private BUS_Device bus_Device;
        private BUS_DeviceSocket bUS_DeviceSocket;
        private ViewMode viewMode;
        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;
        private bool showLogPanel = false;

        private int currentMode = 2;
        List<DTO_ScorpionAccess.ListViewItem> listViewItems;

        public MainWindow()
        {
            InitializeComponent();
            viewMode = ViewMode.NULL;
            bus_Card = new BUS_Card();
            bus_CardHolder = new BUS_CardHolder();
            bus_Device = new BUS_Device();
            bUS_DeviceSocket = new BUS_DeviceSocket();
        }

        #region Menu Button Click
        private void btCard_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.CARD_VIEW);
        }

        private void btHolder_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.HOLDER_VIEW);
        }

        private void btDevice_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.DEVICE_VIEW);
        }

        private void btDoor_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.DOOR_VIEW);
        }

        private void btSchedule_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.SCHEDULE_VIEW);
        }

        private void btEvent_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.EVENT_VIEW);
        }
        #endregion

        private void BindCardToListItemView()
        {
            listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
            foreach (DTO_Card card in Repository.lstAllCards)
            {
                DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                if (card.Holder != null && card.Holder!="")
                {
                    listViewItem.ImageSource = "/Icon/card_gray_50px.png";
                }
                else
                {
                    listViewItem.ImageSource = "/Icon/card_blue_50px.png";
                }
                
                listViewItem.TextBinding = card.CardNumber;
                listViewItem.Key = card.CardSerial;

                listViewItems.Add(listViewItem);
            }
        }

        private void BindHolderToListItemView()
        {
            listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
            foreach (DTO_CardHolder holder in Repository.lstAllCardHolders)
            {
                DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                listViewItem.ImageSource = "/Icon/person_gray_30px.png";
                listViewItem.TextBinding = holder.Name;
                listViewItem.Key = holder.Id;

                listViewItems.Add(listViewItem);
            }
        }

        private void BindDeviceToListItemView()
        {
            listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
            foreach (DTO_Device device in Repository.lstAllDevices)
            {
                DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                listViewItem.ImageSource = "/Icon/device_gray_50px.png";
                listViewItem.TextBinding = device.Name;
                listViewItem.Key = device.Id;

                listViewItems.Add(listViewItem);
            }
        }

        private void UpdateView(ViewMode viewMode)
        {
            this.viewMode = viewMode;
            
            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    tbViewName.Text = "Card";
                    BindCardToListItemView();
                    
                    lbListItems.ItemsSource = listViewItems;
                    
                    break;
                case ViewMode.HOLDER_VIEW:
                    tbViewName.Text = "Holder";

                    BindHolderToListItemView();

                    lbListItems.ItemsSource = listViewItems;
                    break;
                case ViewMode.DEVICE_VIEW:
                    tbViewName.Text = "Device";

                    BindDeviceToListItemView();

                    lbListItems.ItemsSource = listViewItems;
                    break;
                case ViewMode.DOOR_VIEW:
                    tbViewName.Text = "Door";
                    break;
                case ViewMode.SCHEDULE_VIEW:
                    tbViewName.Text = "Schedule";
                    break;
                case ViewMode.RIGHT_VIEW:
                    tbViewName.Text = "Right";
                    break;
                case ViewMode.EVENT_VIEW:
                    tbViewName.Text = "Event";
                    break;
            }
            lbListItems.SelectedIndex = 0;
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbListItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.currentMode = CHANGE_MODE;

            DTO_ScorpionAccess.ListViewItem selectedItem = (DTO_ScorpionAccess.ListViewItem)lbListItems.SelectedItem;

            if (selectedItem != null)
            {
                switch (viewMode)
                {
                    case ViewMode.CARD_VIEW:
                        #region SHOW CARD DETAIL VIEW
                        DTO_Card selectedCard = Repository.lstAllCards.FirstOrDefault(card => card.CardSerial == selectedItem.Key);
                        Repository.selectedCard = selectedCard;

                        //Show card detail info
                        CardDetailView uc = new CardDetailView(mode: currentMode);
                        uc.HorizontalAlignment = HorizontalAlignment.Stretch;
                        uc.VerticalAlignment = VerticalAlignment.Stretch;
                        pnlData.Children.Clear();

                        pnlData.Children.Add(uc);

                        #endregion
                        break;
                    case ViewMode.HOLDER_VIEW:
                        #region SHOW HOLDER VIEW
                        DTO_CardHolder selectedHolder = Repository.lstAllCardHolders.FirstOrDefault(holder => holder.Id == selectedItem.Key);
                        Repository.selectedHolder = selectedHolder;

                        //Show holder detail info
                        HolderDetailView ucHolder = new HolderDetailView(mode: currentMode);
                        ucHolder.HorizontalAlignment = HorizontalAlignment.Stretch;
                        ucHolder.VerticalAlignment = VerticalAlignment.Stretch;
                        pnlData.Children.Clear();

                        pnlData.Children.Add(ucHolder);
                        #endregion
                        break;
                    case ViewMode.DEVICE_VIEW:
                        #region SHOW DEVICE DETAIL VIEW
                        DTO_Device selectedDevice = Repository.lstAllDevices.FirstOrDefault(deice => deice.Id == selectedItem.Key);
                        Repository.selectedDevice = selectedDevice;

                        //Show device detail info
                        DeviceDetailView ucDevice = new DeviceDetailView(mode: currentMode);
                        ucDevice.HorizontalAlignment = HorizontalAlignment.Stretch;
                        ucDevice.VerticalAlignment = VerticalAlignment.Stretch;
                        pnlData.Children.Clear();

                        pnlData.Children.Add(ucDevice);
                        #endregion
                        break;
                    case ViewMode.DOOR_VIEW:



                        //Show door detail info

                        break;
                    case ViewMode.SCHEDULE_VIEW:

                        //Show schedule detail info

                        break;
                    case ViewMode.RIGHT_VIEW:

                        //Show right detail info

                        break;
                    case ViewMode.EVENT_VIEW:

                        break;
                }
            }           
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            List<DTO_ScorpionAccess.ListViewItem> listViewItems;
            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
                    IEnumerable<DTO_Card> selectedCards;
                    if (tbSearchValue.Text.Count() <= 0)
                    {
                        selectedCards = Repository.lstAllCards;
                    }
                    else
                    {
                        selectedCards = Repository.lstAllCards.Where(card => card.CardNumber.Contains(tbSearchValue.Text));
                    }

                    if (selectedCards != null && selectedCards.Count() > 0)
                    {
                        foreach (DTO_Card card in selectedCards)
                        {
                            DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                            listViewItem.ImageSource = "/Icon/card_gray_50px.png";
                            listViewItem.TextBinding = card.CardNumber;
                            listViewItem.Key = card.CardSerial;

                            listViewItems.Add(listViewItem);
                        }
                    }

                    lbListItems.ItemsSource = listViewItems;

                    break;
                case ViewMode.HOLDER_VIEW:

                    listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
                    IEnumerable<DTO_CardHolder> selectedHolders;
                    if (tbSearchValue.Text.Count() <= 0)
                    {
                        selectedHolders = Repository.lstAllCardHolders;
                    }
                    else
                    {
                        selectedHolders = Repository.lstAllCardHolders.Where(holder => holder.Name.Contains(tbSearchValue.Text));
                    }

                    if (selectedHolders != null && selectedHolders.Count() > 0)
                    {
                        foreach (DTO_CardHolder holer in selectedHolders)
                        {
                            DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                            listViewItem.ImageSource = "/Icon/person_gray_30px.png";
                            listViewItem.TextBinding = holer.Name;
                            listViewItem.Key = holer.Id;

                            listViewItems.Add(listViewItem);
                        }
                    }

                    lbListItems.ItemsSource = listViewItems;

                    break;
                case ViewMode.DEVICE_VIEW:
                    
                    //Show device detail info

                    break;
                case ViewMode.DOOR_VIEW:

                    //Show door detail info

                    break;
                case ViewMode.SCHEDULE_VIEW:

                    //Show schedule detail info

                    break;
                case ViewMode.RIGHT_VIEW:

                    //Show right detail info

                    break;
                case ViewMode.EVENT_VIEW:

                    break;
            }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(tbSearchValue.Text))
                return true;
            else
                return ((item as DTO_Card).CardNumber.IndexOf(tbSearchValue.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void AddLog(UserLog log)
        {
            if (log != null)
            {
                Run run = new Run(log.LogString);
                SolidColorBrush brush;
                switch (log.LogType)
                {
                    case EType.UserLogType.LOG_TIME:
                        brush = new SolidColorBrush(Colors.Black);
                        break;
                    case EType.UserLogType.LOG_STATUS_ERROR:
                        brush = new SolidColorBrush(Colors.Crimson);
                        break;
                    case EType.UserLogType.LOG_STATUS_SUCCESS:
                        brush = new SolidColorBrush(Colors.DarkGreen);
                        break;
                    case EType.UserLogType.LOG_STATUS_WARNING:
                        brush = new SolidColorBrush(Colors.Yellow);
                        break;
                    default:
                        brush = new SolidColorBrush(Colors.Gray);
                        break;
                }

                run.Foreground = brush;
                Paragraph paragraph = new Paragraph(run);
                tbLog.Document.Blocks.Add(paragraph);
            }

        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    if (this.currentMode == CHANGE_MODE)
                    {
                        //Update card
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_Card cardToUpdate = Repository.selectedCard;
                        string strCardInfo = string.Format(" UPDATE CARD INFORMATION\r\n Serial: {0}\r\n Number: {1}\r\n Start Time: {2}\r\n End Time: {3}", cardToUpdate.CardSerial, cardToUpdate.CardNumber, cardToUpdate.STime.ToString("dd/MM/yyyy"), cardToUpdate.ETime.ToString("dd/MM/yyyy"));
                        AddLog(new UserLog(strCardInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_Card.UpdateCard(cardToUpdate);
                        string strReasultLog = "";
                        if (result.Result)
                        {
                            DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardSerial == cardToUpdate.CardSerial);
                            if (oldCard != null)
                            {
                                oldCard = cardToUpdate;
                            }
                            RefreshListView();
                            AddLog(new UserLog(" Update successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            strReasultLog = "Status: Error -> " + result.Detail;
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }
                    else
                    {
                        //Insert card
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_Card addCard = Repository.newCard;
                        string strCardInfo = string.Format(" INSERT NEW CARD\r\n Serial: {0}\r\n Number: {1}\r\n Start Time: {2}\r\n End Time: {3}", addCard.CardSerial, addCard.CardNumber, addCard.STime.ToString("dd/MM/yyyy"), addCard.ETime.ToString("dd/MM/yyyy"));
                        AddLog(new UserLog(strCardInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_Card.AddNewCard(addCard);
                        if (result.Result)
                        {
                            Repository.lstAllCards.Add(addCard);
                            RefreshListView();
                            AddLog(new UserLog(" Insert successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }
                    break;
                case ViewMode.HOLDER_VIEW:
                    if (this.currentMode == CHANGE_MODE)
                    {
                        //Update holder
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_CardHolder holderToUpdate = Repository.selectedHolder;
                        string strHolderInfo = string.Format(" UPDATE CARD HOLDER INFORMATION\r\n Id: {0}\r\n Name: {1}\r\n Gender: {2}\r\n DOB: {3}\r\n Address: {4}\r\n Phone: {5}\r\n Email: {6}\r\n Description: {7}"
                            , holderToUpdate.Id
                            , holderToUpdate.Name
                            , holderToUpdate.Gender
                            , holderToUpdate.DOB.ToString("dd/MM/yyyy")
                            , holderToUpdate.Address
                            , holderToUpdate.PhoneNumber
                            , holderToUpdate.Email
                            , holderToUpdate.Description);

                        AddLog(new UserLog(strHolderInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_CardHolder.UpdateCardHolder(holderToUpdate);
                        string strReasultLog = "";
                        if (result.Result)
                        {                            
                            DTO_CardHolder oldHolder = Repository.lstAllCardHolders.FirstOrDefault(h => h.Id == holderToUpdate.Id);
                            if (oldHolder != null)
                            {
                                oldHolder = holderToUpdate;
                            }
                            RefreshListView();
                            AddLog(new UserLog(" Update successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            strReasultLog = "Status: Error -> " + result.Detail;
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }
                    else
                    {
                        //Insert holder
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_CardHolder addHolder = Repository.newHolder;
                        string strHolderInfo = string.Format(" ADD NEW CARD HOLDER\r\n Id: {0}\r\n Name: {1}\r\n Gender: {2}\r\n DOB: {3}\r\n Address: {4}\r\n Phone: {5}\r\n Email: {6}\r\n Description: {7}"
                            , addHolder.Id
                            , addHolder.Name
                            , addHolder.Gender
                            , addHolder.DOB.ToString("dd/MM/yyyy")
                            , addHolder.Address
                            , addHolder.PhoneNumber
                            , addHolder.Email
                            , addHolder.Description);

                        AddLog(new UserLog(strHolderInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_CardHolder.AddNewCardHolder(addHolder);
                        if (result.Result)
                        {                           
                            Repository.lstAllCardHolders.Add(addHolder);
                            RefreshListView();
                            AddLog(new UserLog(" Insert successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }

                    break;
                case ViewMode.DEVICE_VIEW:
                    if (this.currentMode == CHANGE_MODE)
                    {
                        //Update device
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_Device deviceToUpdate = Repository.selectedDevice;
                        string strHolderInfo = string.Format(" UPDATE DEVICE INFORMATION\r\n Id: {0}\r\n Name: {1}\r\n MAC: {2}\r\n IP: {3}\r\n Subnet: {4}\r\n Gateway: {5}\r\n Host: {6}\r\n FAMode: {7}\r\n CtrlMode: {8}\r\n Description: {9}"
                            , deviceToUpdate.Id
                            , deviceToUpdate.Name
                            , deviceToUpdate.MAC
                            , deviceToUpdate.IP
                            , deviceToUpdate.Subnet
                            , deviceToUpdate.GateWay
                            , deviceToUpdate.HostIp
                            , deviceToUpdate.FAMode.IsUse == true ? "USED: " + deviceToUpdate.FAMode.FAHWNumber.ToString(): "NOT_USE: 0"
                            , deviceToUpdate.CtrMode
                            , deviceToUpdate.Description) ;

                        AddLog(new UserLog(strHolderInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_Device.UpdateDevice(deviceToUpdate);
                        string strReasultLog = "";
                        if (result.Result)
                        {
                            DTO_Device oldDevice = Repository.lstAllDevices.FirstOrDefault(h => h.Id == deviceToUpdate.Id);
                            if (oldDevice != null)
                            {
                                oldDevice = deviceToUpdate;
                            }
                            RefreshListView();
                            AddLog(new UserLog(" Update successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            strReasultLog = "Status: Error -> " + result.Detail;
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }
                    else
                    {
                        //Insert device
                        AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
                        DTO_Device addDevice = Repository.newDevice;
                        string strHolderInfo = string.Format(" ADD DEVICE \r\n Id: {0}\r\n Name: {1}\r\n MAC: {2}\r\n IP: {3}\r\n Subnet: {4}\r\n Gateway: {5}\r\n Host: {6}\r\n FAMode: {7}\r\n CtrlMode: {8}\r\n Description: {9}"
                            , addDevice.Id
                            , addDevice.Name
                            , addDevice.MAC
                            , addDevice.IP
                            , addDevice.Subnet
                            , addDevice.GateWay
                            , addDevice.HostIp
                            , addDevice.FAMode.IsUse == true ? "USED: " + addDevice.FAMode.FAHWNumber.ToString() : "NOT_USE: 0"
                            , addDevice.CtrMode
                            , addDevice.Description);

                        AddLog(new UserLog(strHolderInfo, EType.UserLogType.LOG_CONTENT));
                        SQLResult result = bus_Device.AddNewDevice(addDevice);
                        if (result.Result)
                        {
                            //Load socket for new deview
                            addDevice.Sockets = bUS_DeviceSocket.GetSocketByDevice(addDevice.Id);

                            Repository.lstAllDevices.Add(addDevice);
                            RefreshListView();
                            lbListItems.SelectedIndex = lbListItems.Items.Count - 1;
                            AddLog(new UserLog(" Insert successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        tbStatus.Text = result.Detail;
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }

                    break;
                case ViewMode.DOOR_VIEW:


                    break;
                case ViewMode.SCHEDULE_VIEW:

                    break;
                case ViewMode.RIGHT_VIEW:


                    break;
                case ViewMode.EVENT_VIEW:

                    break;
            }
        }

        private void RefreshListView()
        {
            this.listViewItems.Clear();

            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    BindCardToListItemView();
                    break;
                case ViewMode.HOLDER_VIEW:
                    BindHolderToListItemView();
                    break;
                case ViewMode.DEVICE_VIEW:
                    BindDeviceToListItemView();                    
                    break;
            }         
            lbListItems.ItemsSource = listViewItems;
        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            this.currentMode = ADD_MODE;

            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    CardDetailView uc = new CardDetailView(mode: currentMode);
                    uc.HorizontalAlignment = HorizontalAlignment.Stretch;
                    uc.VerticalAlignment = VerticalAlignment.Stretch;
                    pnlData.Children.Clear();

                    pnlData.Children.Add(uc);

                    break;
                case ViewMode.HOLDER_VIEW:
                    HolderDetailView ucHolder = new HolderDetailView(mode: currentMode);
                    ucHolder.HorizontalAlignment = HorizontalAlignment.Stretch;
                    ucHolder.VerticalAlignment = VerticalAlignment.Stretch;
                    pnlData.Children.Clear();

                    pnlData.Children.Add(ucHolder);
                    break;
                case ViewMode.DEVICE_VIEW:
                    DeviceDetailView ucDevice = new DeviceDetailView(mode: currentMode);
                    ucDevice.HorizontalAlignment = HorizontalAlignment.Stretch;
                    ucDevice.VerticalAlignment = VerticalAlignment.Stretch;
                    pnlData.Children.Clear();

                    pnlData.Children.Add(ucDevice);

                    break;
                case ViewMode.DOOR_VIEW:


                    break;
                case ViewMode.SCHEDULE_VIEW:

                    break;
                case ViewMode.RIGHT_VIEW:


                    break;
                case ViewMode.EVENT_VIEW:

                    break;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.currentMode == ADD_MODE)
            {
                //DO NOTHING
                return;
            }

            MessageBoxResult messResult = MessageBox.Show("Confirm to delete?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messResult == MessageBoxResult.No)
            {
                //DO NOTHING
                return;
            }

            AddLog(new UserLog(DateTime.Now.ToString(), EType.UserLogType.LOG_TIME));
            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    //Delete card
                    DTO_Card cardToDelete = Repository.selectedCard;
                    string strCardInfo = string.Format(" DELETE CARD \r\n Number: {0}\r\n Serial: {1}", cardToDelete.CardNumber, cardToDelete.CardSerial);
                    AddLog(new UserLog(strCardInfo, EType.UserLogType.LOG_CONTENT));
                    SQLResult result = bus_Card.DeleteCard(cardToDelete.CardSerial);
                    if (result.Result)
                    {
                        DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardSerial == cardToDelete.CardSerial);
                        Repository.lstAllCards.Remove(oldCard);

                        RefreshListView();
                        lbListItems.SelectedIndex = 0;
                        AddLog(new UserLog(" Delete successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                    }
                    else
                    {
                        AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                    }
                    tbStatus.Text = result.Detail;
                    AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));

                    break;
                case ViewMode.HOLDER_VIEW:
                    //Delete holder
                    DTO_CardHolder holderToDelete = Repository.selectedHolder;
                    string strHolderInfo = string.Format(" DELETE HOLDER \r\n Id: {0}\r\n Name: {1}", holderToDelete.Id, holderToDelete.Name);
                    AddLog(new UserLog(strHolderInfo, EType.UserLogType.LOG_CONTENT));
                    SQLResult delHolderResult = bus_CardHolder.DeleteCardHolder(holderToDelete.Id);
                    if (delHolderResult.Result)
                    {
                        //Update holder in card
                        foreach(DTO_Card card in Repository.lstAllCards)
                        {
                            if(card.Holder == holderToDelete.Id)
                            {
                                card.Type = EType.CardType.NOT_USE;
                                card.Holder = null;
                            }
                        }

                        //Remove holder in list holders
                        DTO_CardHolder oldHolder = Repository.lstAllCardHolders.FirstOrDefault(h => h.Id == holderToDelete.Id);
                        Repository.lstAllCardHolders.Remove(oldHolder);

                        RefreshListView();
                        lbListItems.SelectedIndex = 0;
                        AddLog(new UserLog(" Delete successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                    }
                    else
                    {
                        AddLog(new UserLog(" Error: " + delHolderResult.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                    }
                    tbStatus.Text = delHolderResult.Detail;
                    AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));

                    break;
                case ViewMode.DEVICE_VIEW:
                    //Delete device
                    DTO_Device deviceToDelete = Repository.selectedDevice;
                    string strDeviceInfo = string.Format(" DELETE DEVICE \r\n Id: {0}\r\n Name: {1}", deviceToDelete.Id, deviceToDelete.Name);
                    AddLog(new UserLog(strDeviceInfo, EType.UserLogType.LOG_CONTENT));
                    SQLResult delDeviceResult = bus_Device.DeleteDevice(deviceToDelete.Id);
                    if (delDeviceResult.Result)
                    {
                        //Remove device in list devices
                        DTO_Device oldDevice = Repository.lstAllDevices.FirstOrDefault(h => h.Id == deviceToDelete.Id);
                        Repository.lstAllDevices.Remove(oldDevice);

                        RefreshListView();
                        lbListItems.SelectedIndex = 0;
                        AddLog(new UserLog(" Delete successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                    }
                    else
                    {
                        AddLog(new UserLog(" Error: " + delDeviceResult.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                    }
                    tbStatus.Text = delDeviceResult.Detail;
                    AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));

                    break;
                case ViewMode.DOOR_VIEW:


                    break;
                case ViewMode.SCHEDULE_VIEW:

                    break;
                case ViewMode.RIGHT_VIEW:


                    break;
                case ViewMode.EVENT_VIEW:

                    break;
            }
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.L && Keyboard.Modifiers == ModifierKeys.Control)
            {
              
                if(showLogPanel)
                {
                    stackLog.Height = 0;
                    boderData.CornerRadius = new CornerRadius(0, 10, 10, 0);
                    showLogPanel = false;
                }
                else
                {
                    stackLog.Height = 250;
                    boderData.CornerRadius = new CornerRadius(0, 10, 0, 0);
                    showLogPanel = true;
                }

                
            }
        }

        private void hideLog_Click(object sender, RoutedEventArgs e)
        {
            stackLog.Height = 0;
            boderData.CornerRadius = new CornerRadius(0, 10, 10, 0);

            showLogPanel = false;
        }

        private void btClearLog_Click(object sender, RoutedEventArgs e)
        {
            tbLog.Document.Blocks.Clear();
        }

        private void btExportLog_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, StringFromRichTextBox(tbLog));

            tbStatus.Text = "Success export log file: " + saveFileDialog.FileName;
        }

        public string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
    }

    public enum ViewMode
    {
        NULL = 0,
        CARD_VIEW = 1,
        HOLDER_VIEW = 2,
        DEVICE_VIEW = 3,
        DOOR_VIEW = 4,
        SCHEDULE_VIEW = 5,
        RIGHT_VIEW = 6,
        EVENT_VIEW = 7
    }
}
