using RaccoonMsgr.iOS.Renderers;
using RaccoonMsgr.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace RaccoonMsgr.iOS.Renderers
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        public static void Init() { }
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var view = item as ExtendedViewCell;
            if (cell != null)
            {
                // Disable native cell selection color style
                if (view.SelectedBackgroundColor == Color.Transparent)
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                else
                    cell.BackgroundColor = view.SelectedBackgroundColor.ToUIColor();

            }
            return cell;
        }
        public ExtendedViewCellRenderer()
        {

        }
    }
}
