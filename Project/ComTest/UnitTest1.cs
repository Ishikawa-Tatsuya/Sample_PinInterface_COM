using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using mshtml;
using Codeer.Friendly;
using VSHTC.Friendly.PinInterface;

namespace ComTest
{
    [TestClass]
    public class Sample
    {
        WindowsAppFriend app;

        [TestInitialize]
        public void TestInitialize()
        {
            app = new WindowsAppFriend(Process.Start("ComTarget"));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Process.GetProcessById(app.ProcessId).CloseMainWindow();
        }

        [TestMethod]
        public void Test()
        {
            var web = app.Type().System.Windows.Forms.Application.OpenForms[0].Controls[0];
            web.Url = new Uri("http://www.codeer.co.jp/");
            while ((WebBrowserReadyState)web.ReadyState != WebBrowserReadyState.Complete)
            {
                Thread.Sleep(10);
            }

            var iframeWindow = web.Document.Window;
            AppVar com = iframeWindow.DomWindow;

            //IHTMLWindow2にピン止めする
            //実体は対象アプリ内にある
            var win = com.Pin<IHTMLWindow2>();
            //IHTMLDocument2で返ってくる
            //コピーではなく実体は対象アプリ内にある
            var doc = win.document.selection;
            //IHTMLSelectionObjectで返ってくる
            var sel = win.document.selection;
            var type = sel.type;
        }
    }
}
