﻿using Application.Data;
using Application.Elements.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Application.Services;

public static class DialogService
{
    private static readonly Dictionary<string, RenderFragment> _DialogFragments = new()
    {
        { DialogId.Save,    RenderDialog<SaveModal>(DialogId.Save)      },
        { DialogId.Rename,  RenderDialog<RenameDialog>(DialogId.Rename) }
    };
    
    private static readonly Dictionary<string, Dialog> _Dialogs = new();

    public static void ShowDialog(string id, Action<bool, object?>? callback = null)
    {
        _Dialogs[id].OpenModal(callback);
    }
    
    public static async Task<bool> ShowDialogAsync(string id, Action<bool, object?>? callback = null)
    {
        return await _Dialogs[id].OpenModalAsync(callback);
    }

    public static RenderFragment RenderDialog(string id)
    {
        return _DialogFragments[id];
    }
    
    public static IEnumerable<RenderFragment> RenderDialogs(params string[] ids)
    {
        return ids.Length <= 0 ? _DialogFragments.Values : ids.Select(id => _DialogFragments[id]);
    }
    
    public static IEnumerable<RenderFragment> RenderAllDialogs()
    {
        return _DialogFragments.Values;
    }
    
    public static RenderFragment RenderDialog<T>(string id) where T : Dialog
    {
        return _RenderFragment;

        void _RenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<T>(0);
            builder.AddAttribute(1, nameof(Dialog.Id), id);
            builder.AddComponentReferenceCapture(2, dialog =>
            {
                _Dialogs[id] = (T)dialog;
            });
            builder.CloseComponent();
        }
    }
}