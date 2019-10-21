using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class RoomManagerValidator : IManagerValidator<RoomManager>
    {
        public bool IsDataValidForAddition(RoomManager manager)
        {
            return BasicValidator.IsIntValidInRange(manager.addRoomNo.Text, 0, 1000) &&
                   BasicValidator.IsIntValidInRange(manager.addRoomArea.Text, 0, 1000);
        }

        public bool IsDataValidForEdition(RoomManager manager)
        {
            return BasicValidator.IsIntValidInRange(manager.editRoomNo.Text, 0, 1000) &&
                   BasicValidator.IsIntValidInRange(manager.editRoomArea.Text, 0, 1000);
        }
    }
}
