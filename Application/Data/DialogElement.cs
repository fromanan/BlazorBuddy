using Microsoft.JSInterop;

namespace Application.Data;

public class DialogElement
{
    private readonly IJSRuntime _jsRuntime;

    private readonly string _id;

    public DialogElement(IJSRuntime jsRuntime, string id)
    {
        _jsRuntime = jsRuntime;
        _id = id;
    }

    public async Task OpenDialog()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("openDialog", _id);
    }

    public async Task CloseDialog()
    {
        await using IJSObjectReference module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/dialog.js");
        await module.InvokeVoidAsync("closeDialog", _id);
    }
}