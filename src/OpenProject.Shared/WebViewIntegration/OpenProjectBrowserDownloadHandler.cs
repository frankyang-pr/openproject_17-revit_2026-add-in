using System;
using Serilog;

namespace OpenProject.Shared.WebViewIntegration
{
  public sealed class OpenProjectBrowserDownloadHandler : CefSharp.IDownloadHandler
  {
    public event EventHandler<CefSharp.DownloadItem> OnBeforeDownloadFired;

    public event EventHandler<CefSharp.DownloadItem> OnDownloadUpdatedFired;

    public bool CanDownload(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, string url, string requestMethod)
    {
      return true;
    }

    public void OnBeforeDownload(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem,
      CefSharp.IBeforeDownloadCallback callback)
    {
      Log.Information("Download triggered for item '{name}'", downloadItem.SuggestedFileName);

      OnBeforeDownloadFired?.Invoke(this, downloadItem);

      if (callback.IsDisposed) return;

      using (callback)
        callback.Continue(downloadItem.SuggestedFileName, true);
    }

    public void OnDownloadUpdated(CefSharp.IWebBrowser chromiumWebBrowser, CefSharp.IBrowser browser, CefSharp.DownloadItem downloadItem,
      CefSharp.IDownloadItemCallback callback)
    {
      OnDownloadUpdatedFired?.Invoke(this, downloadItem);
    }
  }
}
