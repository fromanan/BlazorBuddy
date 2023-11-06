namespace Application.Interfaces;

public interface IBrowserService
{
    #region Methods

    void OpenAllTabs();

    void OpenTabsInOneWindow();

    void OpenTabsInThisWindow();

    #endregion
}