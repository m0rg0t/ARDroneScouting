﻿

#pragma checksum "C:\Users\aldanvy\Documents\Git\ARDroneSDK\ARDrone2.Sample.W8\ConfigurationPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "47403F1BDA1A7CE116EF877B04DF41DC"
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
    partial class ConfigurationPage : global::ARDrone2.Sample.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 44 "..\..\..\ConfigurationPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerReleased += this.AltitudeMax_PointerReleased;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 30 "..\..\..\ConfigurationPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

