using System;
using Xamarin.Forms;
namespace RaccoonMsgr.Renderers
{
    public interface IFloatingActionButtonRenderer
    {
        View CreateFloatingActionButton(Action tappedCallback);
    }
}
