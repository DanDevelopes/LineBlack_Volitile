using System;
using static GlobalEnums;
using static GlobalEnums.ItemEnums;

namespace Items.Wearables.interfaces
{
    public interface IWearable{
        public WornOn wornOn{ get; set;}
    }
}