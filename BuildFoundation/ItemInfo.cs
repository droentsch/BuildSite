using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFoundation.Contract;

namespace BuildFoundation
{
    [Serializable]
    public class ItemInfo : IItemInfo
    {
        private DateTime _createdDate;
        private String _created;
        public string Name
        {
            get;
            set;
        }
        public string Label
        {
            get;
            set;
        }

        public String Created
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
                if (!String.IsNullOrEmpty(_created))
                {
                    if (!DateTime.TryParse(_created, out _createdDate))
                    {
                        _createdDate = DateTime.MinValue;
                    }
                }
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
        }
    }
}
