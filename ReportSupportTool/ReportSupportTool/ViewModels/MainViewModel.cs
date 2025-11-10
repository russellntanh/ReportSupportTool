using LiveCharts;
using LiveCharts.Wpf;
using ReportSupportTool.Models;
using ReportSupportTool.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ReportSupportTool.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Service to fetch CSV data
        CsvDataService csvDataService = new CsvDataService();

        public event PropertyChangedEventHandler? PropertyChanged;

        // Places to hold data
        public List<IssueModel> RawCsvData { get; set; }
        public List<MemberStatusModel> MemberStatuses { get; set; }
        public ObservableCollection<IssueModel> IssuesCollection { get; set; } = new();
        public ObservableCollection<MemberStatusModel> MemberStatusCollection { get; set; } = new();

        // Load command
        public ICommand LoadDataCommand { get; }

        public MainViewModel()
        {
            LoadDataCommand = new Commands.RelayCommand(ExecuteLoadData, CanExecuteLoadData);
        }

        private bool CanExecuteLoadData(object parameter)
        {
            return true; // Luôn cho phép tải dữ liệu
        }

        private void ExecuteLoadData(object parameter)
        {
            RawCsvData = csvDataService.GetRawCsvData();
            MemberStatuses = csvDataService.GetMemberStatusData(RawCsvData);

            foreach (var issue in RawCsvData)
            {
                IssuesCollection.Add(issue);
            }

            foreach (var member in MemberStatuses)
            {
                MemberStatusCollection.Add(member);
            }
            DrawByLiveChart();
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SeriesCollection SeriesCollection { get; set; }

        // 2. Thuộc tính để chứa Nhãn của Trục X (Quý)
        public string[] Labels { get; set; }

        // 3. Thuộc tính cho Trình định dạng Nhãn Trục Y (Hiển thị đơn vị)
        public Func<double, string> YFormatter { get; set; }

        private void DrawByLiveChart()
        {
            /// ---1.Dữ liệu Biểu đồ(Series)-- -
            SeriesCollection = new SeriesCollection
            {
                // BarSeries là biểu đồ thanh
                new ColumnSeries
                {
                    Title = "Doanh số 2025",
                    // Values là một ChartValues<T> chứa các giá trị
                    Values = new ChartValues<double> { 50, 100, 200, 150 }
                }
            };

            // --- 2. Nhãn Trục X (Labels) ---
            Labels = new[] { "Quý 1", "Quý 2", "Quý 3", "Quý 4" };

            // --- 3. Định dạng Trục Y (Formatter) ---
            // Định dạng giá trị Y thành "X Triệu"
            // Ví dụ: 50 -> "50 Triệu"
            YFormatter = value => value.ToString("N0") + " Triệu";
        }
    }
}
