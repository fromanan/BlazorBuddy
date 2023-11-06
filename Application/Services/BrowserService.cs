using Application.Interfaces;

namespace Application.Services;

public class BrowserService : IBrowserService
{
    #region Public Methods

    public void OpenAllTabs()
    {
        Console.WriteLine("Opening all tabs!");
    }

    public void OpenTabsInOneWindow()
    {
        Console.WriteLine("Opening tabs in one window!");
    }

    public void OpenTabsInThisWindow()
    {
        Console.WriteLine("Opening tabs in this window!");
    }

    #endregion
}