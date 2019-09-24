using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using static Android.Provider.MediaStore;
using Com.Baidu.Android.Pushservice;

namespace BaiduPushDemo.Droid
{
    [Activity(Label = "BaiduPushDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            //BaiduPushReceiver.InitializeBaiduPushManager(this);
            InitBuilder(this);
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void InitBuilder(Context context)
        {
            /**
       * 以下通知栏设置2选1。使用默认通知时，无需添加以下设置代码。
       */

            // 1.默认通知
            // 若您的应用需要适配Android O（8.x）系统，且将目标版本targetSdkVersion设置为26及以上时：
            // SDK提供设置Android O（8.x）新特性---通知渠道的设置接口。
            // 若不额外设置，SDK将使用渠道名默认值"Push"；您也可以仿照以下3行代码自定义channelId/channelName。
            // 注：非targetSdkVersion 26的应用无需以下调用且不会生效
            BasicPushNotificationBuilder bBuilder = new BasicPushNotificationBuilder();
            //bBuilder.SetChannelId("testDefaultChannelId");
            //bBuilder.SetChannelName("testDefaultChannelName");
            PushManager.SetDefaultNotificationBuilder(this, bBuilder); //使自定义channel生效
            
            // 2.自定义通知
            // 设置自定义的通知样式，具体API介绍见用户手册
            // 请在通知推送界面中，高级设置->通知栏样式->自定义样式，选中并且填写值：1，
            // 与下方代码中 PushManager.setNotificationBuilder(this, 1, cBuilder)中的第二个参数对应


            CustomPushNotificationBuilder cBuilder = new CustomPushNotificationBuilder(
                  Resource.Layout.NotificationCustomBuilder,
                  Resource.Id.notification_icon,
                  Resource.Id.notification_title,
                  Resource.Id.notification_text);

            cBuilder.SetNotificationFlags((int)NotificationFlags.AutoCancel);
            cBuilder.SetNotificationDefaults((int)NotificationDefaults.Sound);
            cBuilder.SetStatusbarIcon(context.ApplicationInfo.Icon);
            cBuilder.SetLayoutDrawable(Resource.Mipmap.icon_round);
            cBuilder.SetNotificationSound(Android.Net.Uri.WithAppendedPath(
                    Audio.Media.InternalContentUri, "6").ToString());
            // 若您的应用需要适配Android O（8.x）系统，且将目标版本targetSdkVersion设置为26及以上时：
            // 可自定义channelId/channelName, 若不设置则使用默认值"Push"；
            // 注：非targetSdkVersion 26的应用无需以下2行调用且不会生效
            //cBuilder.SetChannelId("testId");
            //cBuilder.SetChannelName("testName");
            // 推送高级设置，通知栏样式设置为下面的ID，ID应与server下发字段notification_builder_id值保持一致
            PushManager.SetNotificationBuilder(context, 1, cBuilder);
            
        }
    }
}