﻿

#pragma checksum "C:\Users\m0rg0_000\Documents\GitHub\ARDroneScouting\ARDrone2.Sample.W8\FlyPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6DDE17F7AA24B7C7624DAAA9E3B11B68"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARDrone2.Sample
{
    partial class FlyPage : global::ARDrone2.Sample.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 17 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.Page_OnLoaded;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 127 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.TakeOffLandButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 64 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PlayAnimationButton_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 65 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PlayLedAnimationButton_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 66 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StartVideoRecordingButton_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 67 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.StopVideoRecordingButton_Click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 57 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.TakePicture_Click;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 58 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ResetEmergency_Click;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 59 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Configuration_Click;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 60 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SwitchIndoorOutdoor_Click;
                 #line default
                 #line hidden
                break;
            case 11:
                #line 61 "..\..\..\FlyPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SwitchVideoChannel_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


