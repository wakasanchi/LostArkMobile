using LostArkMobile.Models;
using Prism.Commands;
using Prism.Services.Dialogs.Xaml;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace LostArkMobile.Views
{
    public partial class EventSetting
    {

        public EventSetting()
        {
            InitializeComponent();

            Show();
        }

        private void Show()
        {
            HotTime1.On = GetSwitchCellOn("[250]証明の闘技会：競争戦");
            HotTime2.On = GetSwitchCellOn("[250]証明の闘技会：殲滅戦");
            HotTime3.On = GetSwitchCellOn("[250]証明の闘技会：大将戦");
            HotTime4.On = GetSwitchCellOn("[250]証明の闘技会：乱闘戦");
            ChaosGate1.On = GetSwitchCellOn("[302]揺らめく狂気軍団");
            ChaosGate2.On = GetSwitchCellOn("[302]揺らめく疫病軍団");
            ChaosGate3.On = GetSwitchCellOn("[302]揺らめく暗黒軍団");
            ChaosGate4.On = GetSwitchCellOn("[302]揺らめく夢幻軍団");
            FieldBoss1.On = GetSwitchCellOn("[310]シグナトゥス");
            FieldBoss2.On = GetSwitchCellOn("[340]タルシラ");
            FieldBoss3.On = GetSwitchCellOn("[355]エラスモ");
            FieldBoss4.On = GetSwitchCellOn("[370]ソル＝グランデ");
            FieldBoss5.On = GetSwitchCellOn("[385]混沌の麒麟");
            FieldBoss6.On = GetSwitchCellOn("[415]プロキシマ");
            Voyage1.On = GetSwitchCellOn("[302]航海協同：アルデタイン");
            Voyage2.On = GetSwitchCellOn("[302]航海協同：ベルン");
            Voyage3.On = GetSwitchCellOn("[302]航海協同：アニツ");
            Voyage4.On = GetSwitchCellOn("[302]調和の門");
            Island1.On = GetSwitchCellOn("[250]眠る歌の島");
            Island2.On = GetSwitchCellOn("[250]ドゥキー島");
            Island3.On = GetSwitchCellOn("[250]新月の島");
            Island4.On = GetSwitchCellOn("[250]邪欲の島");
            Island5.On = GetSwitchCellOn("[280]アラケル");
            Island6.On = GetSwitchCellOn("[300]スピーダ島");
            AdventureIsland1.On = GetSwitchCellOn("[250]ポラール島");
            AdventureIsland2.On = GetSwitchCellOn("[320]メーデイア");
            AdventureIsland3.On = GetSwitchCellOn("[325]フォルペ");
            AdventureIsland4.On = GetSwitchCellOn("[430]死の峡谷");
        }

        private bool GetSwitchCellOn(string key)
        {
            if (!Application.Current.Properties.ContainsKey(key)) return true;
            return Application.Current.Properties[key] as bool? ?? false;
        }

        private void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
        {
            Application.Current.Properties[((SwitchCell)sender).Text] = e.Value;

            Show();
        }
    }
}
