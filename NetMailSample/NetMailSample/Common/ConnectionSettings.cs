/****************************** Module Header ******************************\
Module Name:  ConnectionSettings.cs
Project:      NetMainSender
Copyright (c) Microsoft Corporation.

This class is used with the Load Settings feature to keep track of the different 
settings in the xml file that the user is attempting to load.

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetMailSample.Common
{
    public class ConnectionSettings
    {
        public string User = string.Empty;
        public string Domain = string.Empty;
        
        public bool UseSSL = false;
        public bool PasswordRequired = false;
        public bool SendByPort = false;
        public bool CustomPickupLocation = false;
        
        public string Server = string.Empty;
        public string Port = string.Empty;
        public string PickupLocation = string.Empty;

        public string MessageTo = string.Empty;
        public string MessageCC = string.Empty;
        public string MessageBcc = string.Empty;
        public string MessageSubject = string.Empty;
        public string MessageBody = string.Empty;
    }
}
