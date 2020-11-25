using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using ScorpiconAccess.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            PrepareData();
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

                    listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
                    foreach (DTO_CardHolder holder in Repository.lstAllCardHolders)
                    {
                        DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                        listViewItem.ImageSource = "/Icon/person_gray_30px.png";
                        listViewItem.TextBinding = holder.Name;
                        listViewItem.Key = holder.Id;

                        listViewItems.Add(listViewItem);
                    }

                    lbListItems.ItemsSource = listViewItems;
                    break;
                case ViewMode.DEVICE_VIEW:
                    tbViewName.Text = "Device";

                    listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
                    foreach (DTO_Device device in Repository.lstAllDevices)
                    {
                        DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                        listViewItem.ImageSource = "/Icon/device_gray_50px.png";
                        listViewItem.TextBinding = device.Name;
                        listViewItem.Key = device.Id;

                        listViewItems.Add(listViewItem);
                    }

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
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrepareData()
        {
            Repository.lstAllCards = bus_Card.GetAllCard();
            Repository.lstAllCardHolders = bus_CardHolder.GetAllCardHolder();
            Repository.lstAllDevices = bus_Device.GetAllDevice();
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
                        DTO_Card selectedCard = Repository.lstAllCards.FirstOrDefault(card => card.CardSerial == selectedItem.Key);
                        Repository.selectedCard = selectedCard;

                        //Show card detail info
                        CardDetailView uc = new CardDetailView(mode: currentMode);
                        uc.HorizontalAlignment = HorizontalAlignment.Stretch;
                        uc.VerticalAlignment = VerticalAlignment.Stretch;
                        pnlData.Children.Clear();

                        pnlData.Children.Add(uc);
                        

                        break;
                    case ViewMode.HOLDER_VIEW:
                        DTO_CardHolder selectedHolder = Repository.lstAllCardHolders.FirstOrDefault(holder => holder.Id == selectedItem.Key);

                        //Show holder detail info

                        break;
                    case ViewMode.DEVICE_VIEW:
                        DTO_Device selectedDevice = Repository.lstAllDevices.FirstOrDefault(deice => deice.Id == selectedItem.Key);
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
                    

                    //Show holder detail info

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

        private void AddLog(UserLog log)
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
                            MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Information);

                            DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardSerial == cardToUpdate.CardSerial);
                            if (oldCard != null)
                            {
                                oldCard = cardToUpdate;
                            }

                            //Refresh list card view
                            this.listViewItems.Clear();
                            BindCardToListItemView();
                            lbListItems.ItemsSource = listViewItems;

                            AddLog(new UserLog(" Update successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));

                        }
                        else
                        {
                            MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
                            strReasultLog = "Status: Error -> " + result.Detail;
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
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
                            MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Information);

                            Repository.lstAllCards.Add(addCard);

                            //Refresh list card view
                            this.listViewItems.Clear();
                            BindCardToListItemView();
                            lbListItems.ItemsSource = listViewItems;

                            AddLog(new UserLog(" Insert successfull!", EType.UserLogType.LOG_STATUS_SUCCESS));
                        }
                        else
                        {
                            MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
                            AddLog(new UserLog(" Error: " + result.Detail, EType.UserLogType.LOG_STATUS_ERROR));
                        }
                        AddLog(new UserLog("--------------------", EType.UserLogType.LOG_CONTENT));
                    }
                    break;
                case ViewMode.HOLDER_VIEW:


                    break;
                case ViewMode.DEVICE_VIEW:


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


                    break;
                case ViewMode.DEVICE_VIEW:


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

            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    //Delete card
                    DTO_Card cardToDelete = Repository.selectedCard;
                    SQLResult result = bus_Card.DeleteCard(cardToDelete.CardSerial);
                    if (result.Result)
                    {
                        MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Information);

                        DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardSerial == cardToDelete.CardSerial);
                        Repository.lstAllCards.Remove(oldCard);

                        //Refresh list card view
                        this.listViewItems.Clear();
                        BindCardToListItemView();
                        lbListItems.ItemsSource = listViewItems;
                        lbListItems.SelectedIndex = 0;

                    }
                    else
                    {
                        MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;
                case ViewMode.HOLDER_VIEW:


                    break;
                case ViewMode.DEVICE_VIEW:


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
                }
                else
                {
                    stackLog.Height = 250;
                    boderData.CornerRadius = new CornerRadius(0, 10, 0, 0);
                }

                showLogPanel = !showLogPanel;
            }
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
