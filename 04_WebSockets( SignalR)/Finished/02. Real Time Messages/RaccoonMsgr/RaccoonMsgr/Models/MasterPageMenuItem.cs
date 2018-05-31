using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RaccoonMsgr.Models
{

    public class MasterPageMenuItem : BaseModel
    {
        public MasterPageMenuItem()
        {
        }
        private bool _isBadgeVisible = true;
        private int _updates = 0;

        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
        public string Icon { get; set; }
        public int Updates
        {
            get
            {
                return _updates;
            }
            set
            {
                _updates = value;
                RaisePropertyChanged();
            }
        }
        public bool IsBadgeVisible
        {
            get
            {
                return _isBadgeVisible;
            }
            set
            {
                _isBadgeVisible = value;
                RaisePropertyChanged();
            }
        }
        public Color BadgeColor { get; set; }
    }
}