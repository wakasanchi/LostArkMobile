using LostArkMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LostArkMobile.ViewModels
{
    public class EventSettingViewModel : ViewModelBase
    {
        public enum EventType : int
        {
            HotTime = 0,    //ホットタイムイベント
            ChaosGate,      //カオスゲート
            Tournament,     //証明の闘技会
            FieldBoss,      //フィールドボス
            TravelMerchant, //旅商人
            Voyage,         //航海
            Island,         //島
            Sylmael,        //シルマエル
            AdventureIsland,//冒険島
        }

        private DateTime NowDate;
        private List<Event> Events;
        private Dictionary<string, ImageSource> DicImageSource;

        private ObservableCollection<Event> _itemList = new ObservableCollection<Event>();
        public ObservableCollection<Event> ItemList
        {
            get { return _itemList; }
            set
            {
                SetProperty(ref _itemList, value);
            }
        }

        private bool _isRefresing = false;
        public bool IsRefresing
        {
            get { return _isRefresing; }
            set
            {
                SetProperty(ref _isRefresing, value);
            }
        }

        public DelegateCommand RefreshCommand { get; set; }

        public EventSettingViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "イベント設定";
            DicImageSource = new Dictionary<string, ImageSource>();
            RefreshCommand = new DelegateCommand(() => CreateEvents());

            CreateEvents();
        }

        private void CreateEvents()
        {
            IsRefresing = true;

            Task.Run(() =>
            {
                NowDate = DateTime.Now;
                Events = new List<Event>();

                foreach (EventType value in Enum.GetValues(typeof(EventType)))
                {
                    CreateEvent(value);
                }

                SetString();
                Events = Events.OrderBy(e => e.Time).ToList();
                ItemList = new ObservableCollection<Event>(Events);

                IsRefresing = false;
            });
        }

        private void CreateEvent(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.HotTime:
                    CreateEventHotTime();
                    break;
                case EventType.ChaosGate:
                    CreateEventChaosGate();
                    break;
                case EventType.Tournament:
                    break;
                case EventType.FieldBoss:
                    CreateEventChaosFieldBoss();
                    break;
                case EventType.TravelMerchant:
                    break;
                case EventType.Voyage:
                    CreateEventVoyage();
                    break;
                case EventType.Island:
                    CreateEventIsland();
                    break;
                case EventType.Sylmael:
                    break;
                case EventType.AdventureIsland:
                    CreateEventAdventureIsland();
                    break;
                default:
                    break;
            }
        }

        private void CreateEventHotTime()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から1週間）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(7);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [250]証明の闘技会：競争戦
            //毎週土曜日の14時と20時
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 14, 0, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeHotTime.png", "[250]証明の闘技会：競争戦");
            #endregion

            #region [250]証明の闘技会：殲滅戦
            //毎週土曜日の14時と20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 14, 0, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeHotTime.png", "[250]証明の闘技会：殲滅戦");
            #endregion

            #region [250]証明の闘技会：大将戦
            //毎週土曜20時と日曜14時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 14, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeHotTime.png", "[250]証明の闘技会：大将戦");
            #endregion

            #region [250]証明の闘技会：乱闘戦
            //毎週土曜14時と日曜20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 14, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeHotTime.png", "[250]証明の闘技会：乱闘戦");
            #endregion
        }

        private void CreateEventChaosGate()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から1週間）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(7);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [302]揺らめく狂気軍団
            //毎週火曜日の20時
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Tuesday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeChaosGate.png", "[302]揺らめく狂気軍団");
            #endregion

            #region [302]揺らめく疫病軍団
            //毎週金曜日の20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Friday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeChaosGate.png", "[302]揺らめく疫病軍団");
            #endregion

            #region [302]揺らめく暗黒軍団
            //毎週土曜日の22時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 22, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeChaosGate.png", "[302]揺らめく暗黒軍団");
            #endregion

            #region [302]揺らめく夢幻軍団
            //毎週日曜日の22時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Sunday) continue;
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 22, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeChaosGate.png", "[302]揺らめく夢幻軍団");
            #endregion
        }

        private void CreateEventChaosFieldBoss()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から1週間）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(7);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [310]シグナトゥス
            //毎週木曜日の20時と金曜日の2時
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 2, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[310]シグナトゥス");
            #endregion

            #region [340]タルシラ
            //毎週土曜日の20時と日曜日の20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[340]タルシラ");
            #endregion

            #region [355]エラスモ
            //毎日の6時,14時,20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 6, 0, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 14, 0, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[355]エラスモ");
            #endregion

            #region [370]ソル＝グランデ
            //毎週土曜日の20時と日曜日の20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[370]ソル＝グランデ");
            #endregion

            #region [385]混沌の麒麟
            //毎週木曜日の20時と金曜日の2時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 2, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[385]混沌の麒麟");
            #endregion

            #region [415]プロキシマ
            //毎週金曜日の2時と土曜日の20時
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 2, 0, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 0, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeFieldBoss.png", "[415]プロキシマ");
            #endregion
        }

        private void CreateEventVoyage()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から1週間）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(7);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [302]航海協同：アルデタイン
            //毎週
            //月曜日の19:30
            //火曜日の23:30
            //水曜日の21:30
            //木曜日の19:30
            //金曜日の23:30
            //土曜日の21:30
            //日曜日の19:30
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeVoyage.png", "[302]航海協同：アルデタイン");
            #endregion

            #region [302]航海協同：ベルン
            //毎週
            //月曜日の23:30
            //火曜日の21:30
            //水曜日の19:30
            //木曜日の23:30
            //金曜日の21:30
            //土曜日の19:30
            //日曜日の21:30
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeVoyage.png", "[302]航海協同：ベルン");
            #endregion

            #region [302]航海協同：アニツ
            //毎週
            //月曜日の21:30
            //火曜日の19:30
            //水曜日の23:30
            //木曜日の21:30
            //金曜日の19:30
            //土曜日の23:30
            //日曜日の23:30
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 30, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeVoyage.png", "[302]航海協同：アニツ");
            #endregion

            #region [302]調和の門
            //毎週
            //月曜日の18:00 22:00
            //水曜日の18:00 22:00
            //土曜日の18:00 23:00
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 18, 00, 0));
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 22, 00, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 18, 00, 0));
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 22, 00, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 18, 00, 0));
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 00, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeVoyage.png", "[302]調和の門");
            #endregion
        }

        private void CreateEventIsland()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から2日後）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(2);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [250]眠る歌の島
            //毎日の0:20,3:20,6:20,9:20,12:20,15:20,18:20,21:20
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 0, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 3, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 6, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 9, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 12, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 15, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 18, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 21, 20, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_nemureru.png", "[250]眠る歌の島");
            #endregion

            #region [250]ドゥキー島
            //毎日の0:50,4:50,8:50,12:50,16:50,20:50
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 0, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 4, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 8, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 12, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 16, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_dokey.png", "[250]ドゥキー島");
            #endregion

            #region [250]新月の島
            //毎日の3:00,7:00,11:00,15:00,19:00,23:00
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 3, 00, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 7, 00, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 11, 00, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 15, 00, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 00, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 23, 00, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_singetu.png", "[250]新月の島");
            #endregion

            #region [250]邪欲の島
            //毎日の1:20,7:20,13:20,19:20
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 1, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 7, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 20, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 20, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_jayoku.png", "[250]邪欲の島");
            #endregion

            #region [280]アラケル
            //毎日の1:50,7:50,13:50,19:50
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 1, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 7, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 50, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_arakeru.png", "[250]アラケル");
            #endregion

            #region [300]スピーダ島
            //毎日の1:30,7:30,13:30,19:30,22:30
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 1, 30, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 7, 30, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 30, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 19, 30, 0));
                eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 22, 30, 0));
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeIsland_speeda.png", "[250]スピーダ島");
            #endregion
        }

        private void CreateEventAdventureIsland()
        {
            //基準日
            var kijunDate = NowDate.AddDays(-1);

            //終了日（現時点から1週間）
            var endDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day).AddDays(7);

            //日付リスト
            var dateList = new List<DateTime>();
            GetDateList(ref dateList, kijunDate, endDate);

            //イベント格納
            var eventDates = new List<DateTime>();

            #region [250]ポラール島 x
            ////毎週
            ////月曜日の20:50
            ////火曜日の20:50
            ////木曜日の20:50
            ////土曜日の13:50
            ////日曜日の13:50
            //foreach (var date in dateList)
            //{
            //    if (date.DayOfWeek == DayOfWeek.Monday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Tuesday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Thursday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
            //    }
            //}
            //SetEvents(eventDates, "LostArkMobile.Resources.EventTypeAdventureIsland.png", "[250]ポラール島");
            #endregion

            #region [320]メーデイア
            //毎週
            //月曜日の20:50
            //水曜日の20:50
            //金曜日の20:50
            //土曜日の20:50
            //日曜日の13:50
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeAdventureIsland.png", "[320]メーデイア");
            #endregion

            #region [325]フォルペ
            //毎週
            //火曜日の20:50
            //木曜日の20:50
            //土曜日の13:50
            //土曜日の20:50
            //日曜日の20:50
            eventDates = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek == DayOfWeek.Tuesday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
                }
            }
            SetEvents(eventDates, "LostArkMobile.Resources.EventTypeAdventureIsland.png", "[325]フォルペ");
            #endregion

            #region [430]死の峡谷 x
            ////毎週
            ////火曜日の20:50
            ////木曜日の20:50
            ////土曜日の13:50
            ////土曜日の20:50
            ////日曜日の20:50
            //eventDates = new List<DateTime>();
            //foreach (var date in dateList)
            //{
            //    if (date.DayOfWeek == DayOfWeek.Tuesday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Thursday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 13, 50, 0));
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //    else if (date.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        eventDates.Add(new DateTime(date.Year, date.Month, date.Day, 20, 50, 0));
            //    }
            //}
            //SetEvents(eventDates, "LostArkMobile.Resources.EventTypeAdventureIsland.png", "[430]死の峡谷");
            #endregion
        }

        private void SetEvents(List<DateTime> eventDates, string imageSource, string title)
        {
            eventDates = eventDates.Where(d => d >= NowDate.AddMinutes(-10)).Take(5).ToList();
            foreach (var date in eventDates)
            {
                Events.Add(new Event()
                {
                    ImageSource = GetImageSource(imageSource),
                    Title = title,
                    Time = date,
                });
            }
        }

        private ImageSource GetImageSource(string keyName)
        {
            if (DicImageSource.ContainsKey(keyName))
            {
                return DicImageSource[keyName];
            }
            else
            {
                var img = ImageSource.FromResource(keyName);
                DicImageSource.Add(keyName, img);
                return img;
            }
        }

        private void GetDateList(ref List<DateTime> DateList, DateTime startDate, DateTime endDate)
        {
            DateList.Clear();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                DateList.Add(date);
            }
        }

        private void SetString()
        {
            foreach (var item in Events)
            {
                item.SubTitle = $"{item.Time.ToString("yyyy/MM/dd(ddd)")} {item.Time.Hour.ToString().PadLeft(2,'0')}:{item.Time.Minute.ToString().PadLeft(2, '0')}開始";
                var timespan = item.Time - NowDate;
                item.TimeString = item.Time > NowDate ? $"-{((timespan.Days * 24) + timespan.Hours).ToString().PadLeft(2, '0')}:{timespan.Minutes.ToString().PadLeft(2, '0')}" : "";
            }
        }
    }
}
