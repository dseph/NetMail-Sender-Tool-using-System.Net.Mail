﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetMailSample.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ReadRcpt {
            get {
                return ((bool)(this["ReadRcpt"]));
            }
            set {
                this["ReadRcpt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ASCII")]
        public string BodyEncoding {
            get {
                return ((string)(this["BodyEncoding"]));
            }
            set {
                this["BodyEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ASCII")]
        public string SubjectEncoding {
            get {
                return ((string)(this["SubjectEncoding"]));
            }
            set {
                this["SubjectEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ASCII")]
        public string HeaderEncoding {
            get {
                return ((string)(this["HeaderEncoding"]));
            }
            set {
                this["HeaderEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Normal")]
        public string MsgPriority {
            get {
                return ((string)(this["MsgPriority"]));
            }
            set {
                this["MsgPriority"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AltViewHtml {
            get {
                return ((string)(this["AltViewHtml"]));
            }
            set {
                this["AltViewHtml"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AltViewPlain {
            get {
                return ((string)(this["AltViewPlain"]));
            }
            set {
                this["AltViewPlain"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QuotedPrintable")]
        public string htmlBodyTransferEncoding {
            get {
                return ((string)(this["htmlBodyTransferEncoding"]));
            }
            set {
                this["htmlBodyTransferEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AltViewCal {
            get {
                return ((string)(this["AltViewCal"]));
            }
            set {
                this["AltViewCal"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool BodyHtml {
            get {
                return ((bool)(this["BodyHtml"]));
            }
            set {
                this["BodyHtml"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QuotedPrintable")]
        public string plainBodyTransferEncoding {
            get {
                return ((string)(this["plainBodyTransferEncoding"]));
            }
            set {
                this["plainBodyTransferEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("QuotedPrintable")]
        public string vCalBodyTransferEncoding {
            get {
                return ((string)(this["vCalBodyTransferEncoding"]));
            }
            set {
                this["vCalBodyTransferEncoding"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int SendSyncTimeout {
            get {
                return ((int)(this["SendSyncTimeout"]));
            }
            set {
                this["SendSyncTimeout"] = value;
            }
        }
    }
}
