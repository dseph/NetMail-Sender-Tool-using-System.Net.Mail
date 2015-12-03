﻿/****************************** Module Header ******************************\
Module Name:  UserIoHelper.cs
Project:      NetMailSender
Copyright (c) Microsoft Corporation.

THis class is used to prompt the user with File dialogs for the Load and Save Settings features

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
using System.IO;
using System.Windows.Forms;

namespace NetMailSample.Common
{
    public class UserIoHelper
    {
        /// <summary>
        /// this function is used to get the save settings file location
        /// </summary>
        /// <param name="InitialDirectory"></param>
        /// <param name="SuggestedName"></param>
        /// <param name="SelectedFile"></param>
        /// <param name="FileFilter"></param>
        /// <returns></returns>
        public static bool PickSaveFileToFolder(string InitialDirectory, string SuggestedName, ref string SelectedFile, string FileFilter)
        {
            bool bRet = false;
            SaveFileDialog fsd = new SaveFileDialog();

            SelectedFile = SuggestedName;
            fsd.InitialDirectory = InitialDirectory;
            fsd.FileName = SuggestedName;
            fsd.Filter = FileFilter; 
            fsd.FilterIndex = 1;
            fsd.RestoreDirectory = false;
            fsd.Title = "Save Settings To File";

            if (fsd.ShowDialog() == DialogResult.OK)
            {
                bRet = true;
                SelectedFile = fsd.FileName.Trim();
            }
            fsd = null;
            return bRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InitialDirectory"></param>
        /// <param name="SuggestedName"></param>
        /// <param name="SelectedFile"></param>
        /// <param name="FileFilter"></param>
        /// <returns></returns>
        public static bool PickLoadFromFile(string InitialDirectory, string SuggestedName, ref string SelectedFile, string FileFilter)
        {
            bool bRet = false;
            OpenFileDialog fsd = new OpenFileDialog();

            SelectedFile = SuggestedName;
            fsd.InitialDirectory = InitialDirectory;
            fsd.FileName = SuggestedName;
            fsd.Filter = FileFilter;
            fsd.FilterIndex = 1;
            fsd.RestoreDirectory = false;
            fsd.Title = "Select File";

            if (fsd.ShowDialog() == DialogResult.OK)
            {
                bRet = true;
                SelectedFile = fsd.FileName.Trim();
            }
            fsd = null;
            return bRet;
        }
    }
}
