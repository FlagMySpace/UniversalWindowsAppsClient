﻿using System;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace SSWindows.Controls
{
    public abstract class PageBase : Page, IView
    {
        public abstract Task ShowProgressBar(string text);

        public abstract Task HideProgressBar();
    }
}
