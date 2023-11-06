using Application.Data;
using Application.Models;

namespace Application.Interfaces;

public interface ILayoutService
{
    #region Properties

    LayoutStyle ActiveLayoutStyle { get; }
    
    SortOrder ActiveSortOrder { get; }

    #endregion

    #region Events

    event Action<LayoutStyle> LayoutStyleChanged;
    
    event Action<SortOrder> SortOrderChanged;

    #endregion

    #region Methods

    void ChangeLayoutStyle(LayoutStyle layoutStyle);

    void ChangeSortOrder(SortOrder sortOrder = SortOrder.Default);

    Session GetSessionByPosition(double x, double y);

    Window GetWindowByPosition(double x, double y);

    Tab GetTabByPosition(double x, double y);

    void FocusWindow(int windowId);

    #endregion
}