using System;
using System.Collections.Generic;
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

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        #region Public Properties
        public List<string> MonthName { get; set; }
        public List<int> YearList { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //public object YearButtonObject { get; set; }
        //public object MonthButtonObject { get; set; }
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            FillMonthName();
            FillYear();
        }
        #endregion

        #region Events
        private void btnMonth_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //if(MonthButtonObject != null)
                //(MonthButtonObject as Button).FontWeight = FontWeights.Normal;

                string strMonth = Convert.ToString(((ListBoxItem)lstMonth.ContainerFromElement((Button)sender)).Content);
                int intMonth = (int)((EnumMonth)Enum.Parse(typeof(EnumMonth), strMonth));

                Month = intMonth;

                SetCalendar();

                tbMonth.Text = strMonth;

                //((ListBoxItem)lstMonth.ContainerFromElement((Button)sender)).FontWeight = FontWeights.Bold;
                //MonthButtonObject = (ListBoxItem)lstMonth.ContainerFromElement((Button)sender); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tbYear_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (YearButtonObject != null)
                //    (YearButtonObject as Button).FontWeight = FontWeights.Normal;

                var strYear = ((ListBoxItem)lstYear.ContainerFromElement((Button)sender)).Content;
                int intYear = (int)strYear;

                Year = intYear;

                SetCalendar();

                tbYear.Text = Year.ToString();

                //((ListBoxItem)lstYear.ContainerFromElement((Button)sender)).FontWeight = FontWeights.Bold;
                //YearButtonObject = (ListBoxItem)lstYear.ContainerFromElement((Button)sender);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnToday_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Year = DateTime.Now.Year;
                tbYear.Text = Year.ToString();

                Month = DateTime.Now.Month;
                tbMonth.Text = ((EnumMonth)Month).ToString();

                SetCalendar();

                lstYear.ScrollIntoView(((List<int>)(lstYear.ItemsSource)).Where(x => x.Equals(Year)).First());
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Functions
        private int GetGridColumnNoFromDayOfWeek(EnumWeekDays enumWeekDay)
        {
            int intWeekDayNo = -1;
            try
            {
                switch (enumWeekDay)
                {
                    case EnumWeekDays.Sunday:
                        intWeekDayNo = 0;
                        break;
                    case EnumWeekDays.Monday:
                        intWeekDayNo = 1;
                        break;
                    case EnumWeekDays.Tuesday:
                        intWeekDayNo = 2;
                        break;
                    case EnumWeekDays.Wednesday:
                        intWeekDayNo = 3;
                        break;
                    case EnumWeekDays.Thursday:
                        intWeekDayNo = 4;
                        break;
                    case EnumWeekDays.Friday:
                        intWeekDayNo = 5;
                        break;
                    case EnumWeekDays.Saturday:
                        intWeekDayNo = 6;
                        break;
                }

                return intWeekDayNo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int GetMonthNoFromMonthName(EnumMonth enumMonth)
        {
            int intMonthNo = -1;
            try
            {
                switch (enumMonth)
                {
                    case EnumMonth.Jan:
                        intMonthNo = 1;
                        break;
                    case EnumMonth.Feb:
                        intMonthNo = 2;
                        break;
                    case EnumMonth.Mar:
                        intMonthNo = 3;
                        break;
                    case EnumMonth.Apr:
                        intMonthNo = 4;
                        break;
                    case EnumMonth.May:
                        intMonthNo = 5;
                        break;
                    case EnumMonth.Jun:
                        intMonthNo = 6;
                        break;
                    case EnumMonth.Jul:
                        intMonthNo = 7;
                        break;
                    case EnumMonth.Aug:
                        intMonthNo = 8;
                        break;
                    case EnumMonth.Sep:
                        intMonthNo = 9;
                        break;
                    case EnumMonth.Oct:
                        intMonthNo = 10;
                        break;
                    case EnumMonth.Nov:
                        intMonthNo = 11;
                        break;
                    case EnumMonth.Dec:
                        intMonthNo = 12;
                        break;
                }

                return intMonthNo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FillYear()
        {
            YearList = new List<int>();
            int intCurrentYear = DateTime.Now.Year; //Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            try
            {
                for (int i = 1751; i < 2050; i++)
                {
                    YearList.Add(i);
                }

                lstYear.ItemsSource = YearList;
                lstYear.ScrollIntoView(((List<int>)(lstYear.ItemsSource)).Where(x => x.Equals(intCurrentYear)).First());

                SetCalendar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FillMonthName()
        {
            MonthName = new List<string>();
            try
            {
                var objCultureInfo = new System.Globalization.CultureInfo("en-US");
                string[] strMonthNameArray = objCultureInfo.DateTimeFormat.AbbreviatedMonthNames;

                for (int i = 0; i < strMonthNameArray.Length - 1; i++)
                {
                    MonthName.Add(strMonthNameArray[i]);
                }

                lstMonth.ItemsSource = MonthName;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetCalendar()
        {
            try
            {
                tbMessage.Text = string.Empty;

                int intFirstDayOfWeekColumnNo = GetGridColumnNoFromDayOfWeek(EnumWeekDays.Sunday);
                int intTotalCount = 1;
                //int intCurrentMonthLastRowNo = 1;
                int intCurrentMonthDaysRowNo = 1;

                if (Year <= 0)
                {
                    Year = DateTime.Now.Year;

                    tbYear.Text = Year.ToString();
                }

                if (Month <= 0)
                {
                    Month = DateTime.Now.Month;

                    tbMonth.Text = ((EnumMonth)Month).ToString();
                }

                

                #region Set Previous Month Days in Current Month
                var previousMonthLastDay = new DateTime(Year, Month, 1).AddDays(-1);
                var previousMonthLastDayOfWeek = (EnumWeekDays)previousMonthLastDay.DayOfWeek;
                int intPreviousMonthLastDate = previousMonthLastDay.Day;

                //intCurrentMonthLastRowNo = intCurrentMonthDaysRowNo;
                //intCurrentMonthDaysRowNo = 1;
                int intPreviousMonthLastDayColumnNo = GetGridColumnNoFromDayOfWeek(previousMonthLastDayOfWeek);

                int intPreviousMonthTotalDaysShowOnCurrentMonth = intPreviousMonthLastDate - intPreviousMonthLastDayColumnNo;

                for (int i = 0; i <= intPreviousMonthLastDayColumnNo; i++)
                {
                    Button btn = new Button();
                    btn.Width = 25;
                    btn.Height = 25;
                    btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    btn.IsEnabled = false;
                    btn.Content = intPreviousMonthTotalDaysShowOnCurrentMonth;

                    intPreviousMonthTotalDaysShowOnCurrentMonth++;

                    btn.SetValue(Grid.RowProperty, intCurrentMonthDaysRowNo);
                    btn.SetValue(Grid.ColumnProperty, intFirstDayOfWeekColumnNo);

                    intFirstDayOfWeekColumnNo++;

                    clndr.Children.Add(btn);

                    intTotalCount++;
                }
                #endregion

                #region Set CurrentMonth Days
                //Get total no of days of select year and month
                int intCurrentMonthNoOfDays = DateTime.DaysInMonth(Year, Month);

                var currentMonthFirstDay = new DateTime(Year, Month, 1);
                var currentMonthFirstDayOfWeek = (EnumWeekDays)currentMonthFirstDay.DayOfWeek;

                //var lastday = firstDay.AddMonths(1).AddDays(-1);
                //var lastDayOfWeek = lastday.DayOfWeek;

                int intCurrentMonthFirstDayColumnNo = GetGridColumnNoFromDayOfWeek(currentMonthFirstDayOfWeek);

                if (intFirstDayOfWeekColumnNo > 6)
                {
                    intCurrentMonthDaysRowNo++;
                }
                else 
                {
                    intCurrentMonthDaysRowNo = 1;
                }

                for (int i = 1; i <= intCurrentMonthNoOfDays; i++)
                {
                    Button btn = new Button();
                    btn.Width = 25;
                    btn.Height = 25;
                    btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    btn.Content = i;
                    btn.Click += btn_Click;

                    if (Month.Equals(DateTime.Now.Month) && Year.Equals(DateTime.Now.Year))
                    {
                        if (btn.Content.ToString().Equals(DateTime.Now.Day.ToString()))
                        {
                            btn.FontWeight = FontWeights.Bold;
                        }
                    }

                    if (intCurrentMonthFirstDayColumnNo <= 6)
                    {
                        btn.SetValue(Grid.RowProperty, intCurrentMonthDaysRowNo);
                        btn.SetValue(Grid.ColumnProperty, intCurrentMonthFirstDayColumnNo);

                        intCurrentMonthFirstDayColumnNo++;
                    }
                    else
                    {
                        intCurrentMonthDaysRowNo++;
                        intCurrentMonthFirstDayColumnNo = 0;

                        btn.SetValue(Grid.RowProperty, intCurrentMonthDaysRowNo);
                        btn.SetValue(Grid.ColumnProperty, intCurrentMonthFirstDayColumnNo);

                        intCurrentMonthFirstDayColumnNo++;
                    }

                    intTotalCount++;
                    clndr.Children.Add(btn);
                }
                #endregion

                #region Set Next Month Days in Current Month
                //intCurrentMonthLastRowNo = intCurrentMonthDaysRowNo;

                int intNextMonthTotalDays = 42 - intTotalCount;

                for (int i = 1; i <= intNextMonthTotalDays + 1; i++)
                {
                    Button btn = new Button();
                    btn.Width = 25;
                    btn.Height = 25;
                    btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    btn.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    btn.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    btn.Content = i;
                    btn.IsEnabled = false;

                    if (intCurrentMonthFirstDayColumnNo <= 6)
                    {
                        btn.SetValue(Grid.RowProperty, intCurrentMonthDaysRowNo); //btn.SetValue(Grid.RowProperty, intCurrentMonthLastRowNo);
                        btn.SetValue(Grid.ColumnProperty, intCurrentMonthFirstDayColumnNo);

                        intCurrentMonthFirstDayColumnNo++;
                    }
                    else
                    {
                        intCurrentMonthDaysRowNo++; //intCurrentMonthLastRowNo++;
                        intCurrentMonthFirstDayColumnNo = 0;

                        btn.SetValue(Grid.RowProperty, intCurrentMonthDaysRowNo); //btn.SetValue(Grid.RowProperty, intCurrentMonthLastRowNo);
                        btn.SetValue(Grid.ColumnProperty, intCurrentMonthFirstDayColumnNo);

                        intCurrentMonthFirstDayColumnNo++;
                    }

                    //intTotalCount++;
                    clndr.Children.Add(btn);
                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                if (Month.Equals(DateTime.Now.Month) && Year.Equals(DateTime.Now.Year))
                {
                    if (btn != null)
                    {
                        if (btn.Content.ToString().Equals(DateTime.Now.Day.ToString()))
                        {
                            tbMessage.Text = "Today date is " + DateTime.Now.Date + ".";
                        }
                        else
                        {
                            tbMessage.Text = "This is not a today date.";
                        }
                    }
                }
                else
                {
                    tbMessage.Text = "This is not a today date.";
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

    }
}
