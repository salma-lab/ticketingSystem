﻿#pragma checksum "..\..\..\UserDashboard.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0E38322E0DE15069C33D2F5D63259EC13A1AA8CD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace WpfAuthenticationApp {
    
    
    /// <summary>
    /// UserDashboard
    /// </summary>
    public partial class UserDashboard : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTicketDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox NewTicketOralementCheckBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTicketAppareilNomTextBox;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTicketEtageTextBox;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTicketEmplacementTextBox;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTicketMotifTextBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox NewTicketTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TicketsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfAuthenticationApp;component/userdashboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserDashboard.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\..\UserDashboard.xaml"
            ((WpfAuthenticationApp.UserDashboard)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\UserDashboard.xaml"
            ((System.Windows.Controls.TabControl)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NewTicketDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.NewTicketOralementCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.NewTicketAppareilNomTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.NewTicketEtageTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.NewTicketEmplacementTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.NewTicketMotifTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.NewTicketTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            
            #line 102 "..\..\..\UserDashboard.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTicket_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 104 "..\..\..\UserDashboard.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTickets_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.TicketsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

